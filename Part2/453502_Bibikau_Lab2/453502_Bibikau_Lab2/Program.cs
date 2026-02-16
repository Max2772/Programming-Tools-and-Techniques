using Library;

namespace _453502_Bibikau_Lab2;

class Program
{
    static async Task Main(string[] args)
    {
        var streamService = new StreamService<Passenger>();

        List<Passenger> passengers = new List<Passenger>();
        Random rand = new Random();
        for (int i = 0; i < 1000; i++)
        {
            passengers.Add(new Passenger($"Passenger {i}", rand.Next(2) == 0));
        }

        Console.WriteLine($"Thread {Environment.CurrentManagedThreadId}: Начало работы");

        MemoryStream stream = new MemoryStream();
        Progress<string> progress = new Progress<string>(s => Console.WriteLine(s));

        Task writeTask = streamService.WriteToStreamAsync(stream, passengers, progress);
        await Task.Delay(150); 
        
        Task copyTask = streamService.CopyFromStreamAsync(stream, "passengers.json", progress);

        Console.WriteLine($"Thread {Environment.CurrentManagedThreadId}: Потоки 1 и 2 запущены");

        Task.WaitAll(writeTask, copyTask);

        Task<int> statsTask = streamService.GetStatisticsAsync("passengers.json", p => p.HasBaggage);
        int result = await statsTask;
        Console.WriteLine($"Статистика: {result}");
    }
}