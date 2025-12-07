using Gtk;
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
