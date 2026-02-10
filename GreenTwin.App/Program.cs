// See https://aka.ms/new-console-template for more information
using GreenTwin.App.Abstractions;
using GreenTwin.App.Simulation;

Console.WriteLine("--- GreenTwin System Rozpoczęty ---");

// KROK 1: Wybieramy, czy używamy symulacji czy sprzętu
IGreenhouseHardware greenhouse = new GreenhouseSimulator();

// KROK 2: Prosta pętla logiki
while (true)
{
    double water = greenhouse.ReadWaterLevelLiters();
    Console.WriteLine($"Poziom wody: {water:F1} L");

    if (water < 100)
    {
        Console.WriteLine("ALARM: Mało wody! Wyłączam pompę.");
        greenhouse.SetPumpState(false);
    }
    else
    {
        greenhouse.SetPumpState(true);
    }

    Thread.Sleep(1000);
}