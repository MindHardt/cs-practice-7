using App;

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, _) => cts.Cancel(); // а что так можно было?!

var urls = Input.GetUrls();
var dest = Input.GetOutputFile();
var destStream = dest.OpenWrite();

await Parallel.ForEachAsync(urls, cts.Token, async (url, ct) =>
{
    using var http = new HttpClient();
    await using var content = await http.GetStreamAsync(url, ct);
    await content.CopyToAsync(destStream, ct);
});

await destStream.DisposeAsync();

