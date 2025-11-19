using Gtk;
public class Screen
{
    private string text { get; set; } = "0";
    private TextBuffer entry { get; set; }
    private bool NumAlreadyHaveDot { get; set; } = false;
    private bool NumAlreadyHaveExp { get; set; } = false;
    public bool IsComplete { get; set; } = true;

    public Screen(TextView e)
    {
        entry = e.Buffer!;
    }
    public void AddChar(uint keyval, uint keycode)
    {
        if (keycode == 37 || keycode == 22)
        {
            Backspace();
            return;
        }

        char symb = (char)keyval;
        Console.WriteLine($"Pressed {keycode} | {keyval} : {symb}");

        text += symb;
        ValidateScreen();
        UpdateScreen();
    }
    public void UpdateScreen()
    {
        entry.Text = text;
    }
    public void Backspace()
    {
        text = text[..^1]; // [0:-1]
        if (text.Length <= 0)
        {
            text = "0";
            UpdateScreen();
            return;
        }
        UpdateScreen();
    }
    public void SetScreen(string ss)
    {
        ClearScreen();
        foreach (char c in ss)
        {
            text += c;
            ValidateScreen();
            UpdateScreen();
        }
    }
    public void ClearScreen()
    {
        //text = text[0..0];
        //Backspace();
        text = "0";
        UpdateScreen();
    }
    public void ValidateScreen()
    {
        // Допускаем только определённые символы
        bool isValidSymb = false;
        string correctSymbols = "1234567890,.e+-";
        foreach (char s in correctSymbols)
        {
            if (text[^1] == s) // [-1]
            {
                isValidSymb = true;
            }
        }
        if (!isValidSymb)
        {
            Backspace();
            return;
        }
        // Заменяем запятые на точки
        if (text[^1] == ',')
        {
            Backspace();
            text += '.';
        }
        // Не допускаем точки если они уже есть (а так же после e)
        if (text[^1] == '.' && (NumAlreadyHaveDot || NumAlreadyHaveExp)) Backspace();
        // Если в начале 0, точку нужно поставить после него
        // Не допускаем E если они уже есть
        else if (text[^1] == 'e' && NumAlreadyHaveExp) Backspace();
        // Знаки допускаем только в начале или после E
        else if (text[^1] == '+' || text[^1] == '-')
        {
            if (!(text.Length == 2 || text[^2] == 'e')) Backspace();
            else if (text.Length == 2) text = (text[1]).ToString() + (text[0]).ToString();
        }

        while (text[0] == '0' && text.Length > 1 && text[1] != '.' && text[1] != 'e') text = text.Substring(1);

        IsComplete = text[^1] switch
        {
            '.' => false,
            'e' => false,
            '+' => false,
            '-' => false,
            _ => true
        };

        NumAlreadyHaveDot = false;
        NumAlreadyHaveExp = false;


        for (int i = text.Length - 1; i >= 0; i--)
        {
            switch (text[i])
            {
                case '.':
                    NumAlreadyHaveDot = true;
                    break;
                case 'e':
                    NumAlreadyHaveExp = true;
                    break;
                case '+':
                case '-':
                    break;
            }
        }

    }

    public double? Convert()
    {
        double n;
        //val = Convert.ToDouble(text);
        //val = Double.Parse(text);

        if (!Double.TryParse(text, out n))
        {
            if (!Double.TryParse(text.Replace('.', ','), out n))
            {
                return null;
            }
        }
        return n;
    }
    public decimal? ConvertDecimal()
    {
        //Console.WriteLine(text);
        decimal n;
        //val = Convert.ToDouble(text);
        //val = Double.Parse(text);

        if (!Decimal.TryParse(text, out n))
        {
            if (!Decimal.TryParse(text.Replace('.', ','), out n))
            {
                return null;
            }
        }
        return n;
    }
    public bool TryConvert(out double result)
    {
        double n;
        //val = Convert.ToDouble(text);
        //val = Double.Parse(text);

        if (!Double.TryParse(text, out n))
        {
            if (!Double.TryParse(text.Replace('.', ','), out n))
            {
                result = 0;
                return false;
            }
        }
        result = n;
        return true;
    }
}