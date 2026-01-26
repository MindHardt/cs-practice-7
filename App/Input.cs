namespace App;

/// <summary>
/// Считывает и валидирует ввод пользователя.
/// </summary>
public static class Input
{
    /// <summary>
    /// Считывает от пользователя URL файлов из интернета.
    /// </summary>
    public static string[] GetUris()
    {
        string[] result = [];
        while (true)
        {
            Console.Write("Введите нужные URL через пробел: ");
            result = Console.ReadLine()!.Split(' ');
            if (result.Length > 0 && result.All(x => IsValidUri(x))) return result;
            Console.WriteLine("Ошибка! Вы ввели некорректные URL!");
        }
    }

    private static bool IsValidUri(string uri) => uri.StartsWith("https://");
    
    
    /// <summary>
    /// Считывает от пользователя путь до файла с результатом.
    /// </summary>
    public static FileInfo GetOutputFile()
    {
        while (true)
        {
            Console.Write("Введите путь до файла с результатом: ");
            var file = new FileInfo(Console.ReadLine()!);
            if (file.Exists)
            {
                Console.WriteLine("Файл уже существует. Перезаписать ? (y/n): ");
                if (Console.ReadLine()!.ToLower() != "y") continue;
            }
            return file;
        }
    }
}