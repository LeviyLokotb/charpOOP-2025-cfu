using Gtk;

public class Program
{
    private static readonly Application app = Application.New("com.leviylokotb.nuclearterminal", Gio.ApplicationFlags.FlagsNone);
    static void Main(string[] args)
    {
        StartGTK(args);
    }

    static void StartGTK(string[] args)
    {
        app.OnActivate += (sender, e) =>
        {
            NuclearTerminalWindow window = new();
            window.Show();
            window.Application = app;
        };
        app.RunWithSynchronizationContext(args);
    }

}
