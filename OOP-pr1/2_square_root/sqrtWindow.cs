using System.IO.Pipelines;
using Gtk;

public class sqrtWindow : templateWindow
{
    public sqrtWindow() : base()
    {
        controlBox.Append(Label.New("Вычисление квадратного корня методом Ньютона"));
        // Заголовок
        var MyTitle = new Box()
        {
            Homogeneous = true
        };
        MyTitle.SetMarginTop(5);
        MyTitle.SetMarginBottom(5);
        MyTitle.SetMarginStart(5);
        MyTitle.SetMarginEnd(5);
        MyTitle.SetSpacing(20);
        controlBox.Append(MyTitle);

        // Кнопка очистки
        MyTitle.Append(clearButton);

        // Кнопка закрытия
        MyTitle.Append(closeButton);

        // Ввод с валидацией -- число
        controlBox.Append(Label.New("Введите число:"));
        controlBox.Append(entry);

        // Ввод с валидацией -- точность
        controlBox.Append(Label.New("Точность:"));
        controlBox.Append(entry2);
        screen2.SetScreen("1e-28");

        // Ввод с валидацией -- основание
        controlBox.Append(Label.New("Основание:"));
        controlBox.Append(entryInt);
        entryInt.Buffer!.Text = "2";

        // Вычисление
        var calculateButton = new Button();
        calculateButton.SetLabel("Вычисление квадратного корня");
        calculateButton.OnClicked += (sender, e) =>
        {
            double input = screen.Convert() ?? -1;
            decimal eps = screen2.ConvertDecimal() ?? 1e-28m;
            int degree = int.Parse(entryInt.Buffer.Text ?? "0");
            if (degree < 2)
            {
                Log("Введите корректное основание!");
                return;
            }
            if (input < 0)
            {
                Log("Введите корректное неотрицательное число!");
                return;
            }
            Log($"√{input}\nε = {eps}");

            // Наш метод
            (int iters, decimal? resultOwn) = OwnSqrt(input, eps, degree);
            Log($"= {resultOwn}\n{iters} итераций\n(Newton)");
            // Способ Microsoft
            if (degree == 2)
            {
                double resultMicrosoft = Math.Sqrt(input);
                Log($"= {resultMicrosoft}\n(Math.Sqrt)");
                Log($"Погрешность (разница между вычислениями):\n{Math.Abs((decimal)resultOwn! - (decimal)resultMicrosoft)}");
            }

        };
        controlBox.Append(calculateButton);
    }

    public static (int, decimal?) OwnSqrt(double n, decimal eps, int deg)
    {
        decimal num = (decimal)n;
        if (num < 0) return (0, null);
        if (num == 0 || num == 1) return (0, num);
        //decimal guess = num / 2m;

        /* IEEE 754 double
            [1 бит знак][11 бит экспонента][52 бита мантисса]
            value = (-1)^sign * (1 + mantissa/2^52) * 2^(exp - 1023)
        */
        // конвертируем в битовое представление
        long bits = BitConverter.DoubleToInt64Bits(n);

        // Экспонента - сдвигаем мантиссу и выбираем 11 бит
        long exp = ((bits >> 52) & 0b111111111111) - 1023;
        // Мантисса - выбираем 52 бита
        long mantissa = bits & 0b1111111111111111111111111111111111111111111111111111;
        // Мантисса - преобразуем в double
        double dmantissa = 1.0 + ( (double)mantissa / Math.Pow(2, 52) );
        // Мантисса - риближённый корень
        double sqrtmantissa = (1.0 + dmantissa) / deg;
        sqrtmantissa = (1.0 + sqrtmantissa) / deg;
        sqrtmantissa = (1.0 + sqrtmantissa) / deg;
        sqrtmantissa = (1.0 + sqrtmantissa) / deg;

        //Console.WriteLine($"Битовое представление: {bits}");
        //Console.WriteLine($"Экспонента: {exp}");
        //Console.WriteLine($"Мантисса: {mantissa}");
        // Приближение - 2^(exp/2) + (mantissa)^0.5
        double guess0 = Math.Pow(2, exp / deg) + sqrtmantissa;

        decimal guess = (decimal)guess0;
        Console.WriteLine($"Начальное приближение: {guess0}");

        int i = 0;
        decimal res = ((num / guess) + guess) / deg;
        while (Math.Abs(res - guess) > eps)
        {
            i++;
            guess = res;
            res = ((num / guess) + guess) / deg;
            Console.WriteLine($"{i}) Приближение: {guess} | Изменение: {guess-res}");
            if (i > 1000) break;
        }
        return (i, guess);
    }

}