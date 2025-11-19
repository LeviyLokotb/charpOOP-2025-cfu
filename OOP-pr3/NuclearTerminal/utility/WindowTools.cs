using System.ComponentModel;
using Gdk;
using GLib;
using Gtk;

public static class WindowTools
{
    /// <summary>Создаёт обычную кнопку</summary>
    /// <param name="label">Надпись на кнопке</param>
    /// <param name="onClicked">Событие при нажатии</param>
    public static Button AddButton(string label, Action<Button, EventArgs> onClicked)
    {
        Button myButton = new Button();
        myButton.SetLabel(label);
        myButton.OnClicked += (sender, e) =>
        {
            onClicked(sender, e);
        };
        return myButton;
    }
    /// <summary>Создаёт обычную кнопку и добавляет её в контейнер</summary>
    /// <param name="label">Надпись на кнопке</param>
    /// <param name="box"></param>
    /// <param name="onClicked">Событие при нажатии</param>
    public static void AddButton(string label, Box box, Action<Button, EventArgs> onClicked) => box.Append(AddButton(label, onClicked));
    /*
    public static Button AddNavigationButton<T>(string label) where T : Window, new()
    {
        Button myButton = AddButton(label, (sender, e) =>
        {
            Program.WindowManager.ShowWindow<T>();
        });
        return myButton;
    }
    
    public static void AddNavigationButton<T>(string label, Box box) where T : Window, new() => box.Append(AddNavigationButton<T>(label));
    */
    public static void ConfigureBox(Box box, Orientation orientation = Orientation.Vertical)
    {
        box.SetOrientation(orientation);
        box.SetMarginTop(20);
        box.SetMarginBottom(20);
        box.SetMarginStart(20);
        box.SetMarginEnd(20);
        box.SetSpacing(20);
    }

    /// <summary>
    /// Создаёт обычное поле ввода
    /// </summary>
    /// <returns></returns>
    public static TextView AddEntry(int lines=1)
    {
        // Текстовое поле
        var entry = new TextView()
        {
            CursorVisible = true,
            Editable = true,
            Monospace = true,
            MarginStart = 10,
            MarginEnd = 10,
            HeightRequest = 20*(lines+1),
            LeftMargin = 10,
            RightMargin = 10,
            TopMargin = 10,
        };
        return entry;
    }

    /// <summary>
    /// Создаёт обычное поле ввода и добавляет его в контейнер
    /// </summary>
    /// <param name="box"></param>
    public static TextView AddEntry(Box box, int lines=1)
    {
        var entry = AddEntry(lines);
        box.Append(entry);
        return entry;
    }
    /// <summary>
    /// Создаёт неизменяемое текстовое поле
    /// </summary>
    /// <returns></returns>
    public static TextView AddDisplay(int lines = 1)
    {
        // Текстовое поле
        var entry = new TextView()
        {
            CursorVisible = true,
            Editable = false,
            Monospace = true,
            MarginStart = 10,
            MarginEnd = 10,
            HeightRequest = 20*(lines+1),
            LeftMargin = 10,
            RightMargin = 10,
            TopMargin = 10,
        };
        return entry;
    }
    /// <summary>
    /// Создаёт неизменяемое текстовое поле и добавляет его в контейнер
    /// </summary>
    /// <param name="box"></param>
    public static TextView AddDisplay(Box box, int lines = 1)
    {
        var display = AddDisplay(lines);
        box.Append(display);
        return display;
    }
    /// <summary>
    /// Добавляет простую вадидацию ввода к неизменяемому текстовуму полю
    /// </summary>
    /// <param name="entry">Текстовое поле</param>
    /// <param name="alowed">Строка с разрешёнными символами</param>
    public static void AddValidation(TextView entry, string allowed)
    {
        var keyController = EventControllerKey.New();
        keyController.OnKeyPressed += CreateValidationController(entry, allowed);
        entry.AddController(keyController);
    }

    public static SpinButton AddSpinButton(int default_, int lower, int upper)
    {
        Adjustment adjustment1 = Adjustment.New(default_, lower, upper, 1, 2, 0);
        SpinButton button = SpinButton.New(adjustment1, 1.0, 0);
        button.Wrap = true;
        return button;
    }
    public static SpinButton AddSpinButton(Box box, int default_, int lower, int upper)
    {
        SpinButton button = AddSpinButton(default_, lower, upper);
        box.Append(button);
        return button;
    }

    public static GObject.ReturningSignalHandler<EventControllerKey, EventControllerKey.KeyPressedSignalArgs, bool> CreateValidationController(TextView entry, string allowed)
    {
        return (sender, e) =>
        {
            TextBuffer buff = entry.Buffer!;
            buff.Text ??= "0";
            if (e.Keycode == 37 || e.Keycode == 22)
            {
                if (buff.Text.Length <= 0) return true;
                buff.Text = buff.Text![0..^1];
                return true;
            }
            if (e.Keycode == 119)
            {
                buff.Text = "";
                return true;
            }
            //Console.WriteLine($"{e.Keycode}");

            char symb = (char)e.Keyval;
            if (!allowed.Contains(symb)) return true;
            buff.Text += symb;
            return true;
        };
    }

    public static void ReplaceChild(Box box, int index, Widget newWiget)
    {
        Widget[] childs = GetChilds(box);
        //foreach (var c in childs) Console.WriteLine(c);
        Widget old = childs[index];
        if (old != null)
        {
            box.Remove(old);
            Widget? prev = childs[index - 1];
            if (prev != null) box.InsertChildAfter(newWiget, prev);
        }
    }
    
    public static Widget[] GetChilds(Box box)
    {
        Widget[] childs = [];
        var child = box.GetFirstChild();
        while (child != null)
        {
            childs = [.. childs, child];
            child = child!.GetNextSibling();
        }
        //foreach (var c in childs) Console.WriteLine(c);
        return childs;
    }

    public enum Side
    {
        Start,
        End,
        Top,
        Bottom,
    }

    public static void SetMargin(object obj, int margin, params Side[] sides)
    {
        Side[] default_ = [Side.Start, Side.End, Side.Top, Side.Bottom];
        sides ??= default_;
        if (sides.Length == 0) sides = default_;

        // Устанавливаем значение параметров с помощью рефлексии, если они есть
        Type type = obj.GetType();
        foreach(Side side in sides)
        {
            switch (side)
            {
                case Side.Start:  type.GetProperty("MarginStart")?.SetValue(obj, margin); break;
                case Side.End:    type.GetProperty("MarginEnd")?.SetValue(obj, margin); break;
                case Side.Top:    type.GetProperty("MarginTop")?.SetValue(obj, margin); break;
                case Side.Bottom: type.GetProperty("MarginBottom")?.SetValue(obj, margin); break;
                default: Console.WriteLine(" [w] Margin не найден"); break;
            }
        }
    }

    public static void ScrollToEnd(ScrolledWindow scroll)
    {
        var adjustment = scroll.Vadjustment;
        if (adjustment != null) adjustment.Value = adjustment.Upper - adjustment.PageSize;
    }
}
