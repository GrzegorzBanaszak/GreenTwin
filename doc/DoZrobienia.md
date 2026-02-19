# Dokumentacja Elementów Systemu App

Niniejszy dokument zawiera specyfikację atrybutów oraz listę funkcji do zaimplementowania dla poszczególnych modułów systemu.

---

## 1. Sensory gleby

### Atrybuty

| Atrybut                | Typ      | Opis                                                                |
| :--------------------- | :------- | :------------------------------------------------------------------ |
| **Id**                 | `int`    | Unikalny identyfikator w systemie.                                  |
| **Description**        | `string` | Opis (np. "Donica - Papryka Chili", "Sekcja Północna").             |
| **AdcChannel**         | `int`    | Kanał w przetworniku ADS1115 (0, 1, 2 lub 3).                       |
| **DryValue**           | `int`    | Wartość surowa (RAW) odczytana w suchym powietrzu (np. ok. 20000).  |
| **WetValue**           | `int`    | Wartość surowa (RAW) odczytana po włożeniu do wody (np. ok. 10000). |
| **LastRawReading**     | `int`    | (Stan) Ostatni bezpośredni odczyt z ADC.                            |
| **MoisturePercentage** | `double` | (Stan) Wyliczona wilgotność (0-100%).                               |
| **MinThreshold**       | `double` | (Logika) Próg, poniżej którego system powinien podlać roślinę.      |

### Funkcje biznesowe

- [x] Pobieranie aktualnego stanu wilgotności w %
- [x] Aktualizacja opisu
- [ ] Aktualizacja pinu
- [x] Aktualizacja wartości kalibracji

### Serwis sensora gleby

- [x] Możliwość pobrania listy sensorów gleby
- [x] Pobieranie sensora po ID
- [x] Dodawanie sensora do listy
- [x] Usuwanie sensora z listy
- [x] Aktualizacja opisu sensora
- [ ] Aktualizacja pinu sensora
- [x] Aktualizacja wartości kalibracji sensora

---

## 2. Sensor poziomu wody

### Atrybuty

| Atrybut             | Typ      | Opis                                                              |
| :------------------ | :------- | :---------------------------------------------------------------- |
| **Id**              | `int`    | Unikalny identyfikator czujnika.                                  |
| **Description**     | `string` | Opis (np. "Główna beczka 120L").                                  |
| **TriggerPin**      | `int`    | Numer pinu GPIO wysyłającego impuls.                              |
| **EchoPin**         | `int`    | Numer pinu GPIO odbierającego powrót fali.                        |
| **EmptyDistance**   | `double` | Odległość od czujnika do dna pustej beczki (cm).                  |
| **FullDistance**    | `double` | Odległość od czujnika do lustra wody, gdy beczka jest pełna (cm). |
| **TotalCapacity**   | `double` | Całkowita pojemność zbiornika w litrach.                          |
| **CurrentDistance** | `double` | Ostatnio zmierzona surowa odległość (cm).                         |
| **CurrentLiters**   | `double` | (Stan) Aktualna ilość wody wyliczona w litrach.                   |

### Funkcje biznesowe

- [ ] Możliwość pobrania listy sensorów poziomu wody.

---

## 3. Sensory Atmosfery

### Atrybuty

| Atrybut         | Typ        | Opis                                                        |
| :-------------- | :--------- | :---------------------------------------------------------- |
| **Id**          | `int`      | Unikalny identyfikator.                                     |
| **Description** | `string`   | Opis (np. "Klimat wewnątrz", "Sekcja sadzonek").            |
| **I2cAddress**  | `int`      | Adres urządzenia na szynie I2C (standardowo 0x76 lub 0x77). |
| **Temperature** | `double`   | (Stan) Aktualna temperatura w stopniach Celsjusza.          |
| **Humidity**    | `double`   | (Stan) Aktualna wilgotność względna powietrza (%).          |
| **Pressure**    | `double`   | (Stan) Ciśnienie atmosferyczne (hPa).                       |
| **LastUpdate**  | `DateTime` | Znacznik czasu ostatniego odczytu.                          |

### Funkcje biznesowe

- [ ] Możliwość pobrania listy sensorów temperatury / atmosfery.
- [ ] Możliwość pobrania listy zaworów.

---

## 4. Sensory światła

### Atrybuty

| Atrybut             | Typ      | Opis                                                             |
| :------------------ | :------- | :--------------------------------------------------------------- |
| **Id**              | `int`    | Unikalny identyfikator czujnika.                                 |
| **Description**     | `string` | Opis (np. "Nasłonecznienie - Półka Górna").                      |
| **I2cAddress**      | `int`    | Adres I2C (zazwyczaj 0x23 lub 0x5C).                             |
| **CurrentLux**      | `double` | (Stan) Aktualne natężenie światła w luksach (lx).                |
| **MinLuxThreshold** | `double` | (Logika) Próg, poniżej którego należy włączyć doświetlanie.      |
| **MaxLuxThreshold** | `double` | (Logika) Próg, powyżej którego należy zasłonić rolety (ochrona). |

---

## 5. Pompa wody

### Atrybuty

| Atrybut           | Typ         | Opis                                                        |
| :---------------- | :---------- | :---------------------------------------------------------- |
| **Id**            | `int`       | Unikalny identyfikator pompy.                               |
| **Description**   | `string`    | Opis (np. "Główna pompa podająca", "Pompa sekcji A").       |
| **RelayPin**      | `int`       | Numer pinu GPIO, do którego podłączony jest przekaźnik.     |
| **IsRunning**     | `bool`      | (Stan) Czy pompa jest aktualnie włączona.                   |
| **LastStartedAt** | `DateTime?` | Znacznik czasu ostatniego uruchomienia.                     |
| **TotalRunTime**  | `TimeSpan`  | Całkowity czas pracy (serwisowanie i żywotność).            |
| **FlowRate**      | `double`    | Wydajność pompy (np. l/min) – pozwala szacować ubytek wody. |
