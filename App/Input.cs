namespace App;

/// <summary>
/// Считывает и валидирует ввод пользователя.
/// </summary>
public static class Input
{
    /// <summary>
    /// Считывает от пользователя URL файлов из интернета.
    /// </summary>
    public static string[] GetUrls()
    {
        string[] result = [];
        var valid = false;
        
        Console.Write("Введите нужные URL через пробел: ");
        result = Console.ReadLine()!.Split(' ');

        var validResult = result.Where(x => IsValidUrl(x));

        if (validResult.Count() == 0)
        {
            Console.Clear();
            Console.WriteLine("Ошибка! Вы ввели некорректные URL!");
            return GetUrls();
        }

        return validResult.ToArray();
        
        
        
        // while (valid is false)
        // {
        //     Console.Write("Введите нужные URL через пробел: ");
        //     result = Console.ReadLine()!.Split(' ');
        //     valid = result.All(x => IsValidUrl(x));
        //     if (valid is false)
        //     {
        //         Console.WriteLine("Ошибка! Вы ввели некорректные URL!");
        //     }
        // }
        //
        // return result;
    }

    private static bool IsValidUrl(string uri) => uri.StartsWith("https://");
    
    
    /// <summary>
    /// Считывает от пользователя путь до файла с результатом.
    /// </summary>
    public static FileInfo GetOutputFile()
    {
        while (true)
        {
            Console.Write("Введите путь до файла с результатом: ");
            try
            {
                var file = new FileInfo(Console.ReadLine()!);
                Rewrite(file);
                return file;
            }
            catch
            {
                Console.WriteLine("Произошла ошибка, вероятно вы ввели некорректный путь. Попробуйте ещё раз.");
            }
        }
    }
    
    private static void Rewrite(FileInfo file)
    {
        if (File.Exists(file.FullName))
        {
            Console.Write($"файл {file.Name} уже существует, перезаписать? y/n: ");

            char answer = Console.ReadLine()!.ToLower()[0];
            
            if (answer == 'y')
            {
                File.Delete(file.FullName);
                using (File.Create(file.FullName)) { }
                return;
            }
            if (answer == 'n')
            {
                System.Environment.Exit(0);
                return;
            }
            throw new Exception("неверный ввод");
        }
    }
    
}