using App;

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, _) => cts.Cancel();

// var uris = Input.GetUris();
// var dest = Input.GetOutputFile();
// var destStream = dest.OpenWrite();

var urls = Debug.Urls.Split(' ');
var dest = Debug.GetOutFileName();
var destStream = dest.OpenWrite();

using HttpClient client = new HttpClient();

Console.WriteLine((await client.GetStringAsync("https://un1ver5e.ru/api/files/ny3kqx01.s1w.txt"))[0]);


StreamReader r = new StreamReader(await client.GetStringAsync("https://un1ver5e.ru/api/files/ny3kqx01.s1w.txt"));


Console.WriteLine(urls.Length);
Console.WriteLine(dest);

var writerOut = new StreamWriter(destStream);

StreamReader[] readers = new StreamReader[urls.Length];
Console.WriteLine(111);
Console.WriteLine(urls[0]);

for (int i = 0; i < urls.Length; i++)
{
    Console.WriteLine("-----");
    readers[i] = new StreamReader(urls[i]);
    Console.WriteLine("-----");
}

bool[] isDone = new bool[urls.Length];
string line;
Console.WriteLine(111);

while (!isDone.All(x => x))
{
Console.WriteLine(111);
    
    for (int i = 0; i < urls.Length; i++)
    {
        Console.WriteLine(111);
        if (isDone[i])
        {
            continue;
        }
        
        
        line = await readers[i].ReadLineAsync();
        Console.WriteLine(111);
        
        
        if (line == null)
        {
            isDone[i] = true;
            continue;
        }
        
        writerOut.WriteLine(line);
        Console.WriteLine(111);
        
    }
}



// await Parallel.ForEachAsync(urls, cts.Token, async (uri, ct) =>
// {
//     using var http = new HttpClient();
//     await using var content = await http.GetStreamAsync(uri, ct);
//     await content.CopyToAsync(destStream, ct);
// });

// await destStream.DisposeAsync();
