namespace App;

public static class Counter
{
    public static void Count(FileInfo file)
    {
        
        Console.WriteLine(File.ReadAllLines(file.ToString()).Length);
    }
}