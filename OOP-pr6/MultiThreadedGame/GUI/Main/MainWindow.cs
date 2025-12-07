using Gtk;
using MultiThreadedRace;

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
        mainBox.Append(Label.New("Parallel Race demo"));
        WindowTools.AddNavigationButton<MultiThreadedRaceWindow>("Threads Race", mainBox);
        WindowTools.AddNavigationButton<MultiTaskRaceWindow>("Task Race", mainBox);
        WindowTools.AddNavigationButton<ParallelForWindow>("Parallel For", mainBox);
        mainBox.Append(exitButton);
    }

}
