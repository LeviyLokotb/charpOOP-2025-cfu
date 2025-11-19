using System;
using Gio;
using Gtk;
using HarfBuzz;

/// <summary>
/// Главное окно приложения, содержащее навигационные кнопки 
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>Кнопка для демонстрации неявных преобразований</summary>
    private Button ImplicitButton;
    /// <summary>Кнопка для демонстрации явных преобразований</summary>
    private Button ExplicitButton;
    /// <summary>Кнопка выхода</summary>
    private Button QuitButton;
    /// <summary>Кнопка для демонстрации валидируемого ввода вещественных чисел</summary>
    private Button ValidateButton;
    /// <summary>Кнопка для демонстрации преобразований строки в число с помощью Parse, TryParse, Convert</summary>
    private Button convertTryParseButton;
    /// <summary>Кнопка для демонстрации безопасного приведения ссылочных типов с помощью операторов as, is</summary>
    private Button asIsButton;
    /// <summary>Кнопка для демонстрации пользовательских преобразований с ключевыми словами implicit, explicit</summary>
    private Button ourConvButton;
    /// <summary>Кнопка для вычисления корня n-ной степени методом Ньютона</summary>
    private Button sqrtButton;
    /// <summary>Кнопка для демонстрации преобразования из любой системы счисления в любую (до основания 16)</summary>
    private Button notationButton;
    /// <summary>Кнопка для демонстрации преобразования между римскими и арабскими числами</summary>
    private Button RomeNotationButton;
    /// <summary>
    /// Инициализирует новый экземпляр главного окна приложения
    /// </summary>
    public MainWindow() : base()
    {
        Title = "GTK Application";
        SetDefaultSize(600, 400);

        var mainBox = new Box()
        {
            Homogeneous = true
        };
        mainBox.SetOrientation(Orientation.Vertical);
        mainBox.SetMarginTop(20);
        mainBox.SetMarginBottom(20);
        mainBox.SetMarginStart(20);
        mainBox.SetMarginEnd(20);
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
        MyTitle.Append(Label.New("Преобразования типов в C#"));

        // Implicit
        ImplicitButton = new Button();
        ImplicitButton.SetLabel("Неявные (Implicit)");
        ImplicitButton.OnClicked += (sender, e) =>
        {
            Program.WindowManager.ShowWindow<ImplicitWindow>();
        };
        mainBox.Append(ImplicitButton);
        // Explicit
        ExplicitButton = new Button();
        ExplicitButton.SetLabel("Явные (Explicit)");
        ExplicitButton.OnClicked += (sender, e) =>
        {
            Program.WindowManager.ShowWindow<ExplicitWindow>();
        };
        mainBox.Append(ExplicitButton);

        // Convert, Parse и TryParse
        convertTryParseButton = new Button();
        convertTryParseButton.SetLabel("Convert, Parse и TryParse");
        convertTryParseButton.OnClicked += (sender, e) =>
        {
            Program.WindowManager.ShowWindow<convertTryParseWindow>();
        };
        mainBox.Append(convertTryParseButton);

        // is, as
        asIsButton = new Button();
        asIsButton.SetLabel("is, as");
        asIsButton.OnClicked += (sender, e) =>
        {
            Program.WindowManager.ShowWindow<asIsWindow>();
        };
        mainBox.Append(asIsButton);

        // модификаторы implicit, explicit
        ourConvButton = new Button();
        ourConvButton.SetLabel("Пользовательские преобразования");
        ourConvButton.OnClicked += (sender, e) =>
        {
            Program.WindowManager.ShowWindow<ourConvWindow>();
        };
        mainBox.Append(ourConvButton);

        // Заголовок
        var MyTitle2 = new Box()
        {
            Homogeneous = true
        };
        MyTitle2.SetMarginTop(5);
        MyTitle2.SetMarginBottom(5);
        MyTitle2.SetMarginStart(5);
        MyTitle2.SetMarginEnd(5);
        MyTitle2.SetSpacing(20);
        mainBox.Append(MyTitle2);
        //
        MyTitle2.Append(Label.New("Числа"));

        // Валидация
        ValidateButton = new Button();
        ValidateButton.SetLabel("Валидатор ввода");
        ValidateButton.OnClicked += (sender, e) =>
        {
            Program.WindowManager.ShowWindow<ValidateWindow>();
        };
        mainBox.Append(ValidateButton);

        // Вычисление квадратного корня
        sqrtButton = new Button();
        sqrtButton.SetLabel("Вычисление квадратного корня");
        sqrtButton.OnClicked += (sender, e) =>
        {
            Program.WindowManager.ShowWindow<sqrtWindow>();
        };
        mainBox.Append(sqrtButton);

        // Преобразование между системами счисления
        notationButton = new Button();
        notationButton.SetLabel("Перевод между системами счисления");
        notationButton.OnClicked += (sender, e) =>
        {
            Program.WindowManager.ShowWindow<notationWindow>();
        };
        mainBox.Append(notationButton);

        // Преобразование в римскую и обратно
        RomeNotationButton = new Button();
        RomeNotationButton.SetLabel("Перевод между арабскими и римскими");
        RomeNotationButton.OnClicked += (sender, e) =>
        {
            Program.WindowManager.ShowWindow<RomeNotationWindow>();
        };
        mainBox.Append(RomeNotationButton);

        mainBox.Append(Label.New("Выход"));
        // Выход
        QuitButton = new Button();
        QuitButton.SetLabel("⏻ Выход ");
        QuitButton.OnClicked += (sender, e) =>
        {
            Program.AppQuit();
        };
        mainBox.Append(QuitButton);
    }

}
