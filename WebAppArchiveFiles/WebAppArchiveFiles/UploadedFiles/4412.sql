--select * from TranslationTable where genericname = 'InvalidTimeSheetValuesOS_SL'

--Nu este permisa adaugarea de ore suplimentare de tip non sarbatoare legala in zile care sunt sarbatoare legala!
Update TranslationTable 
--Set Value = 'Nu este permisa adaugarea de Ore suplimentare non Sarbatoare Legala in zile de Sarbatoare Legala!'
set value = 'xxx'
Where GenericName = 'InvalidTimeSheetValuesNonOS_SL'
And Category = 'TimeSheetDailyCho'
And Culture = 'RO'

--Nu este permisa adaugarea de ore suplimentare de tip sarbatoare legala in zile care nu sunt sarbatoare legala!
Update TranslationTable 
--Set Value = 'Nu este permisa adaugarea de Ore suplimentare de tip Sarbatoare Legala in zile care nu sunt Sarbatori Legale!'
set value = 'yyy'
Where GenericName = 'InvalidTimeSheetValuesOS_SL'
And Category = 'TimeSheetDailyCho'
And Culture = 'RO'

