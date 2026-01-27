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
        var valid = false;
        while (valid is false)
        {
            Console.Write("Введите нужные URL через пробел: ");
            result = Console.ReadLine()!.Split(' ');
            valid = result.All(IsValidUri);
            if (valid is false)
            {
                Console.WriteLine("Ошибка! Вы ввели некорректные URL!");
            }
        }

        return result;
    }

    private static bool IsValidUri(string uri)
    {
        return Uri.TryCreate(uri, UriKind.Absolute, out var createdUri)
               && (createdUri.Scheme == Uri.UriSchemeHttp || createdUri.Scheme == Uri.UriSchemeHttps);
    }

    
    
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
                if (file.Exists)
                {
                    Console.WriteLine("Файл уже существует, хотите его перезаписать? (Y/N):");
                    while (true)
                    {
                        ConsoleKeyInfo key = Console.ReadKey();
                    
                        switch (key.Key)
                        {
                            case ConsoleKey.Y:
                                Console.Clear();
                                return file;
                            case ConsoleKey.N:
                                break;
                            default:
                                Console.WriteLine("Введите Y или N");
                                continue;
                        }
                        break;
                    }
                }
                else
                {
                    Console.Clear();
                    return file;
                }
            }
            catch
            {
                Console.WriteLine("Вы ввели некорректный путь или отказались от перезаписи файла. Попробуйте ещё раз.");
            }
        }
    }
}