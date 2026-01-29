namespace App;

public class CountLinesAsync
{
    public static async Task<int> CountLines(FileInfo filePath)
    {
        int countLines = 0;
        using (StreamReader reader = new StreamReader(filePath.FullName))
        {
            while (await reader.ReadLineAsync() != null)
            {
                countLines++;
            }
        }
        return countLines;
    }
}