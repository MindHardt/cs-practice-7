using App;

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, arg) =>
{
    Console.WriteLine("Получена команда отмены");
    arg.Cancel = true;
    cts.Cancel();
};

var uris = Input.GetUris();
var destFile = Input.GetOutputFile();
var destStream = destFile.OpenWrite();

using var http = new HttpClient();
var semaphoreSlim = new SemaphoreSlim(3, 3);
try
{
    await Parallel.ForEachAsync(uris, cts.Token, async (uri, ct) =>
    {
        try
        {
            await using var content = await http.GetStreamAsync(uri, ct);
            await semaphoreSlim.WaitAsync(ct);
            try
            {
                await content.CopyToAsync(destStream, ct);
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            Console.WriteLine($"Ошибка при чтении файла {uri}: {e.Message}, файл игнорируется");
        }
    });
}
catch (OperationCanceledException)
{
    await destStream.DisposeAsync();
    if (File.Exists(destFile.FullName))
    {
        destFile.Delete();
    }
    Console.WriteLine("Программа экстренно завершила работу. Файл удален.");
    
}
finally
{
    await destStream.DisposeAsync();
}
Console.WriteLine($"Процесс завершен, данные загружены в {destFile.FullName}.");
int countLines = await CountLinesAsync.CountLines(destFile);
Console.WriteLine($"Количество строк в исходных файлах: {countLines}");