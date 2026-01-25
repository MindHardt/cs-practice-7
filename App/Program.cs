using App;
using System.Collections.Concurrent;

var cts = new CancellationTokenSource();
var dest = (FileInfo?)null;
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
    if (dest?.Exists == true)
    {
        try
        {
            dest.Delete();
            Console.WriteLine("\nФайл удален.");
        }
        catch
        {
            Console.WriteLine("\nНе удалось удалить файл.");
        }
    }
};
try
{
    var uris = Input.GetUris();
    dest = Input.GetOutputFile();

    var lineQueue = new ConcurrentQueue<string>();
    var totalLines = 0;
    var writeLock = new SemaphoreSlim(1, 1);
    await using var destStream = dest.Open(FileMode.Create, FileAccess.Write, FileShare.None);
    await using var writer = new StreamWriter(destStream);
    var tasks = uris.Select(async uri =>
    {
        try
        {
            using var http = new HttpClient();
            await using var stream = await http.GetStreamAsync(uri, cts.Token);
            using var reader = new StreamReader(stream);

            while (await reader.ReadLineAsync(cts.Token) is { } line)
            {
                await writeLock.WaitAsync(cts.Token);
                try
                {
                    await writer.WriteLineAsync(line);
                    totalLines++;
                }
                finally
                {
                    writeLock.Release();
                }
            }
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            Console.WriteLine($"Ошибка при чтении {uri}: {ex.Message}");
        }
    });

    await Task.WhenAll(tasks);

    Console.WriteLine($"\nВсего строк записано: {totalLines}");
}
catch (OperationCanceledException)
{
    Console.WriteLine("\nОперация отменена.");
}
