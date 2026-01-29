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
            valid = !result.Any(x => IsValidUri(x) is false);
            if (valid is false)
            {
                Console.WriteLine("Ошибка! Вы ввели некорректные URL!");
            }
        }

        return result;
    }

    private static bool IsValidUri(string uri) => Uri.TryCreate(uri, UriKind.Absolute, out _);

    private static bool IsValidPath(string path)
    {
        try
        {
            using (var file = File.Create(path))
            {
            }

            File.Delete(path);
            return true;
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("Указана неправильная директория");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
}
    
    
    /// <summary>
    /// Считывает от пользователя путь до файла с результатом.
    /// </summary>
    public static FileInfo GetOutputFile()
    {
        FileInfo file;
        while (true)
        {
            Console.Write("Введите путь до файла с результатом: ");
            file = new FileInfo(Console.ReadLine()!);
            try
            {
                if (file.Exists)
                {
                    Console.WriteLine("Файл с таким названием уже существует. Хотите его перезаписать? [y/n]");
                    var input = Console.ReadKey(true);
                    if (char.ToLowerInvariant(input.KeyChar) == 'y')
                    {
                        File.Delete(file.FullName);
                        return file;
                    }

                    Console.WriteLine("Вы отказались от перезаписи, вернемся в начало");
                    continue;
                }

                if (IsValidPath(file.FullName) is false)
                {
                    Console.WriteLine("Путь к файлу некорректен");
                    continue;
                }

                return file;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Произошла ошибка {file.FullName}: {e.Message}");
            }
        }
    }
}