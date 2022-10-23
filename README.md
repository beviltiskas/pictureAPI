# T120B165 Nuotraukų dalinimosi ir prekyvietės sistema „Momentary sight“ projekto ataskaita
# 1. Sprendžiamo uždavinio aprašymas
# 1.1 Sistemos paskirtis
Projekto tikslas – paspartinti bei pagerinti nuotraukų pardavimo, dalinimosi, portfolio
kūrimo procesą.
Veikimo principas – šį projektą sudarys du komponentai: internetine aplikacija taip pat bus
pasitelkiama aplikacijų programavimo sąsaja.
Naudojimas – vartotojas norėdamas naudotis šia aplikacija prisiregistruos prie jos, galės
sudaryti nuotraukų ar nuotraukų albumų skelbimus ar paprastus įrašus, užpildyti reikiama
informaciją apie nuotrauką, pridėti tinkamas žymes bei kainą jei ketinama parduoti
nuotrauką ar albumą. Paskelbtus įrašus prisijungę naudotojai galės komentuoti.
Administratorius tvirtina arba atšaukia naujas pardavėjų registracijas, peržiūri sudarytus
skelbimus prieš paskelbimą (supildyta reikalinga informacija, skelbimas nėra kenksmingas).
# 1.2 Funkciniai reikalavimai
Svečias projekte galės:
  1. Peržiūrėti pradinį puslapį.
  2. Registruotis į internetinį puslapį.
  3. Prisijungti prie internetinio puslapio.
Registruotas naudotojas (pardavėjas) galės:
  1. Prisijungti prie aplikacijos.
  2. Atsijungti nuo aplikacijos.
  3. Įkelti nuotrauką ar nuotraukas.
  4. Pridėti nuotraukos aprašymą.
  5. Pridėti žymes.
  6. Pridėti Kainą.
  7. Paskelbti nuotrauką.
  8. Peržiūrėti kitų pardavėjų įkeltas nuotraukas ar albumus.
  9. Komentuoti kitų naudotojų nuotraukas ar albumus.
  10. Nusipirkti nuotrauką ar albumą.
Administratorius galės:
  1. Patvirtinti pardavėjo registraciją.
  2. Patvirtinti pardavėjų sukurtus skelbimus.
  3. Šalinti pasirinktus pardavėjus.
  4. Šalinti netinkamus skelbimus.
  2. Sistemos architektūra
Sistema sudarys:

  • Kliento pusė – React.js
  • Serverio pusė – .NET Core, duomenų bazė – MS SQL Server.
  
Žemiau pateiktame paveiksliuke matome sistemos diegimo diagrama. Sistemos talpinimui
bus pasirintkas Azure/AWS serveris. Kiekviena sistemos dalis bus sudiegta tam pačiame
serveryje. Internetinę aplikaciją naudotojas galės pasiekti naudodamas HTTP protokolą
(interneto naršyklę). Šioje sistemoje taip pat naudosime savo sukurtą įrašų parduotuvės
aplikacijų programavimo sąsają. Saugoti įrašus naudosime MS SQL serverį.

![image](https://user-images.githubusercontent.com/78384738/197424123-2f3d3c70-82ac-4e32-b5d2-71276637d61f.png)
