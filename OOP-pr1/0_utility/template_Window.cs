using Gdk;
using Gtk;
public partial class templateWindow : Window
{
    protected Action<string> Log;
    protected Box controlBox;
    protected Box logBox;
    protected Button closeButton;
    protected TextView logTextView;
    protected TextView entry;
    protected TextView entry2;
    protected TextView entryInt;
    protected TextView entryHex;
    protected TextView entryRome;
    protected EventControllerKey keyControllerInt;
    protected EventControllerKey keyControllerHex;
    protected Screen screen;
    protected Screen screen2;
    protected Button clearButton;
    public templateWindow() : base()
    {
        Title = "GTK Application";
        SetDefaultSize(1000, 600);
        // Главный бокс
        var mainBox = new Box()
        {
            Homogeneous = true,
        };
        mainBox.SetOrientation(Orientation.Horizontal);
        mainBox.SetSpacing(20);
        SetChild(mainBox);

        // Элементы управления
        controlBox = new Box()
        {
        };
        controlBox.SetOrientation(Orientation.Vertical);
        mainBox.SetMarginTop(15);
        mainBox.SetMarginBottom(15);
        mainBox.SetMarginStart(15);
        mainBox.SetMarginEnd(15);
        controlBox.SetSpacing(15);
        mainBox.Append(controlBox);

        // Вывод текста
        logBox = new Box()
        {
            Homogeneous = true,
        };
        logBox.SetOrientation(Orientation.Vertical);
        logBox.SetSpacing(5);
        mainBox.Append(logBox);

        // Добавляем поле для вывода текста и метод
        logTextView = new TextView()
        {
            CursorVisible = false,
            Editable = false,
            Valign = Align.Fill,
            Halign = Align.Fill,
            Monospace = true,
        };
        var scrolledWindow = new ScrolledWindow()
        {
            MinContentHeight = 400,
        };
        scrolledWindow.SetChild(logTextView);
        logBox.Append(scrolledWindow);
        Log = LogMessage.CreateLogMessage(logTextView);

        //Log("Привет!");

        // Кнопка закрытия
        closeButton = new Button();
        closeButton.SetLabel("✗ Закрыть ");
        closeButton.OnClicked += (sender, e) =>
        {
            Program.WindowManager.ShowWindow<MainWindow>();
        };

        // Очистка экрана
        clearButton = new Button();
        clearButton.SetLabel("∅ Очистить");
        clearButton.OnClicked += (sender, e) =>
        {
            logTextView.Buffer!.Text = "";
        };

        // Валидированный ввод
        // Текстовое поле
        entry = new TextView()
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
        screen = new Screen(entry);
        var keyController = EventControllerKey.New();
        keyController.OnKeyPressed += (sender, e) =>
        {
            screen.AddChar(e.Keyval, e.Keycode);
            return true;
        };
        entry.AddController(keyController);

        //
        // Текстовое поле
        entry2 = new TextView()
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
        screen2 = new Screen(entry2);
        var keyController2 = EventControllerKey.New();
        keyController2.OnKeyPressed += (sender, e) =>
        {
            screen2.AddChar(e.Keyval, e.Keycode);
            return true;
        };
        entry2.AddController(keyController2);

        //
        // Текстовое поле
        entryInt = new TextView()
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
        keyControllerInt = EventControllerKey.New();
        keyControllerInt.OnKeyPressed += (sender, e) =>
        {
            TextBuffer buff = entryInt.Buffer!;
            buff.Text ??= "0";
            if (e.Keycode == 37 || e.Keycode == 22)
            {
                if (buff.Text.Length <= 0) return true;
                buff.Text = buff.Text![0..^1];
                return true;
            }

            char symb = (char)e.Keyval;
            if (!"0123456789".Contains(symb)) return true;
            buff.Text += symb;
            return true;
        };
        entryInt.AddController(keyControllerInt);


        //
        // Текстовое поле
        entryHex = new TextView()
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
        // Контроллер для hex ввода
        keyControllerHex = EventControllerKey.New();
        keyControllerHex.OnKeyPressed += (sender, e) =>
        {
            TextBuffer buff = entryHex.Buffer!;
            buff.Text ??= "0";
            if (e.Keycode == 37 || e.Keycode == 22)
            {
                if (buff.Text.Length <= 0) return true;
                buff.Text = buff.Text![0..^1];
                return true;
            }

            char symb = ((char)e.Keyval).ToString().ToUpper()[0];
            if (!"0123456789ABCDEF".Contains(symb)) return true;
            buff.Text += symb;
            return true;
        };
        entryHex.AddController(keyControllerHex);

        
        //
        // Текстовое поле
        entryRome = new TextView()
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
        // Контроллер для Rome ввода
        var keyControllerRome = EventControllerKey.New();
        keyControllerRome.OnKeyPressed += (sender, e) =>
        {
            TextBuffer buff = entryRome.Buffer!;
            buff.Text ??= "0";
            if (e.Keycode == 37 || e.Keycode == 22)
            {
                if (buff.Text.Length <= 0) return true;
                buff.Text = buff.Text![0..^1];
                return true;
            }

            char symb = ((char)e.Keyval).ToString().ToUpper()[0];
            if (!"IVXLCDM".Contains(symb)) return true;
            buff.Text += symb;
            return true;
        };
        entryRome.AddController(keyControllerRome);

        // 
        //controlBox.Append(Label.New("SAMPLE"));
    }
}