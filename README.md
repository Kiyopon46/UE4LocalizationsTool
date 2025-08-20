# UE Localizations Tool

- Замінено бібліотеку для `CSV`.
- Виправлено роботу з `CSV`, через що втрачалися пробіли при імпорті в **Google Sheets** та **Crowdin**.
- Виправлено зависання програми при збереженні великого `.locres`.
- Прискорено роботу програми шляхом використання асинхронних методів.
- Українізація та покращення інтерфейсу.
- Підтримка `.locres v4` (**Stellar Blade**).

Для тестування швидкості було взято `.locres` файл на **107233** рядка з **мільйоном слів**.
### UE4 Localizations Tool 2.7:
Відкриття `.locres`: **28 с**; Імпорт `CSV`: **2,60 с**; Збереження `.locres`: **2 хв 47 с**.
### UE Localizations Tool 2.7.8:
Відкриття `.locres`: **1,283 с**; Імпорт `CSV`: **2,567 с**; Збереження `.locres`: **2,750 с**.

Original author of program is **amrshaheen61**
https://github.com/amrshaheen61/UE4LocalizationsTool
