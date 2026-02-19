using System.Collections.Concurrent;

namespace GreenTwin.App.Data;

/// <summary>
/// Reprezentuje "Cyfrowego Bliźniaka" szklarni, przechowując symulowany stan fizyczny.
/// W trybie symulacji, jest to jedyne źródło prawdy dla surowych odczytów czujników.
/// Klasa jest statyczna, aby zapewnić globalny, pojedynczy stan dla całej aplikacji.
/// </summary>
public static class GreenhouseState
{
    /// <summary>
    /// Przechowuje surowe wartości odczytów z poszczególnych kanałów ADC.
    /// Klucz: numer kanału ADC.
    /// Wartość: surowa wartość odczytu.
    /// </summary>
    public static ConcurrentDictionary<int, int> AdcRawValues { get; } = new();

    /// <summary>
    /// Pobiera surową wartość dla danego kanału ADC.
    /// Jeśli kanał nie istnieje w słowniku, zwraca 0.
    /// </summary>
    public static int GetAdcRawValue(int channel)
    {
        return AdcRawValues.TryGetValue(channel, out var value) ? value : 0;
    }

    /// <summary>
    /// Czyści cały stan symulacji. Używane głównie w testach.
    /// </summary>
    public static void ClearState()
    {
        AdcRawValues.Clear();
    }
}