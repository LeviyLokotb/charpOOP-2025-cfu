//#
using Gtk;
using System;
public partial class Program
{
    /// <summary>Точка входа в программу</summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        StartGTK(args);
    }
    /// <summary>Инициализация приложения GTK</summary>
    private static Application app = Application.New("com.leviylokotb.OOP-pr1", Gio.ApplicationFlags.FlagsNone);
    /// <summary>Функция, запускающая отображение GUI приложения </summary>
    /// <param name="args"></param>
    static void StartGTK(string[] args)
    {
        app.OnActivate += (sender, e) =>
        {
            WindowManager manager = new WindowManager(app);
            WindowManager.ShowWindow<MainWindow>();
        };

        app.RunWithSynchronizationContext(args);
    }
    /// <summary>Класс, отвечающй за смену отображаемого окна</summary>
    public class WindowManager
    {
        /// <summary>Ссылка на экземпляр приложения</summary>
        private static Application? sender;
        /// <summary>Поле, которое хранит текущий объект окна, наследуемый от Gtk.Window</summary>
        private static Window? currentWindow;
        /// <summary>Инициалиизрует WindowManager</summary>
        public WindowManager(Application a)
        {
            sender = a;
        }
        /// <summary>Дженерик для смены окон</summary>
        public static void ShowWindow<T>() where T : Window, new()
        {
            currentWindow?.Close();
            currentWindow = new T();
            currentWindow.Show();
            currentWindow.Application = sender;
        }
    }
    /// <summary>
    /// Метод для выхода из приложения 
    /// (доступен из любого места программы)
    /// </summary>    
    public static void AppQuit()
    {
        app.Quit();
    }
    
}
