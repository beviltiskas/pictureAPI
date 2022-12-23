# T120B165 Nuotraukų dalinimosi ir prekyvietės sistema „Momentary sight“ projekto ataskaita
## 1. Sprendžiamo uždavinio aprašymas
### 1.1 Sistemos paskirtis
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
### 1.2 Funkciniai reikalavimai
Svečias projekte galės:
  1. Peržiūrėti pradinį puslapį.
  2. Registruotis į internetinį puslapį.
  3. Prisijungti prie internetinio puslapio.
  
### Registruotas naudotojas (pardavėjas) galės:
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
  
### Administratorius galės:
  1. Patvirtinti pardavėjo registraciją.
  2. Patvirtinti pardavėjų sukurtus skelbimus.
  3. Šalinti pasirinktus pardavėjus.
  4. Šalinti netinkamus skelbimus.
  
### Sistemos architektūra Sistema sudarys:

  • Kliento pusė – React.js
  
  • Serverio pusė – .NET Core, duomenų bazė – MS SQL Server.
  
Žemiau pateiktame paveiksliuke matome sistemos diegimo diagrama. Sistemos talpinimui
bus pasirintkas Azure/AWS serveris. Kiekviena sistemos dalis bus sudiegta tam pačiame
serveryje. Internetinę aplikaciją naudotojas galės pasiekti naudodamas HTTP protokolą
(interneto naršyklę). Šioje sistemoje taip pat naudosime savo sukurtą įrašų parduotuvės
aplikacijų programavimo sąsają. Saugoti įrašus naudosime MS SQL serverį.

