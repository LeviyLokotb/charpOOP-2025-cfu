using Gtk;

/// <summary>
/// Главное окно приложения, содержащее навигационные кнопки 
/// </summary>
public partial class MainWindow : baseWindow
{
    /// <summary>
    /// Инициализирует новый экземпляр главного окна приложения
    /// </summary>
    public MainWindow() : base()
    {
        mainBox.Append(Label.New("ПР-3"));
        WindowTools.AddNavigationButton<OverflowWindow>("Переполнение типов", mainBox);
        WindowTools.AddNavigationButton<HashWindow>("Хэширование", mainBox);
    }

}
