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

## 📈 Status Projektu

- [x] Planowanie architektury
- [ ] Implementacja silnika symulacji
- [ ] Budowa UI
- [ ] Integracja z RPi (Hardware)
