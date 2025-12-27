namespace App;

public class StreamReaderUrl
{
    private StreamReader _reader;

    public StreamReaderUrl(HttpClient http, string url)
    {
        _reader = new StreamReader(http.GetStreamAsync(url).GetAwaiter().GetResult());
    }

    public async Task<string> ReadLine()
    {
        string line = await _reader.ReadLineAsync();
        if (line != null)
        {
            return line;
        }

        return "null";
    }
}