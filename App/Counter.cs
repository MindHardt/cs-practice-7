namespace App;

public static class Counter
{
    public static void Count(FileInfo file)
    {
        int countOfLines = 0;

        using (StreamReader sr = new StreamReader(file.OpenRead()))
        {
            while (sr.ReadLine() != null)
            {
                countOfLines++;
            }
        }
        
        Console.WriteLine($"Число строк в итоговом файле: {countOfLines}");
    }
}