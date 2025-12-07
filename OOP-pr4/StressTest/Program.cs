//#
using Gtk;
using StressTest;
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
    private static Application app = Application.New("com.leviylokotb.OOP-pr4", Gio.ApplicationFlags.FlagsNone);
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

    /// <summary>
    /// Метод для выхода из приложения 
    /// (доступен из любого места программы)
    /// </summary>    
    public static void AppQuit()
    {
        app.Quit();
    }
}
