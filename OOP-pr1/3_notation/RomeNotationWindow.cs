using System.IO.Pipelines;
using Gtk;
public class RomeNotationWindow : templateWindow
{
    public RomeNotationWindow() : base()
    {
        controlBox.Append(Label.New("Перевод чисел между Арабскими и Римскими"));

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


        // Поле ввода
        var RometoArabicBox = new Box()
        {
            Homogeneous = true,
        };
        RometoArabicBox.SetMarginTop(5);
        RometoArabicBox.SetMarginBottom(5);
        RometoArabicBox.SetMarginStart(5);
        RometoArabicBox.SetMarginEnd(5);
        RometoArabicBox.SetSpacing(20);
        RometoArabicBox.Append(entryRome);
        // Преобразование Римские -> Арабские
        var RometoArabicButton = new Button();
        RometoArabicButton.SetLabel("Перевод");
        RometoArabicButton.OnClicked += (sender, e) =>
        {
            string? rome = entryRome.Buffer!.Text;
            if (rome == null) return;
            int num = RomeToArabic(rome);
            Log($"{rome} == {num}");
        };
        RometoArabicBox.Append(Label.New("Римские -> Арабские"));
        RometoArabicBox.Append(RometoArabicButton);


        // Поле ввода
        var ArabictoRomeBox = new Box()
        {
            Homogeneous = true,
        };
        ArabictoRomeBox.SetMarginTop(5);
        ArabictoRomeBox.SetMarginBottom(5);
        ArabictoRomeBox.SetMarginStart(5);
        ArabictoRomeBox.SetMarginEnd(5);
        ArabictoRomeBox.SetSpacing(20);
        ArabictoRomeBox.Append(entryInt);
        // Преобразование Арабские -> Римские
        var ArabictoRomeButton = new Button();
        ArabictoRomeButton.SetLabel("Перевод");
        ArabictoRomeButton.OnClicked += (sender, e) =>
        {
            string? s = entryInt.Buffer!.Text;
            if (s == null) return;
            int arabic = Convert.ToInt32(s);
            string rome = ArabicToRome(arabic);
            Log($"{arabic} == {rome}");
        };
        ArabictoRomeBox.Append(Label.New("Арабские -> Римские"));
        ArabictoRomeBox.Append(ArabictoRomeButton);

        // Поменять
        var swichButton = new Button();
        swichButton.SetLabel(" ⇌ ");
        bool swiched = true;
        swichButton.OnClicked += (sender, e) =>
        {
            if (swiched)
            {
                controlBox.Remove(RometoArabicBox);
                controlBox.Append(ArabictoRomeBox);
            }
            else
            {
                controlBox.Remove(ArabictoRomeBox);
                controlBox.Append(RometoArabicBox);
            }
            swiched = !swiched;
        };

        MyTitle.Append(swichButton);

        // Кнопка закрытия
        MyTitle.Append(closeButton);


        controlBox.Append(RometoArabicBox);
    }

    public Dictionary<char, int> RomeToArabicDict = new Dictionary<char, int>
    {
        {'I', 1 },
        {'V', 5 },
        {'X', 10 },
        {'L', 50 },
        {'C', 100 },
        {'D', 500 },
        {'M', 1000 },
    };

    public int RomeToArabic(string rome)
    {
        int result = 0;
        for (int i = 0; i < rome.Length; i++)
        {
            // Если число меньше следующего, оно вычитается
            if (i + 1 < rome.Length && RomeToArabicDict[rome[i]] < RomeToArabicDict[rome[i + 1]])
            {
                result -= RomeToArabicDict[rome[i]];
            }
            // Иначе прибавляется
            else
            {
                result += RomeToArabicDict[rome[i]];
            }
        }
        return result;
    }
    public Dictionary<int, string> ArabicToRomeDict = new Dictionary<int, string>
    {
        {1, "I"},
        {4, "IV"},
        {5, "V"},
        {9, "IX"},
        {10, "X"},
        {40, "XL"},
        {50, "L"},
        {90, "XC"},
        {100, "C"},
        {400, "CD"},
        {500, "D"},
        {900, "CM"},
        {1000, "M"},
    };
    public string ArabicToRome(int arabic)
    {
        string result = "";
        // Массив определённых чисел, от больших к меньшим
        var nums = ArabicToRomeDict.Keys.ToArray().Reverse();
        foreach (int div in nums)
        {
            // Остаток
            int residual = arabic % div;
            // Целочисленное деление
            int amount = (arabic - residual) / div;
            // Добавляем столько чисел, сколько раз поместилось
            if (amount > 0) for (int i = 0; i < amount; i++) result += ArabicToRomeDict[div];
            // Оставляем только остаток
            arabic = residual;
            if (residual <= 0) break;
        }
        return result;
    }
}