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

var writerOut = new StreamWriter(destStream);

StreamReaderUrl[] readers = new StreamReaderUrl[urls.Length];

for (int i = 0; i < urls.Length; i++)
{
    readers[i] = new StreamReaderUrl(client, urls[i]);
}

bool[] isDone = new bool[urls.Length];
string line;

while (!isDone.All(x => x))
{
    for (int i = 0; i < urls.Length; i++)
    {
        if (isDone[i])
        {
            continue;
        }
        
        line = await readers[i].ReadLine();
        
        if (line == "null")
        {
            isDone[i] = true;
            continue;
        }
        
        writerOut.WriteLine(line);
        
    }
}
