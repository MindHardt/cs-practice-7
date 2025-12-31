using App;

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, _) => cts.Cancel();


var urls = Input.GetUrls();
var dest = Input.GetOutputFile();

var destStream = dest.OpenWrite();





Console.CancelKeyPress += (_,_) => File.Delete(dest.FullName);


using HttpClient client = new HttpClient();

var writerOut = new StreamWriter(destStream);


var readers = new StreamReader[urls.Length];

await Parallel.ForAsync(0, urls.Length, async (i, cts) =>
{
    readers[i] = new StreamReader(await client.GetStreamAsync(urls[i]));
});


await Parallel.ForEachAsync(readers, async (reader, cts) =>
{
    await writerOut.WriteAsync(await reader.ReadToEndAsync());
});

foreach (var reader in readers)
{
    reader.Dispose();
}
await writerOut.DisposeAsync();
