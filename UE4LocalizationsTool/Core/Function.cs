using Helper.MemoryList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AssetParser
{
    public class Function
    {
        MemoryList FuncBlock;
        Uexp uexp;
        bool Modify;
        int ScriptBytecodeSize;
        int scriptStorageSize;
        List<int> offsetList;
        /// <summary>
        /// [old] [new] [size] 
        /// </summary>
        List<Tuple<int, int, int>> stringOffset;
        int NewSize;

        public Function(MemoryList memoryList, Uexp Uexp, bool modify = false)
        {
            uexp = Uexp;
            Modify = modify;
            offsetList = new List<int>();
            stringOffset = new List<Tuple<int, int, int>>();
            NewSize = 0;
            if (uexp.UassetData.EngineVersion < UEVersions.VER_UE4_16)
            {
                return;
            }

            memoryList.Skip(4 * 2);

            int numIndexEntries = memoryList.GetIntValue();

            List<int> IndexEntries = new List<int>();
            for (int i = 0; i < numIndexEntries; i++)
            {
                int ExportIndex = memoryList.GetIntValue();
                IndexEntries.Add(ExportIndex);
            }

            if (uexp.UassetData.Exports_Directory[Uexp.ExportIndex].Value >= 4 || uexp.UassetData.EngineVersion >= UEVersions.VER_UE4_ADDED_PACKAGE_OWNER)
            {
                if (uexp.UassetData.AutoVersion)
                {
                    uexp.UassetData.EngineVersion = UEVersions.VER_UE4_26;
                }

                int num = memoryList.GetIntValue();

                for (int i = 0; i < num; i++)
                {
                    ReadFProperty(memoryList);
                }
            }

            int StartPosition = memoryList.GetPosition();
            ScriptBytecodeSize = memoryList.GetIntValue();
            scriptStorageSize = memoryList.GetIntValue();

            FuncBlock = new MemoryList(memoryList.GetBytes(scriptStorageSize));
            while (FuncBlock.GetPosition() < FuncBlock.GetSize())
            {
                ReadExpression();
            }

            if (Modify)
            {
                var sortedStringOffsets = stringOffset.OrderBy(t => t.Item1).ToList();

                var offsetChanges = new Dictionary<int, int>();
                int currentOffsetChange = 0;
                foreach (var strOffset in sortedStringOffsets)
                {
                    currentOffsetChange += strOffset.Item3;
                    offsetChanges[strOffset.Item1] = currentOffsetChange;
                }

                for (int n = 0; n < offsetList.Count; n++)
                {
                    int oldOffset = offsetList[n];
                    int totalChange = 0;
                    foreach (var change in offsetChanges)
                    {
                        if (oldOffset > change.Key)
                        {
                            totalChange = change.Value;
                        }
                    }
                    FuncBlock.SetIntValue(FuncBlock.GetIntValue(false, oldOffset) + totalChange, false, oldOffset);
                }

                int newScriptSize = FuncBlock.GetSize() - scriptStorageSize;

                memoryList.Seek(StartPosition);
                memoryList.SetIntValue(memoryList.GetIntValue(false) + newScriptSize);
                memoryList.SetIntValue(memoryList.GetIntValue(false) + newScriptSize);

                memoryList.ReplaceBytes(scriptStorageSize, FuncBlock.ToArray());
            }
        }

        void ReadFProperty(MemoryList memoryList)
        {
            string Property = uexp.UassetData.GetPropertyName(memoryList.GetIntValue());
            memoryList.Skip(4);
            string NameProperty = uexp.UassetData.GetPropertyName(memoryList.GetIntValue());
            memoryList.Skip(4);
            int Flags = memoryList.GetIntValue();
            int ArrayDim = memoryList.GetIntValue();
            int ElementSize = memoryList.GetIntValue();
            long PropertyFlags = memoryList.GetInt64Value();
            ushort RepIndex = memoryList.GetUShortValue();
            long RepNotifyFunc = memoryList.GetInt64Value();
            byte BlueprintReplicationCondition = memoryList.GetByteValue();

            switch (Property)
            {
                case "BoolProperty":
                    memoryList.Skip(6);
                    break;
                case "ObjectProperty":
                case "DelegateProperty":
                case "StructProperty":
                case "SoftClassProperty":
                case "InterfaceProperty":
                case "ByteProperty":
                    memoryList.Skip(4);
                    break;
                case "EnumProperty":
                    memoryList.Skip(4);
                    ReadFProperty(memoryList);
                    break;
                case "ArrayProperty":
                case "SetProperty":
                    ReadFProperty(memoryList);
                    break;
                case "ClassProperty":
                    memoryList.Skip(8);
                    break;
                case "MapProperty":
                    ReadFProperty(memoryList);
                    ReadFProperty(memoryList);
                    break;
            }
        }

        void ReadPPOINTER()
        {
            if (uexp.UassetData.EngineVersion >= UEVersions.VER_UE4_25)
            {
                int num = FuncBlock.GetIntValue();
                for (int i = 0; i < num; i++)
                {
                    FuncBlock.Skip(4); //name
                    FuncBlock.Skip(4);
                }
                FuncBlock.Skip(4);
            }
            else
            {
                FuncBlock.Skip(4);
            }
        }

        void ReadExpressionArray(ExprToken eExpr)
        {
            while (ReadExpression() != eExpr) { }
        }

        ExprToken ReadExpression()
        {
            ExprToken token = (ExprToken)FuncBlock.GetByteValue();
            switch (token)
            {
                case ExprToken.EX_LocalVariable:
                case ExprToken.EX_InstanceVariable:
                case ExprToken.EX_DefaultVariable:
                case ExprToken.EX_ClassSparseDataVariable:
                case ExprToken.EX_LocalOutVariable:
                case ExprToken.EX_PropertyConst:
                    ReadPPOINTER();
                    break;
                case ExprToken.EX_Return:
                case ExprToken.EX_Assert:
                case ExprToken.EX_MetaCast:
                case ExprToken.EX_LetBool:
                case ExprToken.EX_ComputedJump:
                case ExprToken.EX_PopExecutionFlowIfNot:
                case ExprToken.EX_InterfaceContext:
                case ExprToken.EX_SoftObjectConst:
                case ExprToken.EX_ClearMulticastDelegate:
                case ExprToken.EX_FieldPathConst:
                    ReadExpression();
                    break;
                case ExprToken.EX_Jump:
                case ExprToken.EX_SkipOffsetConst:
                    offsetList.Add(FuncBlock.GetPosition());
                    FuncBlock.Skip(4);
                    break;
                case ExprToken.EX_JumpIfNot:
                case ExprToken.EX_Skip:
                    offsetList.Add(FuncBlock.GetPosition());
                    FuncBlock.Skip(4);
                    ReadExpression();
                    break;
                case ExprToken.EX_Nothing:
                case ExprToken.EX_EndParmValue:
                case ExprToken.EX_EndFunctionParms:
                case ExprToken.EX_Self:
                case ExprToken.EX_EndStructConst:
                case ExprToken.EX_EndArray:
                case ExprToken.EX_EndSet:
                case ExprToken.EX_EndMap:
                case ExprToken.EX_EndSetConst:
                case ExprToken.EX_EndMapConst:
                case ExprToken.EX_DeprecatedOp4A:
                case ExprToken.EX_PopExecutionFlow:
                case ExprToken.EX_Breakpoint:
                case ExprToken.EX_EndOfScript:
                case ExprToken.EX_WireTracepoint:
                case ExprToken.EX_Tracepoint:
                case ExprToken.EX_NoObject:
                case ExprToken.EX_IntZero:
                case ExprToken.EX_IntOne:
                case ExprToken.EX_True:
                case ExprToken.EX_False:
                case ExprToken.EX_NoInterface:
                    break;
                case ExprToken.EX_Let:
                case ExprToken.EX_LetMulticastDelegate:
                case ExprToken.EX_LetDelegate:
                case ExprToken.EX_LetObj:
                case ExprToken.EX_LetWeakObjPtr:
                case ExprToken.EX_LetValueOnPersistentFrame:
                case ExprToken.EX_AddMulticastDelegate:
                case ExprToken.EX_RemoveMulticastDelegate:
                case ExprToken.EX_ArrayGetByRef:
                    ReadPPOINTER();
                    ReadExpression();
                    ReadExpression();
                    break;
                case ExprToken.EX_ClassContext:
                case ExprToken.EX_Context:
                case ExprToken.EX_Context_FailSilent:
                case ExprToken.EX_BindDelegate:
                case ExprToken.EX_CallMulticastDelegate:
                case ExprToken.EX_StructMemberContext:
                case ExprToken.EX_ObjToInterfaceCast:
                case ExprToken.EX_CrossInterfaceCast:
                case ExprToken.EX_InterfaceToObjCast:
                    ReadExpression();
                    FuncBlock.Skip(4);
                    ReadPPOINTER();
                    ReadExpression();
                    break;
                case ExprToken.EX_VirtualFunction:
                case ExprToken.EX_LocalVirtualFunction:
                case ExprToken.EX_FinalFunction:
                case ExprToken.EX_LocalFinalFunction:
                case ExprToken.EX_CallMath:
                    FuncBlock.Skip(4);
                    ReadExpressionArray(ExprToken.EX_EndFunctionParms);
                    break;
                case ExprToken.EX_IntConst:
                case ExprToken.EX_FloatConst:
                case ExprToken.EX_ObjectConst:
                case ExprToken.EX_Int64Const:
                case ExprToken.EX_UInt64Const:
                    FuncBlock.Skip(4);
                    break;
                case ExprToken.EX_StringConst:
                case ExprToken.EX_UnicodeStringConst:
                    int thisoffset = FuncBlock.GetPosition();
                    if (!Modify)
                    {
                        string stringValue = (token == ExprToken.EX_StringConst) ? FuncBlock.GetStringUE(Encoding.ASCII) : FuncBlock.GetStringUE(Encoding.Unicode);
                        uexp.Strings.Add(new List<string>() { "FuncText" + uexp.ExportIndex, stringValue, "Changing this value will cause the game crash!", "#141412", "#FFFFFF" });
                    }
                    else
                    {
                        int oldSize = FuncBlock.GetIntValue(false, thisoffset);
                        FuncBlock.ReplaceStringUE_Func(uexp.Strings[uexp.CurrentIndex][1]);
                        int newSize = FuncBlock.GetIntValue(false, thisoffset);
                        stringOffset.Add(Tuple.Create(thisoffset + (FuncBlock.GetSize() - scriptStorageSize) - (newSize - oldSize), thisoffset, newSize - oldSize));
                        NewSize = FuncBlock.GetSize() - scriptStorageSize;
                        uexp.CurrentIndex++;
                    }
                    break;
                case ExprToken.EX_NameConst:
                case ExprToken.EX_InstanceDelegate:
                    FuncBlock.Skip(8);
                    break;
                case ExprToken.EX_RotationConst:
                case ExprToken.EX_VectorConst:
                    FuncBlock.Skip(12);
                    break;
                case ExprToken.EX_ByteConst:
                case ExprToken.EX_IntConstByte:
                case ExprToken.EX_PrimitiveCast:
                    FuncBlock.Skip(1);
                    break;
                case ExprToken.EX_TextConst:
                    byte TextLiteralType = FuncBlock.GetByteValue();
                    switch (TextLiteralType)
                    {
                        case 0:
                            break;
                        case 1:
                            ReadExpression();
                            ReadExpression();
                            ReadExpression();
                            if (uexp.DumpNameSpaces)
                            {
                                uexp.StringNodes.Add(new StringNode() { NameSpace = uexp.Strings[uexp.Strings.Count - 3][1], Key = uexp.Strings[uexp.Strings.Count - 2][1], Value = uexp.Strings[uexp.Strings.Count - 1][1] });
                            }
                            break;
                        case 2:
                        case 3:
                            ReadExpression();
                            break;
                        case 4:
                            FuncBlock.Skip(4);
                            ReadExpression();
                            ReadExpression();
                            if (uexp.DumpNameSpaces)
                            {
                                uexp.StringNodes.Add(new StringNode() { Key = uexp.Strings[uexp.Strings.Count - 2][1], Value = uexp.Strings[uexp.Strings.Count - 1][1] });
                            }
                            break;
                    }
                    break;
                case ExprToken.EX_TransformConst:
                    FuncBlock.Skip(40);
                    break;
                case ExprToken.EX_DynamicCast:
                    FuncBlock.Skip(4);
                    ReadExpression();
                    break;
                case ExprToken.EX_StructConst:
                    FuncBlock.Skip(8);
                    ReadExpressionArray(ExprToken.EX_EndStructConst);
                    break;
                case ExprToken.EX_SetArray:
                    if (uexp.UassetData.EngineVersion >= UEVersions.VER_UE4_CHANGE_SETARRAY_BYTECODE)
                    {
                        ReadExpression();
                    }
                    else
                    {
                        FuncBlock.Skip(4);
                    }
                    ReadExpressionArray(ExprToken.EX_EndArray);
                    break;
                case ExprToken.EX_SetSet:
                    ReadExpression();
                    FuncBlock.Skip(4);
                    ReadExpressionArray(ExprToken.EX_EndSet);
                    break;
                case ExprToken.EX_SetMap:
                    ReadExpression();
                    FuncBlock.Skip(4);
                    ReadExpressionArray(ExprToken.EX_EndMap);
                    break;
                case ExprToken.EX_SetConst:
                    ReadPPOINTER();
                    FuncBlock.Skip(4);
                    ReadExpressionArray(ExprToken.EX_EndSetConst);
                    break;
                case ExprToken.EX_MapConst:
                    ReadPPOINTER();
                    ReadPPOINTER();
                    FuncBlock.Skip(4);
                    ReadExpressionArray(ExprToken.EX_EndMapConst);
                    break;
                case ExprToken.EX_ArrayConst:
                    ReadPPOINTER();
                    FuncBlock.Skip(4);
                    ReadExpressionArray(ExprToken.EX_EndArrayConst);
                    break;
                case ExprToken.EX_EndArrayConst:
                    break;
                case ExprToken.EX_InstrumentationEvent:
                    byte EventType = FuncBlock.GetByteValue();
                    if (EventType == 4)
                    {
                        FuncBlock.Skip(8);
                    }
                    break;
                case ExprToken.EX_SwitchValue:
                    ushort numCases = FuncBlock.GetUShortValue();
                    offsetList.Add(FuncBlock.GetPosition());
                    FuncBlock.Skip(4);
                    ReadExpression();
                    for (int i = 0; i < numCases; i++)
                    {
                        ReadExpression();
                        offsetList.Add(FuncBlock.GetPosition());
                        FuncBlock.Skip(4);
                        ReadExpression();
                    }
                    ReadExpression();
                    break;
                default:
                    throw new Exception("Invalid token type: " + token.ToString("X2"));
            }
            return token;
        }
    }
}