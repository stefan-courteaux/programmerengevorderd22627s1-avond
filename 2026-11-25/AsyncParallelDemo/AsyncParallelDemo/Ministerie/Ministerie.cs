using System.Collections.Concurrent;
using System.Diagnostics;

namespace AsyncParallelDemo.Ministerie;

// Opgelet! Dit is een complexere implementatie om de openstaande vraag uit de les te beantwoorden.
// Kijk in de cursus als je je wil houden aan het eenvoudige voorbeeld.

public class Ministerie
{
    public void RunRustigeDag()
    {
        var ambtenaar = new Ambtenaar() { Naam = "Karel" };
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        ambtenaar.DrinkKoffie();
        ambtenaar.VerrichtKleineTaak(1);
        ambtenaar.DrinkKoffie();
        ambtenaar.VerrichtKleineTaak(2);
        ambtenaar.DrinkKoffie();
        stopwatch.Stop();

        PrintVerstrekenTijd(stopwatch);
    }
    
    public void RunDrukkeDag()
    {
        var ambtenaar = new Ambtenaar() { Naam = "Karel" };
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        ambtenaar.VerrichtKleineTaak(1);
        ambtenaar.VerrichtGroteTaak(2);
        ambtenaar.VerrichtKleineTaak(3);
        stopwatch.Stop();

        PrintVerstrekenTijd(stopwatch);
    }
    
    public void RunDrukkeDagMetChef()
    {
        var chef = new Chef() {Naam = "Bernard"};
        var ambtenaar = new Ambtenaar() { Naam = "Karel", Chef = chef};
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        ambtenaar.VerrichtKleineTaak(1);
        ambtenaar.VraagToestemming(2);
        ambtenaar.VerrichtGroteTaak(2);
        ambtenaar.VerrichtKleineTaak(3);
        stopwatch.Stop();

        PrintVerstrekenTijd(stopwatch);
    }
    
    public async Task RunDrukkeDagMetChefAsync()
    {
        var chef = new Chef() {Naam = "Bernard"};
        var ambtenaar = new Ambtenaar() { Naam = "Karel", Chef = chef};
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        var wachtendeToestemming = ambtenaar.VraagToestemmingAsync(2); 

        ambtenaar.VerrichtKleineTaak(1); 
        ambtenaar.VerrichtKleineTaak(3);

        await wachtendeToestemming;
        ambtenaar.VerrichtGroteTaak(2);
        stopwatch.Stop();

        PrintVerstrekenTijd(stopwatch);
    }
    
    public async Task BeperkWachtTijdCrashAsync() // Dit gaat ontploffen
    {
        var chef = new Chef() {Naam = "Bernard"};
        var ambtenaar = new Ambtenaar() { Naam = "Karel", Chef = chef};
        var stopwatch = new Stopwatch();

        const int maxWachtTijdToestemmingMs = 50;

        stopwatch.Start();
        var wachtendeToestemming = ambtenaar.VraagToestemmingAsync(2)
            .WaitAsync(TimeSpan.FromMilliseconds(maxWachtTijdToestemmingMs)); 
        
        await wachtendeToestemming;
        
        stopwatch.Stop();

        PrintVerstrekenTijd(stopwatch);
    }
    
    public async Task BeperkWachtTijdCrashNietAsync() // Dit stopt met wachten, maar de taak blijft wel draaien
    {
        var chef = new Chef() {Naam = "Bernard"};
        var ambtenaar = new Ambtenaar() { Naam = "Karel", Chef = chef};
        var stopwatch = new Stopwatch();

        const int maxWachtTijdToestemmingMs = 50;

        stopwatch.Start();
        
        var wachtendeToestemming = ambtenaar.VraagToestemmingAsync(2)
            .WaitAsync(TimeSpan.FromMilliseconds(maxWachtTijdToestemmingMs));

        try
        {
            await wachtendeToestemming;
        }
        catch (TimeoutException e)
        {
            Console.WriteLine("Dat duurt hier te lang om te wachten op de toestemming.");
        }

        stopwatch.Stop();

        PrintVerstrekenTijd(stopwatch);
    }
    
    public async Task BeperkWachtTijdCrashNietEnStopTaakAsync() // Dit stopt met wachten en vraagt de taak te stoppen.
    {
        var chef = new Chef() {Naam = "Bernard"};
        var ambtenaar = new Ambtenaar() { Naam = "Karel", Chef = chef};
        var stopwatch = new Stopwatch();

        const int maxWachtTijdToestemmingMs = 85;

        stopwatch.Start();

        using var hetGeduldVanKarel = new CancellationTokenSource(TimeSpan.FromMilliseconds(maxWachtTijdToestemmingMs));
        var wachtendeToestemming = ambtenaar.VraagToestemmingAsync(2, hetGeduldVanKarel.Token);

        try
        {
            await wachtendeToestemming;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Dat duurt hier te lang om te wachten op de toestemming.");
            Console.WriteLine("Ik heb tegen de baas gezegd dat hij mag stoppen met denken.");
        }

        stopwatch.Stop();
        PrintVerstrekenTijd(stopwatch);
    }
    
    private static void PrintVerstrekenTijd(Stopwatch stopwatch)
    {
        Console.WriteLine($"Het ministerie had {stopwatch.ElapsedMilliseconds} 'minuten' nodig");
    }
}