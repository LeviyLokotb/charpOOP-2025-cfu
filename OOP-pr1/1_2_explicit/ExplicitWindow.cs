using System;
using Gio;
using Gtk;
using HarfBuzz;

public partial class ExplicitWindow : Window
{
    private ComboBoxText comboBox1;
    private ComboBoxText comboBox2;
    private Button checkButton;
    private Button tableButton;
    private Button closeButton;
    private TextView logTextView;
    private Action<string> Log;

    public ExplicitWindow() : base()
    {
        Title = "GTK Application";
        SetDefaultSize(600, 400);

        var mainBox = new Box()
        {
        };
        mainBox.SetOrientation(Orientation.Vertical);
        mainBox.SetSpacing(5);
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

        /*// Ряд кнопок
        var MenuPanel = new Box()
        {
            Homogeneous = true
        };
        MenuPanel.SetMarginTop(5);
        MenuPanel.SetMarginBottom(5);
        MenuPanel.SetMarginStart(5);
        MenuPanel.SetMarginEnd(5);
        MenuPanel.SetSpacing(5);*/
        // Кнопка
        tableButton = new Button();
        tableButton.SetLabel("≣ Таблица");
        tableButton.OnClicked += OnTableButtonClicked;
        MyTitle.Append(tableButton);
        //
        MyTitle.Append(Label.New("Явные преобразования типов в C#"));
        // Кнопка
        closeButton = new Button();
        closeButton.SetLabel("✗ Закрыть ");
        closeButton.OnClicked += (sender, e) =>
        {
            Program.WindowManager.ShowWindow<MainWindow>();
        };
        MyTitle.Append(closeButton);
        //mainBox.Append(MenuPanel);

        // Панель управления
        var controlPanel = new Box()
        {
            Homogeneous = true,
        };
        controlPanel.SetOrientation(Orientation.Horizontal);
        controlPanel.SetSpacing(5);
        controlPanel.SetMarginTop(5);
        controlPanel.SetMarginStart(5);
        controlPanel.SetMarginEnd(5);
        mainBox.Append(controlPanel);

        // Выпадающие списки
        comboBox1 = new ComboBoxText();
        comboBox2 = new ComboBoxText();
        foreach (Converter.Type t in Enum.GetValues(typeof(Converter.Type)))
        {
            comboBox1.AppendText($"{t}");
            comboBox2.AppendText($"{t}");
        }
        comboBox1.SetActive(0);
        comboBox2.SetActive(0);

        // Кнопка
        checkButton = new Button();
        checkButton.SetLabel("Проверить");
        checkButton.OnClicked += OnCheckButtonClicked;

        // Добавляем всё на панель
        controlPanel.Append(Label.New("Из: "));
        controlPanel.Append(comboBox1);
        controlPanel.Append(Label.New("В: "));
        controlPanel.Append(comboBox2);
        controlPanel.Append(checkButton);

        // Логи
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
        mainBox.Append(scrolledWindow);
        // 
        Log = LogMessage.CreateLogMessage(logTextView);

        Log("Выберите типы чтобы проверить, возможно ли неявное преобразование");
    }

    private bool InProcessing = false;
    private void OnCheckButtonClicked(Button sender, EventArgs e)
    {
        if (InProcessing)
        {
            Log("Идёт обработка предыдущего запроса, подождите!");
            return;
        }
        InProcessing = true;
        string selected1 = comboBox1.GetActiveText() ?? "Не выбрано";
        string selected2 = comboBox2.GetActiveText() ?? "Не выбрано";
        Log($"Проверка: {selected1} -> {selected2}...");
        Log(ExplicitConv.ExplicitConvDemo(selected1, selected2));
        InProcessing = false;
    }
    private void OnTableButtonClicked(Button sender, EventArgs e)
    {
        // Многострочные string - фишка C# 11
        Log(
"""
o================o============================================================================o
|     FROM       |                           TO                                               |
o================o============================================================================o
| sbyte          ->  byte, ushort, uint, ulong, char                                          |
| byte           ->  sbyte, char                                                              |
| short          ->  sbyte, byte, ushort, uint, ulong, char                                   |
| ushort         ->  sbyte, byte, short, char                                                 |
| int            ->  sbyte, byte, short, ushort, uint, ulong, char                            |
| uint           ->  sbyte, byte, short, ushort, int, char                                    |
| long           ->  sbyte, byte, short, ushort, int, uint, ulong, char                       |
| ulong          ->  sbyte, byte, short, ushort, int, uint, long, char                        |
| char           ->  sbyte, byte, short                                                       |
| float          ->  sbyte, byte, short, ushort, int, uint, long, ulong, char, decimal        |
| double         ->  sbyte, byte, short, ushort, int, uint, long, ulong, char, float, decimal |
| decimal        ->  sbyte, byte, short, ushort, int, uint, long, ulong, char, float, double  |
o================o============================================================================o
""");
    }

    
}
