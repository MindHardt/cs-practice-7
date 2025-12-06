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
        while (valid is false)
        {
            Console.Write("Введите нужные URL через пробел: ");
            result = Console.ReadLine()!.Split(' ');
            valid = result.Any(x => IsValidUrl(x) is true);
            if (valid is false)
            {
                Console.WriteLine("Ошибка! Вы ввели некорректные URL!");
            }
        }

        return result;
    }

    private static bool IsValidUrl(string url) => url.StartsWith("https://");
    
    
    /// <summary>
    /// Считывает от пользователя путь до файла с результатом.
    /// </summary>
    public static FileInfo GetOutputFile()
    {
        while (true)
        {
            Console.WriteLine("Введите путь до файла с результатом: ");
            try
            {
                var file = new FileInfo(Console.ReadLine()!);
                if (!ChooseToOverwrite(file))
                    GetOutputFile();
                return file;
            }
            catch
            {
                Console.WriteLine("Произошла ошибка, вероятно вы ввели некорректный путь. Попробуйте ещё раз.");
            }
        }
    }

    private static bool ChooseToOverwrite(FileInfo file)
    {
        Console.Write("Do you want to overwrite file? [y/n] ");
        var pressedKey = Console.ReadKey(true);
        while (pressedKey.Key is not (ConsoleKey.Y or ConsoleKey.N))
            ChooseToOverwrite(file);
        return pressedKey.Key == ConsoleKey.Y;
    }
}