![image](https://user-images.githubusercontent.com/78384738/197424123-2f3d3c70-82ac-4e32-b5d2-71276637d61f.png)

<p align="center"> 1 Pav. Sistemos diegimo diagrama </p>

# API specifikacija
### POST /register

Sukuria naują naudotoją su nurodytais parametrais

#### Metodo URL

`https://localhost:7058/api/register`

#### Atsakymų kodai

| Pavadinimas | Kodas |
| ----------- | ----- |
| No Content  | 201   |
| Bad request | 400   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas          | Pavyzdys         |
| ----------- | ----------- | --------------------- | ---------------- |
| username    | Taip        | Naudotojo vardas      | `redas`          |
| password    | Taip        | Naudotojo slaptažodis | `Slaptazodis1!`  |
| email       | Taip        | Naudotojo Paštas      | `redas@kavk.com` |

#### Užklausos pavyzdys

`POST https://localhost:7058/api/register`

```
{
    "username" : "string",
    "email" : "string@gmail.com",
    "password" : "String!"
}
```

#### Atsakymo pavyzdys

```
{
    "id": "b5076589-8e92-4962-8467-b283fef5bd94",
    "userName": "string",
    "email": "string@gmail.com"
}
```

### POST /login

Gražina naudotojo sugeneruotą žetoną, kuris vėliau yra naudojamas atpažinti naudotojo rolei

#### Metodo URL

`https://localhost:7058/api/login`

#### Atsakymų kodai

| Pavadinimas | Kodas |
| ----------- | ----- |
| OK          | 200   |
| Bad request | 400   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas          | Pavyzdys        |
| ----------- | ----------- | --------------------- | --------------- |
| username    | Taip        | Naudotojo vardas      | `Testas1`       |
| password    | Taip        | Naudotojo slaptažodis | `Testas1!`      |

#### Užklausos pavyzdys

`POST https://localhost:7058/api/login`

```
{
  "username": "string1",
  "password": "String1!"
}
```

#### Atsakymo pavyzdys

```
{
    "accessToken" : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiWWVwYXMxMjM0IiwianRpIjoiNTg3ZTQ3MDktNzJmMi00MTRhLTg2M2ItZjNhOWZiYTJjMjZmIiwic3ViIjoiYjUwNzY1ODktOGU5Mi00OTYyLTg0NjctYjI4M2ZlZjViZDk0IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQXBwVXNlciIsImV4cCI6MTY3MTgxMDkxNSwiaXNzIjoiTWF0YXMiLCJhdWQiOiJUcnVzdGVkQ2xpZW50In0.NYkel-4847960smd_LNT-LTXDINJeDZaCitv23Jq5FI"
}
```

## Portfolio API metodai

### GET /portfolios

Gražina sąrašą visų portfolių

#### Metodo URL

`https://localhost:7058/api/portfolios`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |

#### Užklausos pavyzdys

`GET https://localhost:7058/api/portfolios`

#### Atsakymo pavyzdys

```
[
    {
        "id": "1957a04b-024f-4e85-df7c-08dabeac1904",
        "name": "portfolio1",
        "description": "portfolio1",
        "creationDate": "2022-11-04T21:32:38.9637616"
    },
    {
        "id": "9c7d6703-819f-4630-9803-08dac5db160f",
        "name": "portfolio2",
        "description": "portfolio2",
        "creationDate": "2022-11-14T00:56:38.432293"
    }
]
```

### GET /portfolios/{Guid}

Gražina portfolio pagal id, kuris perduodamas per URL

#### Metodo URL

`http://localhost:5058/api/portfolios/{Guid}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Not found    | 404   |

#### Užklausos pavyzdys

`GET  https://localhost:7058/api/portfolios/e2075d00-a16a-4d19-3dc2-08dac5e36efa`

#### Atsakymo pavyzdys

```
{
    "resource": {
        "id": "e2075d00-a16a-4d19-3dc2-08dac5e36efa",
        "name": "c",
        "description": "portfolio3",
        "creationDate": "2022-11-14T01:56:23.6190485"
    }
}
```

### POST /portfolios

Sukuria portfolio nurodytais parametrais, funkcija prieinama tik vartotojams

#### Metodo URL

` https://localhost:7058/api/portfolios`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| No Content   | 201   |
| Bad request  | 400   |
| Unauthorized | 401   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas           | Pavyzdys             |
| ----------- | ----------- | ---------------------- | -------------------- |
| name        | Taip        | Portfolio pabadinimas  | `Urban`              |
| description | Taip        | Portfolio apibūdinimas | `Urban albums`       |

#### Užklausos pavyzdys

`POST  https://localhost:7058/api/portfolios`

```
{
  "name": "Portfo2",
  "description": "Forest porfolio"
}
```

### PUT /portfolio/{Guid}

Atnaujiną portfolio duomenis su duotais parametrais, kurie buvo nurodyti užklausos metu, Guid kartu su URL, o kiti parametrai perduodami kartu su užklausos body, funkcija prieinama tik vartotojui, sukūrusiam potfolio, arba adminui.

#### Metodo URL

`https://localhost:7058/api/portfolios/animals/{Guid}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Bad request  | 400   |
| Unauthorized | 401   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas           | Pavyzdys             |
| ----------- | ----------- | ---------------------- | -------------------- |
| name        | Taip        | Portfolio pabadinimas  | `Urban exploration`  |
| description | Taip        | Portfolio apibūdinimas | `Urban albums`       |

#### Užklausos pavyzdys

`PUT https://localhost:7058/api/portfolios/239e8a7e-7705-4083-08fe-08dacb582260`

```
{
  "name": "Urban exploration",
  "description": "Urban albums"
}
```

#### Atsakymo pavyzdys

```
Tuščias body su statuso kodu (200 Success)
```

### DELETE /portfolio/{Guid}

Ištrina gyvūną su nurodytu id per URL, funkcija prieinama tik vartotojui, sukūrusiam potfolio, arba adminui.

#### Metodo URL

`https://localhost:7058/api/portfolios/{Guid}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| No Content   | 204   |
| Unauthorized | 401   |
| Not found    | 404   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas    | Pavyzdys                                 |
| ----------- | ----------- | --------------- | ---------------------------------------- |
| Guid          | Taip      | portfolio guid  | `ee300476-041a-45a6-0c75-08dacb963`      |

#### Užklausos pavyzdys

`DELETE https://localhost:7058/api/portfolios/ee300476-041a-45a6-0c75-08dacb963`

#### Atsakymo pavyzdys

```
Tuščias body su statuso kodu 204 No content
```

## Album API metodai

### GET /portfolios/{Guid}/albums

Gražina sąrašą visų specifinio porfolio albumų

#### Metodo URL

`https://localhost:7058/api/portfolios/{Guid}/albums`, funkcija prieinama prisijungusiems naudotojams

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |

#### Užklausos pavyzdys

`GET https://localhost:7058/api/portfolios/239e8a7e-7705-4083-08fe-08dacb582260/albums`

#### Atsakymo pavyzdys

```
[
    {
        "id": 1,
        "description": "2020/12/05 - Health",
        "isFinished": true,
        "animalId": 1
    }
]
```

### GET /portfolios/{Guid}/albums/{Guid}

Gražina portfolio albumą pagal Guid, kuris perduodamas per URL

#### Metodo URL

` https://localhost:7058/api/portfolios/{Guid}/albums/{Guid}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Not found    | 404   |

#### Užklausos pavyzdys

`https://localhost:7058/api/Portfolios/9c7d6703-819f-4630-9803-08dac5db160f/albums/4f430c09-dfdd-4f36-bd11-08dac5dd2a26`

#### Atsakymo pavyzdys

```
{
    "id": "4f430c09-dfdd-4f36-bd11-08dac5dd2a26",
    "name": "Porfolio albumas",
    "description": "Porfolio albumas",
    "creationDate": "2022-11-14T01:11:31.1742277"
}
```

### POST /porfolios/{Guid}/albums/

Sukuria portfolio albumą nurodytais parametrais, funkcija prieinama tik vartotojui, sukūrusiam potfolio, arba adminui.

#### Metodo URL

` https://localhost:7058/api/portfolios/{Guid}/albums`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| No Content   | 201   |
| Bad request  | 400   |
| Unauthorized | 401   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas       | Pavyzdys         |
| ----------- | ----------- | ------------------ | ---------------- |
| name        | Taip        | Albumo pavadinimas | `Albumas1`       |
| description | Taip        | Albumo aprašas     | `Pirmas albumas` |

#### Užklausos pavyzdys

`POST  https://localhost:7058/api/portfolios/{Guid}/albums`

```
{
    "id": "9d47b78a-df91-4741-028d-08dae4f9c0f8",
    "name": "Albumas1",
    "description": "Pirmas albumas",
    "creationDate": "2022-12-23T15:24:16.1587118Z"
}
```

### PUT /portfolios/{Guid}/albums/{Guid}

Atnaujiną albumo duomenis su duotais parametrais, kurie buvo nurodyti užklausos metu, id kartu su URL, o kiti parametrai perduodami kartu su užklausos body, funkcija prieinama tik vartotojui, sukūrusiam potfolio, arba adminui.

#### Metodo URL

`https://localhost:7058/api/portfolios/{Guid}/albums/{Guid}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Bad request  | 400   |
| Unauthorized | 401   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas       | Pavyzdys   |
| ----------- | ----------- | ------------------ | ---------- |
| name        | Taip        | Albumo pavadinimas | `Albumas2` |
| description | Taip        | Visito aprašas     | `Albumas2` |

#### Užklausos pavyzdys

`PUT https://localhost:7058/api/portfolios/239e8a7e-7705-4083-08fe-08dacb582260/albums/25c6600a-ffdf-445b-5a75-08dacb591088`

```
{
    "name": "Albumas2",
    "description": "Albumas2"
}
```

#### Atsakymo pavyzdys

```
Tuščias body su statuso kodu (200 Success)
```

### DELETE /portfolios/{Guid}/albums/{Guid}

Ištrina albumą su nurodytu id per URL, funkcija prieinama tik vartotojui, sukūrusiam potfolio, arba adminui.

#### Metodo URL

`https://localhost:7058/api/portfolios/{Guid}/albums/{Guid}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| No Content   | 204   |
| Unauthorized | 401   |
| Not found    | 404   |

#### Užklausos pavyzdys

`DELETE https://localhost:7058/api/Portfolios/73df27ac-9969-46d9-c119-08dab55c97ba/albums/e1f889de-bca0-43f3-77bb-08dab55d45ff`

#### Atsakymo pavyzdys

```
Tuščias body su statuso kodu 204 No content
```

## Procedūru API metodai

### GET /portfolios/{Guid}/albums/{Guid}/pictures

Gražina nuotraukų sąrašą visų specifinio albumo 

#### Metodo URL

`https://localhost:7058/api/portfolios/{Guid}/albums/{Guid}/pictures`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |

#### Užklausos pavyzdys

`GET  https://localhost:7058/api/portfolios/239e8a7e-7705-4083-08fe-08dacb582260/albums/25c6600a-ffdf-445b-5a75-08dacb591088/pictures`

#### Atsakymo pavyzdys

```
[
    {
        "id": "9782bc3d-546d-444d-a0ca-08dacb59a15d",
        "name": "Pirma Nuotrauka",
        "description": "Nuotrauka nr1",
        "creationDate": "2022-11-21T00:45:04.5593094",
        "isSold": true,
        "price": 100,
        "imageName": "chicago224504561.jpg",
        "image": null,
        "imagePath": "https://localhost:7058/Images/chicago224504561.jpg"
    },
    {
        "id": "19a38700-26bd-44bf-a0cb-08dacb59a15d",
        "name": "Antra Nuotrauka",
        "description": "Nuotrauka nr2",
        "creationDate": "2022-11-21T00:45:13.6788906",
        "isSold": false,
        "price": 0,
        "imageName": "city223037434.jpg",
        "image": null,
        "imagePath": "https://localhost:7058/Images/city223037434.jpg"
    }
]
```

### GET /portfolios/{Guid}/albums/{Guid}/pictures{Guid}

Gražina albumo nuotrauką pagal Guid, kuris perduodamas per URL

#### Metodo URL

` https://localhost:7058/api/portfolios/{Guid}/albums/{Guid}/pictures/{Guid}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Not found    | 404   |

#### Užklausos pavyzdys

`GET  https://localhost:7058/api/portfolios/239e8a7e-7705-4083-08fe-08dacb582260/albums/25c6600a-ffdf-445b-5a75-08dacb591088/pictures/19a38700-26bd-44bf-a0cb-08dacb59a15d`

#### Atsakymo pavyzdys

```
{
    "resource": {
        "id": "19a38700-26bd-44bf-a0cb-08dacb59a15d",
        "name": "Antra nuotrauka",
        "description": "Nuotrauka nr. 2",
        "creationDate": "2022-11-21T00:45:13.6788906",
        "isSold": false,
        "price": 0,
        "imageName": "city223037434.jpg",
        "image": null,
        "imagePath": "https://localhost:7058/Images/city223037434.jpg"
    }
}
```

### POST /portfolios/{Guid}/albums/{Guid}/pictures

Sukuria nuotrauką su nurodytais parametrais, funkcija prieinama tik vartotojams

#### Metodo URL

`https://localhost:7058/api/portfolios/239e8a7e-7705-4083-08fe-08dacb582260/albums/25c6600a-ffdf-445b-5a75-08dacb591088/pictures`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| No Content   | 201   |
| Bad request  | 400   |
| Unauthorized | 401   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas           | Pavyzdys          |
| ----------- | ----------- | ---------------------- | ----------------- |
| name        | Taip        | Nuotraukos pavadinimas | `Picture`         |
| description | Taip        | Nuotraukos aprašas     | `Second picture`  |
| price       | Taip        | Nuotraukos kaina       | `0`               |
| Image       | Taip        | Nuotrauka              | `picture.jpg`     |

#### Užklausos pavyzdys

`POST   https://localhost:7058/api/portfolios/239e8a7e-7705-4083-08fe-08dacb582260/albums/25c6600a-ffdf-445b-5a75-08dacb591088/pictures`

```
{
    "id": "26255457-24b4-49ad-8c65-08dae4fd292a",
    "name": "Picture",
    "description": "Second picture",
    "creationDate": "2022-12-23T15:48:39.4407927Z",
    "isSold": false,
    "price": 0,
    "imageName": "picture224839445.png",
    "image": {
        "contentType": "image/png",
        "contentDisposition": "form-data; name=\"Image\"; filename=\"picture.png\"",
        "headers": {
            "Content-Disposition": [
                "form-data; name=\"Image\"; filename=\"picture.png\""
            ],
            "Content-Type": [
                "image/png"
            ]
        },
        "length": 134316,
        "name": "Image",
        "fileName": "picture.png"
    },
    "imagePath": null
}
```

### PUT /portfolios/{Guid}/albums/{Guid}/pictures/{Guid}

Atnaujiną nuotraukos duomenis su duotais parametrais, kurie buvo nurodyti užklausos metu, Guid kartu su URL, o kiti parametrai perduodami kartu su užklausos body, funkcija prieinama tik vartotojui, sukūrusiam potfolio, arba adminui.

#### Metodo URL

`https://localhost:7058/api/portfolios/{Guid}/albums/{Guid}/pictures/{Guid}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| OK           | 200   |
| Bad request  | 400   |
| Unauthorized | 401   |

#### Parametrai

| Pavadinimas | Ar būtinas? | Apibūdinimas           | Pavyzdys          |
| ----------- | ----------- | ---------------------- | ----------------- |
| name        | Taip        | Nuotraukos pavadinimas | `Picture`         |
| description | Taip        | Nuotraukos aprašas     | `Second picture`  |
| price       | Taip        | Nuotraukos kaina       | `0`               |
| Image       | Taip        | Nuotrauka              | `picture.jpg`     |

#### Užklausos pavyzdys

`PUT https://localhost:7058/api/portfolios/239e8a7e-7705-4083-08fe-08dacb582260/albums/25c6600a-ffdf-445b-5a75-08dacb591088/pictures/19a38700-26bd-44bf-a0cb-08dacb59a15d`

```
{
    "id": "19a38700-26bd-44bf-a0cb-08dacb59a15d",
    "name": "Picture",
    "description": "Second picture",
    "creationDate": "2022-11-21T00:45:13.6788906",
    "isSold": true,
    "price": 1,
    "imageName": "picture225746953.jpg",
    "image": null,
    "imagePath": null
}
```

#### Atsakymo pavyzdys

```
Tuščias body su statuso kodu (200 Success)
```

### DELETE /portfolios/{Guid}/albums/{Guid}/pictures/{Guid}

Ištrina procedūrą su nurodytu Guid per URL, funkcija prieinama tik vartotojui, sukūrusiam potfolio, arba adminui.

#### Metodo URL

`https://localhost:7058/api/portfolios/{Guid}/albums/{Guid}/pictures/{Guid}`

#### Atsakymų kodai

| Pavadinimas  | Kodas |
| ------------ | ----- |
| No Content   | 204   |
| Unauthorized | 401   |
| Not found    | 404   |

#### Užklausos pavyzdys

`DELETE  https://localhost:7058/api/portfolios/239e8a7e-7705-4083-08fe-08dacb582260/albums/25c6600a-ffdf-445b-5a75-08dacb591088/pictures/4f245dae-7400-450f-6edb-08dacb8da72c`

#### Atsakymo pavyzdys

```
Tuščias body su statuso kodu 204 No content
```
# Išvados
-Įgauta žinių dirbant su front-end ir back-end technologijomis;
-Įgauta žinių kaip saugoti front-end ir back-end dalis debesyje;
