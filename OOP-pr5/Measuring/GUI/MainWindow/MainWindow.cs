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
        mainBox.Append(Label.New("Measuring"));
        WindowTools.AddNavigationButton<MeasuringLengthDeviceWindow>("Measuring Length", mainBox);
        mainBox.Append(exitButton);
    }

}
