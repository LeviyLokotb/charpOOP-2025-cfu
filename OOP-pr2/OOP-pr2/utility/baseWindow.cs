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
        SetChild(mainBox);

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
        closeButton = AddNavigationButton<MainWindow>("✗ Закрыть ");

        // Выход
        exitButton = AddButton("⏻ Выход ",  (sender, e) =>
        {
            Program.AppQuit();
        });
    }
}