
using Gtk;
public static class ApplyCss
{
    /// <summary>Применение CSS стилей к текущему экрану</summary>
    public static void ApplyCSS(string css)
    {
        var cssProvider = new CssProvider();
        cssProvider.LoadFromData(css, css.Length);
        //! Possible exception -- null
        StyleContext.AddProviderForDisplay(Gdk.Display.GetDefault()!, cssProvider, 800);
    }
}