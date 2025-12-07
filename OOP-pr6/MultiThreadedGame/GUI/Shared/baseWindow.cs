using Gtk;
using static WindowTools;
/// <summary>
/// Шаблон окна для приложения 
/// (наследуется от Gtk.Window)
/// </summary>
public class baseWindow : Window
{
    protected Box mainBox;
    protected Box controlPanel;
    protected Button closeButton;
    protected Button exitButton;
    public event EventHandler? WindowClosed;
    public event EventHandler? AppClosed;
    protected baseWindow() : base()
    {
        Title = "GTK Application";
        SetDefaultSize(1000, 600);

        // Главный контейнер
        mainBox = new Box()
        {
            //Homogeneous = true
        };
        ConfigureBox(mainBox);

        ScrolledWindow scrolled = new();
        scrolled.Child = mainBox;

        SetChild(scrolled);

        // Костыль -- "корзина" для ничейных виджетов
        Box nullBox = new Box();

        // Панель управления
        controlPanel = new Box()
        {
            Homogeneous = true
        };
        ConfigureBox(controlPanel, Orientation.Horizontal);
        mainBox.Append(controlPanel);

        // Вернуться в главное меню
        closeButton = AddButton("✗ Закрыть ", (sender, e) =>
        {
            WindowClosed?.Invoke(this, null!);
            
            WindowManager.ShowWindow<MainWindow>();
        });

        // Выход
        exitButton = AddButton("⏻ Выход ",  (sender, e) =>
        {
            WindowClosed?.Invoke(this, null!);
            AppClosed?.Invoke(this, null!);
            Program.AppQuit();
        });
    }
}