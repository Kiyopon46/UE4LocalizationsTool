English | [українська](README-uk.md) | [日本語](README-ja.md)

# UE Localizations Tool

- Replaced the library for `.csv`.
- Fixed the work with `.csv`, which caused the loss of spaces when importing into **Google Sheets** and **Crowdin**.
- Fixed the program freezing when saving a large `.locres`.
- Speeded up the program by using asynchronous methods.
- Ukrainianization and interface improvement.
- Support for `.locres v4` (**Stellar Blade**).

For speed testing, a `.locres` file with **107233** lines and **a million words** was taken.
### UE4 Localizations Tool 2.7:
Opening `.locres`: **28 s 567 ms**; Importing `.csv`: **2 s 983 ms**; Saving `.locres`: **2 min 49 s 350 ms**.
### UE Localizations Tool 2.7.8:
Opening `.locres`: **1 s 283 ms**; Importing `.csv`: **2 s 567 ms**; Saving `.locres`: **2 s 750 ms**.

- Opening is about **95.5%** faster.
- Import is about **13.9%** faster.
- Saving is about **98.4%** faster.

Original author of program is **amrshaheen61**
https://github.com/amrshaheen61/UE4LocalizationsTool