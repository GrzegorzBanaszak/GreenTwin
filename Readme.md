# GreenTwin 🌿🤖

System inteligentnego zarządzania szklarnią oparty na .NET i Raspberry Pi.
Projekt realizuje koncepcję "Digital Twin" – pozwala na pełną symulację
warunków przed wdrożeniem fizycznego sprzętu.

## 🚀 Główne Założenia

- **Digital Twin:** Stworzenie "Cyfrowego Bliźniaka" szklarni, gdzie centralny stan (`GreenhouseState`) odzwierciedla warunki fizyczne. Umożliwia to pełną symulację i testowanie logiki sterowania (podlewanie, ogrzewanie) przed wdrożeniem na sprzęcie.
- **Modularność i Skalowalność:** Architektura oparta na DDD (Domain-Driven Design), serwisach i kontrolerach API, ułatwiająca rozbudowę systemu o nowe czujniki i funkcje.
- **Abstrakcja Sprzętu:** Płynne przejście z trybu symulacji na fizyczne urządzenia na Raspberry Pi dzięki zastosowaniu wstrzykiwania zależności (Inversion of Control).
- **Automatyzacja:** Niezawodne procesy w tle (`IHostedService`) do zarządzania szklarnią bez ciągłej interakcji użytkownika.
- **Interfejs:** Nowoczesny panel sterowania do monitorowania i zarządzania systemem.

## 🏛️ Architektura

1.  **Warstwa Domeny (DDD):** Każdy element (czujnik, pompa) jest modelem z własną logiką (np. `SoilSensor` przeliczający wartość RAW na %).
2.  **Warstwa Usług (Services):** Dedykowane serwisy (np. `SoilSensorService`) zarządzają cyklem życia obiektów domenowych (CRUD).
3.  **Warstwa API:** Kontrolery udostępniają funkcjonalność serwisów przez punkty końcowe HTTP, stanowiąc jedyny punkt wejścia dla UI.
4.  **Silnik Symulacji:** W trybie deweloperskim, modele domenowe odczytują i zapisują swój stan do centralnego obiektu `GreenhouseState`, symulując fizyczne interakcje.

## 🛠 Tech Stack

- **Language:** C# 12 / .NET 8+
- **Platform:** Raspberry Pi (Linux ARM)
- **Libraries:** \* `System.Device.Gpio` (sterowanie pinami)
  - `Iot.Device.Bindings` (obsługa czujników)
- **Architecture:** DDD, Services, API, Inversion of Control (IoC).

## 🛠 Elementy Systemu

- **Zbiornik**: Beczka 120l
- **Czujnik poziomu wody**: JSN-SR04T (wodoodporny ultradźwiękowy)
- **Atmosfera**: BME280
- **Gleba**: Capacitive Soil Moisture
- **Przetwornik ADC**: ADS1115
- **Światło**: BH1750
- **Sterowanie**: Moduł przekaźnika 1-kanał z Botlandu.
- **Zasilanie**: Zasilacz do kamer 12V 5A z regulacją napięcia 4 wyjścia.
- **Pompa wody**: Pompa membranowa serii 21 DC SFDP1-011-070-21
- **Rozgałęźnik**: Rozgałęźnik zasilania Pulsar AWZ593 5x1A 10-30V DC
- **Zawory**: 12V DC 1/2"

## 📈 Status Projektu

- [x] Planowanie architektury
- [x] Zdefiniowanie szczegółowej architektury (DDD, Digital Twin)
- [ ] Implementacja silnika symulacji (w toku)
- [ ] Budowa serwisów i kontrolerów API
- [ ] Budowa UI
- [ ] Integracja z RPi (Hardware)

```Mermaid
graph TD
```
