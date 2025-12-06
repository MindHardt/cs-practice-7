using System.Text.RegularExpressions;

namespace App;

/// <summary>
/// Считывает и валидирует ввод пользователя.
/// </summary>
public static class Input
{
    /// <summary>
    /// Считывает от пользователя URL файлов из интернета и валидирует их через IsValidUrl().
    /// </summary>
    /// <returns>Массив ссылок</returns>
    public static string[] GetUrls()
    {
        string[] result;
        while (true)
        {
            Console.Write("\nВведите нужные URL через пробел: "); 
            result = Console.ReadLine()!.Split();
            var valid = result.Any(IsValidUrl);
            if (!valid)
            {
                Console.WriteLine("Ошибка! Вы ввели некорректные URL!");
                continue;
            }

            break;
        }
        return result;
    }
    
    /// <summary>
    /// Проверяет, что строка валидна как url и возвращает bool
    /// </summary>
    /// <param name="str"></param>
    /// <returns>bool</returns>
    private static bool IsValidUrl(string str)
    {
        string strRegex = @"((http|https)://)(www.)?" +
                          "[a-zA-Z0-9@:%._\\+~#?&//=]" +
                          "{2,256}\\.[a-z]" +
                          "{2,6}\\b([-a-zA-Z0-9@:%" +
                          "._\\+~#?&//=]*)";
        Regex re = new Regex(strRegex);
        return re.IsMatch(str);
    }
    
    /// <summary>
    /// Считывает от пользователя путь до файла с результатом.
    /// </summary>
    public static FileInfo GetOutputFile()
    {
        while (true)
        {
            Console.Write("\nВведите путь до файла с результатом: ");
            try
            {
                var file = new FileInfo(Console.ReadLine()!);
                if (file.Exists && (!ChooseToOverwrite()))
                        GetOutputFile();
                
                return file;
            }
            catch
            {
                Console.WriteLine("Произошла ошибка, вероятно вы ввели некорректный путь. Попробуйте ещё раз.");
            }
        }
    }
    /// <summary>
    /// Спрашивает пользователя хочет ли он перезаписать файл
    /// </summary>
    /// <returns>Согласие/несогласие на перезапись файла</returns>
    private static bool ChooseToOverwrite()
    {
        Console.Write("Do you want to overwrite file? [y/n] ");
        var pressedKey = Console.ReadKey(true);
        while (pressedKey.Key is not (ConsoleKey.Y or ConsoleKey.N))
            ChooseToOverwrite();
        return pressedKey.Key == ConsoleKey.Y;
    }
}