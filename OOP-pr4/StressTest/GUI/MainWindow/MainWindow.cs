using Gtk;
using StressTest;

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
        mainBox.Append(Label.New("Stress Test"));
        WindowTools.AddNavigationButton<StressTestWindow>("Generate Message", mainBox);
        WindowTools.AddNavigationButton<TestCaseResultWindow>("Passes and Failures", mainBox);
        mainBox.Append(exitButton);
    }

}
