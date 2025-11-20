
# CardOperationsService:

**Mikroserwis do obsługi operacji na kartach – .NET 8 + CQRS + Clean Architecture**

---

## Opis projektu:

CardOperationsService to mikroserwis służący do pobierania i analizowania informacji o kartach płatniczych. Projekt został zaprojektowany zgodnie z zasadami Clean Architecture, w technologii .NET 8 z zastosowaniem wzorca CQRS i biblioteki MediatR.

---

## Stos technologiczny:

- .NET 8 (ASP.NET Core)
- Clean Architecture
- CQRS + MediatR
- Swagger (OpenAPI)
- FluentAssertions + xUnit (testy jednostkowe)

---

## Uruchomienie aplikacji:

1. Otwórz projekt w Visual Studio 2022.
2. Uruchom projekt `CardOperationsService.Api` jako projekt startowy.
3. Wejdź w przeglądarce na adres `http://localhost:5000/swagger` lub za pośrednictwem aplikacji Postman.

---

## Endpointy REST API

### POST `/api/CardActions/allowed`

Zwraca listę dozwolonych akcji dla podanej karty na podstawie jej typu, statusu i informacji czy PIN został ustawiony.

#### Przykład:
`http://localhost:5000/api/CardActions/allowed`


#### Przykład body:
```json
{
  "cardType": "Credit",
  "cardStatus": "Active",
  "isPinSet": true
}
```

> Wszystkie pola są **wymagane**\
> `cardType` i `cardStatus` muszą być poprawnymi wartościami enumów
> (`Debit`, `Credit`, `Prepaid`, `Blocked`, `Active`, itp.)


#### 🔄 Odpowiedź:
```json
{
  "allowedActions": [
    "ACTION1",
    "ACTION3",
    "ACTION4",
    "ACTION5",
    "ACTION6",
    "ACTION8",
    "ACTION9",
    "ACTION10",
    "ACTION11",
    "ACTION12",
    "ACTION13"
  ]
}
```

#### Możliwe wartości:
- `cardType`: `Debit`, `Credit`, `Prepaid`
- `cardStatus`: `Active`, `Inactive`, `Blocked`, `Restricted`, `Ordered`, `Closed`
- `isPinSet`: `true` / `false`

---

### GET `/api/CardDetails/{userId}`

Zwraca listę kart należących do użytkownika, z możliwością filtrowania
po typie, statusie i ustawieniu PIN.\
Obsługuje także paginację wyników.

#### Przykład:
`http://localhost:5000/api/CardDetails/User1?page=1&pageSize=10`

#### Request:
-   **Metoda:** `GET`
-   **URL:** `http://localhost:5000/api/CardDetails/{userId}`
-   **Wymagany parametr:** `userId` (np. `User1`, `User2`, `User3`)
-   **Opcjonalne query parametry:**
    -   `cardType` -- `Debit`, `Credit`, `Prepaid`
    -   `cardStatus` -- `Active`, `Blocked`, `Inactive`, `Closed`,
        `Ordered`, `Restricted`
    -   `isPinSet` -- `true` lub `false`
    -   `page` -- numer strony (domyślnie 1)
    -   `pageSize` -- liczba wyników na stronę (1--100, domyślnie 10)


#### Odpowiedź:
``` json
{
  "userId": "User1",
  "cards": [
    {
      "cardNumber": "Card13",
      "cardType": "Credit",
      "cardStatus": "Active",
      "isPinSet": true
    },
    {
      "cardNumber": "Card14",
      "cardType": "Credit",
      "cardStatus": "Active",
      "isPinSet": false
    }
  ],
  "totalCount": 2,
  "page": 1,
  "pageSize": 5,
  "totalPages": 1,
  "cardTypeFilter": "Credit",
  "cardStatusFilter": "Active",
  "isPinSetFilter": null
}
```

## Testowe wartości `userId`

Wbudowany provider (`InMemoryCardService`) obsługuje:

-   `User1`
-   `User2`
-   `User3`

Każdy z nich ma po kilkanaście kart o różnych typach i statusach.

---

### 🔹 GET `/api/CardDetails/{userId}/{cardNumber}`

Zwraca szczegóły pojedynczej karty przypisanej do użytkownika.

#### Przykład:
`GET http://localhost:5000/api/CardDetails/User1/Card14`

#### Request:
-   **Metoda:** `GET`
-   **URL:**
    `http://localhost:5000/api/CardDetails/{userId}/{cardNumber}`
-   **Parametry ścieżki:**
    -   `userId` -- identyfikator użytkownika (np. `User1`, `User2`)
    -   `cardNumber` -- numer karty (np. `Card11`, `Card14`, `Card23`)



#### Przykładowa odpowiedź:
``` json
{
  "cardNumber": "Card14",
  "cardType": "Credit",
  "cardStatus": "Active",
  "isPinSet": true
}
```

## Testowe dane
Można używać użytkowników:

-   `User1`
-   `User2`
-   `User3`

Przykładowe numery kart:

-   `Card11`, `Card12`, `Card13`, ... `Card39`\
    (Generowane w `InMemoryCardService`)

---

## Testy jednostkowe

Znajdują się w projekcie `CardOperationsService.Tests` i testują logikę zwracania dozwolonych akcji w klasach: `CardRules` i `InMemoryCardService`.

---

## 👤 Autor

**Piotr Przybylski**  
 

---

## Licencja

Projekt dostępny jako otwarte źródło – licencja MIT.
