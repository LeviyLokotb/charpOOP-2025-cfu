using Gtk;

//using static Screen;
public partial class ValidateWindow : Window
{
    private Button parseButton;
    private Button closeButton;
    public ValidateWindow() : base()
    {
        Title = "GTK Application";
        SetDefaultSize(600, 400);
        var mainBox = new Box()
        {
        };
        mainBox.SetOrientation(Orientation.Vertical);
        mainBox.SetMarginTop(5);
        mainBox.SetMarginBottom(5);
        mainBox.SetMarginStart(5);
        mainBox.SetMarginEnd(5);
        mainBox.SetSpacing(20);
        SetChild(mainBox);

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
        mainBox.Append(MyTitle);
        //
        MyTitle.Append(Label.New("Вы можете ввести только число с плавающей точкой:"));
        // кнопка выхода
        closeButton = new Button();
        closeButton.SetLabel("✗ Закрыть ");
        closeButton.OnClicked += (sender, e) =>
        {
            Program.WindowManager.ShowWindow<MainWindow>();
        };
        MyTitle.Append(closeButton);

        // Текстовое поле
        TextView entry = new TextView()
        {
            CursorVisible = false,
            Editable = false,
            Monospace = true,
            MarginStart = 10,
            MarginEnd = 10,
            HeightRequest = 40,
            LeftMargin = 10,
            RightMargin = 10,
            TopMargin = 10,
        };
        SetFocus(entry);

        //entry.PlaceholderText = "Введите текст";
        mainBox.Append(entry);

        Screen screen = new Screen(entry);
        var keyController = EventControllerKey.New();
        keyController.OnKeyPressed += (sender, e) =>
        {
            screen.AddChar(e.Keyval, e.Keycode);
            return true;
        };
        entry.AddController(keyController);

        // Конвертация
        TextView outtext = new TextView()
        {
            CursorVisible = false,
            Editable = false,
            Monospace = true,
            MarginStart = 10,
            MarginEnd = 10,
            HeightRequest = 40,
            LeftMargin = 10,
            RightMargin = 10,
            TopMargin = 10,
        };
        parseButton = new Button();
        parseButton.MarginStart = 100;
        parseButton.MarginEnd = 100;
        parseButton.SetLabel("Ввод");
        parseButton.OnClicked += (sender, e) =>
        {
            TextBuffer buff = outtext.Buffer!;
            
            double? d = screen.Convert();
            string result = (d != null ? d.ToString() : "Некорректный ввод!")!;
            Console.WriteLine(result);
            buff.Text = result;
        };
        mainBox.Append(parseButton);
        mainBox.Append(outtext);

    }
}