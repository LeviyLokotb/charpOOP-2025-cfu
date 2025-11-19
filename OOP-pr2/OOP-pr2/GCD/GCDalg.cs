using System.Diagnostics;

public class GCDalg
{
    /// <summary>
    /// Засекаем время выполнения функции
    /// </summary>
    /// <param name="func"></param>
    /// <param name="args"></param>
    /// <returns>Результат выполнения функции и время выполнения в UTC</returns>
    public static (int, string, long) TimerThis<T>(Func<T[], (int, string)> func, params T[] args)
    {
        Stopwatch sw = new();
        sw.Start();
        (int result, string error) = func(args);
        sw.Stop();
        return (result, error, sw.ElapsedTicks);
    }

    /// <summary>
    /// Вычисление НОД методом Евклида для 2-х параметров
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    private static int GCD(int a, int b)
    {
        if (a == 0) return b;
        while (b != 0) if (a > b) a -= b; else b -= a;
        //while (b != 0) (a, b) = (b, a % b);
        return a;
    }

    /// <summary>
    /// Алгоритм Штейна для 2-х параметров -- максимально оптимизирует 
    /// вычисление НОД благодаря некоторым свойствам
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private static int GCDStein(int a, int b)
    {
        if (a == 0 || b == 0) return a | b; // Возвращаем ненулевое значение если есть ноль

        // Если в конце не 1 (чётные) то НОД(a, b) == 2 НОД(a/2, b/2)
        int shift;
        for (shift = 0; ((a | b) & 1) == 0; shift++)
        {
            a >>= 1; // a /= 2
            b >>= 1; // b /= 2
        }

        // Если a чётное и b нечётное, НОД(a, b) == НОД(a/2, b)
        while ((a & 1) == 0) a >>= 1;

        // Если оба нечётные и a > b, НОД(a, b) == НОД( (b-a)/2, a)
        do
        {
            // Если b чётное и a нечётное, НОД(a, b) == НОД(a, b/2)
            while ((b & 1) == 0) b >>= 1;
            
            if (a < b) b -= a;
            else (a, b) = (b, a - b);
            // Т.к. a и b нечётные, a - b чётное
            b >>= 1;
        } while (b != 0);

        return a << shift;
    }

    /// <summary>
    /// Вычисление НОД методом Евклида для N параметров
    /// </summary>
    /// <returns>НОД всех чисел и строка с ошибкой (пустая если всё хорошо)</returns>
    public static (int, string) GCD(params int[] nums)
    {
        if (nums.Length < 1) return (0, "Введите хотя бы одно число!");
        if (nums.Length == 1) return (nums[0], "");
        // Используем LINQ для упрощения
        // Aggregate - применяет к элементам агрегатную фунуцию поочерёдно
        // (постепенно уменьшает количество элементов, в итоге сводя к одному)
        int result;
        try
        {
            result = nums.Aggregate(GCD);
        }
        catch
        {
            return (0, "Ошибка во время вычислений");
        }
        ;
        return (result, "");
    }

    /// <summary>
    /// Алгоритм Штейна для N параметров -- максимально оптимизирует 
    /// вычисление НОД благодаря некоторым свойствам
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static (int, string) GCDStein(params int[] nums)
    {
        if (nums.Length < 1) return (0, "Введите хотя бы одно число!");
        if (nums.Length == 1) return (nums[0], "");
        // Используем LINQ для упрощения
        // Aggregate - применяет к элементам агрегатную фунуцию поочерёдно
        // (постепенно уменьшает количество элементов, в итоге сводя к одному)
        int result;
        try
        {
            result = nums.Aggregate(GCDStein);
        }
        catch
        {
            return (0, "Ошибка во время вычислений");
        }
        ;
        return (result, "");
    }

    public static (int, string) GCD(params string[] lines)
    {
        // Объединяем все строки
        string line = lines.Aggregate((a, b) => a + ' ' + b);
        try
        {
            ////////////// Считываем строку или "0" // Разделяем по ' ', пустые области удаляем // Ко всем применяем int.Parse // Преобразуем в массив
            int[] nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            return GCD(nums);
        }
        catch
        {
            return (0, "Неверный ввод!");
        }
    }
    public static (int, string) GCDStein(params string[] lines)
    {
        // Объединяем все строки
        string line = lines.Aggregate((a, b) => a + ' ' + b);
        try
        {
            ////////////// Считываем строку или "0" // Разделяем по ' ', пустые области удаляем // Ко всем применяем int.Parse // Преобразуем в массив
            int[] nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            return GCDStein(nums);
        }
        catch
        {
            return (0, "Неверный ввод!");
        }
    }
}