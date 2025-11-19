using Gtk;
using static Converter;

public class ourConvWindow : templateWindow
{
    private ComboBoxText MorFt;
    public ourConvWindow() : base()
    {
        controlBox.Append(Label.New("Пользовательские преобразования"));

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

        // Кнопка свеедний
        var helpButton = new Button();
        helpButton.SetLabel("≣ Сведения");
        helpButton.OnClicked += (sender, e) =>
        {
            Log("""
            Два класса - Метры (Meters) и Футы (Feet)
            Неявные (implicit): в double и string
            Явные (explicit): друг в друга
            """);
        };
        MyTitle.Append(helpButton);

        // Кнопка очистки
        MyTitle.Append(clearButton);
        
        // Кнопка закрытия
        MyTitle.Append(closeButton);

        // Ввод
        var inputBox = new Box()
        {
            Homogeneous = true
        };
        controlBox.SetMarginTop(5);
        controlBox.SetMarginBottom(5);
        controlBox.SetMarginStart(5);
        controlBox.SetMarginEnd(5);
        controlBox.SetSpacing(15);
        controlBox.Append(inputBox);

        // Валидация ввода
        inputBox.Append(entry);

        // Выпадающий список
        MorFt = new ComboBoxText();
        MorFt.AppendText("метры -> футы");
        MorFt.AppendText("футы -> метры");
        MorFt.SetActive(0);

        inputBox.Append(MorFt);

        // Кнопка
        var convButton = new Button();
        convButton.SetLabel("Конвертировать");
        convButton.OnClicked += (sender, e) =>
        {
            double? d = screen.Convert();
            if (d == null)
            {
                Log("Некорректный ввод!");
                return;
            }

            string selected = MorFt.GetActiveText() ?? "Не выбрано";
            if (selected == "метры -> футы")
            {
                Meters m = new Meters((double)d);
                Feet f = (Feet)m;
                Log($$"""
                Meters m = new Meters({{d}});
                Feet f = (Feet)m;
                Console.WriteLine($"{(string) m} = {(string) f}");
                """);
                Log($"Результат:\n{(string)m} = {(string)f}");
            }
            else
            {
                Feet f = new Feet((double)d);
                Meters m = (Meters)f;
                Log($$""" 
                Feet f = new Feet({{d}});
                Meters m = (Meters)f;
                Console.WriteLine($"{(string) f} = {(string) m}");
                """);
                Log($"Результат:\n{(string)f} = {(string)m}");
            }
        };
        controlBox.Append(convButton);
    }
}