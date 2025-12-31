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
        Console.Write("Введите нужные URL через пробел: ");
        string[] result = Console.ReadLine()!.Split(' ');

        var validResult = result.Where(x => IsValidUrl(x)).ToArray();

        if (validResult.Length == 0)
        {
            Console.Clear();
            Console.WriteLine("Ошибка! Вы ввели некорректные URL!");
            return GetUrls();
        }

        return validResult;
    }

    private static bool IsValidUrl(string uri) => Uri.TryCreate(uri, UriKind.Absolute, out _);
    
    
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

            char answer = Console.ReadKey().KeyChar;
            
            if (answer == 'y')
            {
                File.WriteAllText(file.FullName, "");
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