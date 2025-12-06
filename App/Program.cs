using App;

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, _) => cts.Cancel(); // а что так можно было?!

var urls = Input.GetUrls();
var dest = Input.GetOutputFile();
var destStream = dest.OpenWrite();

await Parallel.ForEachAsync(urls, cts.Token, async (url, ct) =>
{
    try
    {
        if (ct.IsCancellationRequested)
            ct.ThrowIfCancellationRequested();
        using var http = new HttpClient();
        await using var content = await http.GetStreamAsync(url, ct);
        await content.CopyToAsync(destStream, ct);
        Console.WriteLine($"Successfully proceeded {url} to {destStream.Name}");
    }
    catch(Exception ex)
    {
        if (ex is OperationCanceledException)
        {
            destStream.Close();
            File.Delete(destStream.Name);
        }
        Console.WriteLine($"Oops! Something went wrong when processing {url}");
    }
});

await destStream.DisposeAsync();

