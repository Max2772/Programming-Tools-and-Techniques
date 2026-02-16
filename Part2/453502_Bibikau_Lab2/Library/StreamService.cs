namespace Library;

using System.Text.Json;

public class StreamService<T>
{
    private SemaphoreSlim sem = new SemaphoreSlim(1, 1);
    private const int DELAY = 4000; 
    
    public async Task WriteToStreamAsync(Stream stream, IEnumerable<T> data, IProgress<string> progress)
    {
        progress.Report($"Thread {Environment.CurrentManagedThreadId}: Entered WriteToStreamAsync");
        await sem.WaitAsync();

        progress.Report($"Thread {Environment.CurrentManagedThreadId}: Started writing to stream");
        await JsonSerializer.SerializeAsync(stream, data);
        progress.Report($"Thread {Environment.CurrentManagedThreadId}: Completed writing to stream");

        await Task.Delay(DELAY);

        sem.Release();
        progress.Report($"Thread {Environment.CurrentManagedThreadId}: Exited WriteToStreamAsync");
    }

    public async Task CopyFromStreamAsync(Stream stream, string fileName, IProgress<string> progress)
    {
        progress.Report($"Thread {Environment.CurrentManagedThreadId}: Entered CopyFromStreamAsync");
        await sem.WaitAsync();

        progress.Report($"Thread {Environment.CurrentManagedThreadId}: Started copying from stream");
        using (FileStream fstream = new(fileName, FileMode.Create, FileAccess.Write))
        {
            stream.Seek(0, SeekOrigin.Begin);
            await stream.CopyToAsync(fstream);
        }
        progress.Report($"Thread {Environment.CurrentManagedThreadId}: Completed copying from stream");

        sem.Release();
        progress.Report($"Thread {Environment.CurrentManagedThreadId}: Exited CopyFromStreamAsync");
    }

    public async Task<int> GetStatisticsAsync(string fileName, Func<T, bool> filter)
    {
        using (FileStream stream = new(fileName, FileMode.Open, FileAccess.Read))
        {
            List<T>? entries = await JsonSerializer.DeserializeAsync<List<T>>(stream);
            if (entries is null)
            {
                return 0;
            }

            return entries.Where(filter).Count();
        }
    }
}