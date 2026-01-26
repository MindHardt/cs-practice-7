using App;

var cts = new CancellationTokenSource();
var totalLines = 0;
object fileLock = new();

var uris = Input.GetUris();
var dest = Input.GetOutputFile();

Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

try
{
    using var writer = new StreamWriter(dest.Open(FileMode.Create));

    await Parallel.ForEachAsync(uris, cts.Token, async (uri, ct) =>
    {
        try
        {
            using var http = new HttpClient();
            using var stream = await http.GetStreamAsync(uri, ct);
            using var reader = new StreamReader(stream);

            string? line;
            while ((line = await reader.ReadLineAsync(ct)) != null)
            {
                Interlocked.Increment(ref totalLines);
                lock (fileLock)
                {
                    writer.WriteLine(line);
                }
            }
        }
        catch { }
    });

    Console.WriteLine();
    Console.WriteLine($"Победа. Всего строк: {totalLines}");
}
catch (OperationCanceledException)
{
    if (File.Exists(dest.FullName)) File.Delete(dest.FullName);
    Console.WriteLine("Операция прервана пользователем. Файл удалён");
}
