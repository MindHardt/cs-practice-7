using App;

var cts = new CancellationTokenSource();
bool fileMustBeDeleted = false;
SemaphoreSlim semaphore = new(1);
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
    fileMustBeDeleted = true;
};

var uris = Input.GetUris();
var dest = Input.GetOutputFile();


using var http = new HttpClient();

await using (var destStream = dest.OpenWrite())
{
    try
    {
        await Parallel.ForEachAsync(uris, cts.Token, async (uri, ct) =>
        {
            await semaphore.WaitAsync();
            try
            {
                cts.Token.ThrowIfCancellationRequested();
                await using var content = await http.GetStreamAsync(uri, ct);
                await content.CopyToAsync(destStream, ct);
            }
            finally
            {
                semaphore.Release();
            }

        });
    }
    catch (OperationCanceledException)
    {

    }
}

if (fileMustBeDeleted && dest.Exists)
{
    dest.Delete();
}
else if (dest.Exists)
{
    Counter.Count(dest);
}
