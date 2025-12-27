namespace App;

public class StreamReaderUrl
{
    private StreamReader _reader;

    public StreamReaderUrl(ref HttpClient http, string url)
    {
        _reader = new StreamReader(http.GetStreamAsync(url).GetAwaiter().GetResult());
    }

    public string ReadLine()
    {
        string line = _reader.ReadLine();
        if (line != null)
        {
            return line;
        }

        return "null";
    }
}