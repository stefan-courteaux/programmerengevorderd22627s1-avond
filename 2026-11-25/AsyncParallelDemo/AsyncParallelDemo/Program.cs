using AsyncParallelDemo.Ministerie;
using AsyncParallelDemo.Patisserie;

// Patisserie
/*
var bakkerij = new Bakkerij();
bakkerij.RunSingle();
Console.WriteLine(new string('-', 50));
bakkerij.RunSequential();
Console.WriteLine(new string('-', 50));
bakkerij.RunParallel();
*/

// Ministerie
var ministerie = new Ministerie();
/*
ministerie.RunRustigeDag();
Console.WriteLine(new string('-', 50));
ministerie.RunDrukkeDag();
Console.WriteLine(new string('-', 50));
ministerie.RunDrukkeDagMetChef();
Console.WriteLine(new string('-', 50));
await ministerie.RunDrukkeDagMetChefAsync();
Console.WriteLine(new string('-', 50));
*/

// Wachttijd beperken
// - Enable maar 1 van de drie zodat de output niet door elkaar loopt!
await ministerie.BeperkWachtTijdCrashAsync(); // Dit gaat ontploffen wanneer wachttijd voorbij is.
// await ministerie.BeperkWachtTijdCrashNietAsync(); // Dit stopt met wachten, maar de taak blijft wel draaien
// await ministerie.BeperkWachtTijdCrashNietEnStopTaakAsync(); // Dit stopt met wachten en vraagt de taak te stoppen.