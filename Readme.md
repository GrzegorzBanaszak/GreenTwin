# GreenTwin 🌿🤖

System inteligentnego zarządzania szklarnią oparty na .NET i Raspberry Pi.
Projekt realizuje koncepcję "Digital Twin" – pozwala na pełną symulację
warunków przed wdrożeniem fizycznego sprzętu.

## 🚀 Główne Cele

- **Symulacja:** Wirtualne środowisko do testowania logiki podlewania i ogrzewania.
- **Hardware:** Integracja z czujnikami (I2C/GPIO) na Raspberry Pi.
- **Interfejs:** Nowoczesny panel sterowania (C# / AvaloniaUI lub WPF).

## 🛠 Tech Stack

- **Language:** C# 12 / .NET 8+
- **Platform:** Raspberry Pi (Linux ARM)
- **Libraries:** \* `System.Device.Gpio` (sterowanie pinami)
  - `Iot.Device.Bindings` (obsługa czujników)
- **Architecture:** Inversion of Control (IoC) dla łatwej zamiany symulatora na sprzęt.

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
- [ ] Implementacja silnika symulacji
- [ ] Budowa UI
- [ ] Integracja z RPi (Hardware)

```Mermaid
graph TD

```
