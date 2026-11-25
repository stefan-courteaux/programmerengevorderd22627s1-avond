namespace AsyncParallelDemo.Ministerie;

public class Ambtenaar
{
    public string Naam { get; set; }
    public Chef Chef { get; set; }

    public void VerrichtKleineTaak(int id)
    {
        Thread.Sleep(100);
        Console.WriteLine($"{Naam} verrichte kleine taak {id}");
    }

    public void VerrichtGroteTaak(int id)
    {
        Thread.Sleep(250);
        Console.WriteLine($"{Naam} verrichte grote taak {id}");
    }

    public void DrinkKoffie()
    {
        Thread.Sleep(50);
        Console.WriteLine($"{Naam} dronk een koffie");
    }
    
    public void VraagToestemming(int id)
    {
        Console.WriteLine($"{Naam} vraagt toestemming voor taak {id}");
        Chef.GeefToestemming(id);
    }
    
    public Task VraagToestemmingAsync(int id, CancellationToken? cancellationToken = null)
    {
        Console.WriteLine(
            $"{Naam} vraagt toestemming voor taak {id} aan {Chef.Naam}");
        return Chef.GeefToestemmingAsync(id, cancellationToken);
    }
}

public class Chef : Ambtenaar
{
    public void GeefToestemming(int taakId)
    {
        Thread.Sleep(125);
        Console.WriteLine($"{Naam} geeft toestemming voor taak {taakId}");
    }
    
    public async Task GeefToestemmingAsync(int id, CancellationToken? cancellationToken = null)
    {
        // splits de 125 ms fake werk in chunks zodat opportuniteit ontstaat om cancelation te controleren.
        for (var i = 0; i < 5; i++) 
        {
            // we werken (fake) verder
            Console.WriteLine($"Chef {Naam} werkt aan de toestemming voor taak {id} - {100/5*(i+1)}%");
            await Task.Delay(25);
            
            // controleer of de caller vraagt deze task te cancelen
            if (cancellationToken is not null)
                cancellationToken.Value.ThrowIfCancellationRequested();
        }
        
        Console.WriteLine(
            $"Chef {Naam} geeft toestemming voor taak {id}");
    }
}