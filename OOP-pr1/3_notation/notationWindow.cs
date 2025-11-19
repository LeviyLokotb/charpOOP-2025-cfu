using System.IO.Pipelines;
using Gtk;
public class notationWindow : templateWindow
{
    public notationWindow() : base()
    {
        controlBox.Append(Label.New("Преобразование чисел из любой системы счисления в любую"));

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

        // Поле ввода для hex чисел
        controlBox.Append(entryHex);

        // Ввод данных - основание систем
        var inputBox = new Box()
        {
            // Homogeneous = true
        };
        inputBox.SetOrientation(Orientation.Horizontal);
        inputBox.SetMarginTop(5);
        inputBox.SetMarginBottom(5);
        inputBox.SetMarginStart(5);
        inputBox.SetMarginEnd(5);
        inputBox.SetSpacing(20);
        controlBox.Append(inputBox);

        Adjustment adjustment1 = Adjustment.New(10, 1, 16, 1, 2, 0);
        SpinButton fromButton = SpinButton.New(adjustment1, 1.0, 0);
        fromButton.Wrap = true;
        Adjustment adjustment2 = Adjustment.New(2, 1, 16, 1, 2, 0);
        SpinButton toButton = SpinButton.New(adjustment2, 1.0, 0);
        toButton.Wrap = true;

        inputBox.Append(Label.New("Из:"));
        inputBox.Append(fromButton);
        inputBox.Append(Label.New("В:"));
        inputBox.Append(toButton);

        // Преобразование
        var convertButton = new Button();
        convertButton.SetLabel("Перевести");
        convertButton.OnClicked += (sender, e) =>
        {
            (int fromBase, int toBase) = ((int)fromButton.Value, (int)toButton.Value);
            string num = entryHex.Buffer!.Text ?? "0";
            string? result = ConvertNotation(fromBase, toBase, num);
            if (result == null)
            {
                Log("Некорректный ввод!");
                return;
            }
            Log($"{num} ({fromBase})\n  ==\n{result} ({toBase})");
        };
        controlBox.Append(convertButton);
    }
    private readonly string alphabet = "0123456789ABCDEF";
    // Предполагаем что ввод уже частично валидный
    public string? ConvertNotation(int fromBase, int toBase, string num)
    {
        string fromAlphabet = alphabet[0..fromBase];
        string toAlphabet = alphabet[0..toBase];
        foreach(char digit in num)
        {
            if (!fromAlphabet.Contains(digit)) return null;
        }
        // Переводим в десятичную
        long temp = 0L;
        for (int i = 0; i < num.Length; i++)
        {
            char digit = num[i];
            int di = alphabet.IndexOf(digit);
            temp += (long)(di * Math.Pow(fromBase, num.Length - i - 1));
        }

        Console.WriteLine($"{num} ({fromBase})\n  ==\n{temp} (10)");
        // Переводим в заданную
        string result = "";
        while(temp > 0)
        {
            // Остаток
            int residual = (int)(temp % toBase);
            // Добавляем в конец
            result = toAlphabet[residual] + result;
            // Вычитаем
            temp = (temp - residual) / toBase;
            //Console.WriteLine(temp);
        }
        return result;
    }
}