namespace App;

public static class Debug
{
    public static string Urls =
        "https://un1ver5e.ru/api/files/ny3kqx01.s1w.txt https://un1ver5e.ru/api/files/5hs0j15l.4si.txt" +
        " https://un1ver5e.ru/api/files/rqdszqht.sjz.txt https://un1ver5e.ru/api/files/xujzw3wz.j2r.txt" +
        " https://un1ver5e.ru/api/files/o1apoh5j.bdw.txt";

    public static FileInfo GetOutFileName()
    {
        if (!Directory.Exists("Out"))
        {
            Directory.CreateDirectory("Out");
        }
        
        for (int i = 1; i <= 100; i++)
        {
            if (!File.Exists($"Out//qwerty{i}.txt"))
            {
                Console.WriteLine(new FileInfo($"Out//qwerty{i}.txt"));
                return new FileInfo($"Out//qwerty{i}.txt");
            }
        }

        throw new Exception("ошибка создания файла");
    }
    
}