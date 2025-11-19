using Gtk;
public class NuclearTerminalWindow : Window
{
    private Label displayLabel = Label.New("");
    private ScrolledWindow scrolledWindow;
    public NuclearTerminalWindow() : base()
    {
        SetDefaultSize(1000, 600);
        
        Box backgroundBox = new();
        backgroundBox.GetStyleContext().AddClass("background");

        Box mainBox = new();
        mainBox.SetOrientation(Orientation.Vertical);

        Grid contentBox = AddWarningStripes(mainBox);
        contentBox.SetOrientation(Orientation.Horizontal);
        //contentBox.Homogeneous = true;
        contentBox.ColumnHomogeneous = true;

        ///////// Главный box /////////
        (displayLabel, scrolledWindow) = AddTerminal(contentBox);

        _ = AddToDisplay("Connecting to nuclear reactor terminal...\n", delay: 2);
        _ = AddToDisplay("........\n", delay: 500);
        _ = AddToDisplay("Welcome!\n", delay: 2);

        AddActivateButton(contentBox);

        backgroundBox.Append(mainBox);
        SetChild(backgroundBox);
        ApplyCSS();
    }
    /// <summary>Создаёт чёрно-жёлтый узор в box</summary>
    /// <param name="box"></param>
    private void CreateStripedPattern(Box box, int? width0 = null)
    {
        width0 ??= DefaultWidth;
        int width = (int)width0;

        int correct_width = width;
        int blockLen = 25;
        int blocksCount = width / blockLen / 2;
        while (correct_width >= blockLen)
        {
            var blackStrip = new Box();
            blackStrip.SetOrientation(Orientation.Horizontal);
            blackStrip.SetSizeRequest(blockLen, -1);
            blackStrip.GetStyleContext().AddClass("warning-stripe-black");
            box.Append(blackStrip);

            correct_width -= blockLen;
            if(correct_width < blockLen) break;
            
            var yellowStrip = new Box();
            yellowStrip.SetOrientation(Orientation.Horizontal);
            yellowStrip.SetSizeRequest(blockLen, -1);
            yellowStrip.GetStyleContext().AddClass("warning-stripe-yellow");
            box.Append(yellowStrip);

            correct_width -= blockLen;
        }
    }
    /// <summary>Добавляет чёрно-жёлтые полосы сверху и снизу box</summary>
    /// <param name="box"></param>
    /// <returns>Внутренний (центральный) box</returns>
    private Grid AddWarningStripes(Box box, int height = 20, int? width = null)
    {
        // Верхние полосы
        
        var topStripes = new Box();
        topStripes.SetOrientation(Orientation.Horizontal);
        topStripes.HeightRequest = height;
        CreateStripedPattern(topStripes, width);
        //CreateDiagonalPattern(topStripes);
        box.Append(topStripes);
        
        // Добавляем какой-то контент между полосами
        var contentBox = new Grid();
        contentBox.SetOrientation(Orientation.Vertical);
        contentBox.HeightRequest = DefaultHeight - (2 * height);
        box.Append(contentBox);

        // Нижние полосы
        var bottomStripes = new Box();
        bottomStripes.SetOrientation(Orientation.Horizontal);
        bottomStripes.HeightRequest = height;
        CreateStripedPattern(bottomStripes, width);
        //CreateDiagonalPattern(bottomStripes);
        box.Append(bottomStripes);

        return contentBox;
    }
    /// <summary>Добавляет "терминал"</summary>
    /// <param name="box"></param>
    /// <returns>основной Label с текстом</returns>
    private (Label, ScrolledWindow) AddTerminal(Grid box)
    {
        Frame terminalFrame = new();
        WindowTools.SetMargin(terminalFrame, 20);
        terminalFrame.GetStyleContext().AddClass("terminal-frame");

        Box terminalBox = new();
        //terminalBox.Homogeneous = true;
        terminalBox.SetOrientation(Orientation.Vertical);
        WindowTools.SetMargin(terminalBox, 20);

        var header = Label.New("Reactor control terminal");
        header.GetStyleContext().AddClass("terminal-header");

        Box displayBox = new();
        displayBox.HeightRequest = box.HeightRequest;
        displayBox.Homogeneous = true;
        Frame displayFrame = new();
        displayFrame.GetStyleContext().AddClass("display-frame");

        Label displayLabel = new();
        displayLabel.Halign = Align.Start;
        displayLabel.Valign = Align.Start;
        displayLabel.UseMarkup = true;
        displayLabel.GetStyleContext().AddClass("display-text-deactivate");

        ScrolledWindow scrolled = new();
        //scrolled.SetSizeRequest(300, 300);
        scrolled.Child = displayLabel;

        displayFrame.Child = scrolled;

        terminalBox.Append(header);
        displayBox.Append(displayFrame);
        terminalBox.Append(displayBox);
        terminalFrame.Child = terminalBox;
        
        //box.Append(terminalFrame);
        box.Attach(terminalFrame, 0, 0, 3, 1);
        return (displayLabel, scrolled);
    }
    private void AddActivateButton(Grid box)
    {
        Box buttonBox = new();
        buttonBox.Valign = Align.BaselineCenter;
        buttonBox.Halign = Align.BaselineCenter;

        Box buttonFrame = new();
        buttonFrame.SetOrientation(Orientation.Vertical);

        Grid buttonCenter = AddWarningStripes(buttonFrame, 10, 125);
        buttonCenter.HeightRequest = 80;
        buttonCenter.RowHomogeneous = true;
        buttonCenter.ColumnHomogeneous = true;
        buttonCenter.GetStyleContext().AddClass("warning-button");

        Button activateButton = WindowTools.AddButton("Deactivate", OnDeactivateButtonClicked);
        activateButton.GetStyleContext().AddClass("red-button");

        //buttonBox.Append(activateButton);
        buttonCenter.Attach(activateButton, 0, 0, 1, 1);

        //buttonCenter.RowHomogeneous = true;
        //buttonCenter.ColumnHomogeneous = true;

        buttonBox.Append(buttonFrame);
        //box.Append(buttonBox);
        box.Attach(buttonBox, 3, 0, 1, 1);
    }
    /// <summary>Обработчик нажатия кнопки</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnDeactivateButtonClicked(object sender, EventArgs e)
    {
        _ = UpdateDisplay("");
        SafeFailure reactorDisabler = new( async (text) => { await AddToDisplay($"[{DateTime.Now:HH:mm:ss:fff}] {text}\n", delay: 2); } );
        reactorDisabler.PerformShutdown();
        //await AddToDisplay(content);
        WindowTools.ScrollToEnd(scrolledWindow);
    }

    bool isLocked = false;
    List<(string, int)> stackForDisplay = [];
    public async Task UpdateDisplay(string text, int delay = 10)
    {
        isLocked = true;
        if (displayLabel.GetText() == "" && text == "") displayLabel.GetStyleContext().AddClass("display-text-deactivate");
        else displayLabel.GetStyleContext().AddClass("display-text-activate");
        string tmp = "";
        displayLabel.SetMarkup(tmp);

        foreach (var let in text)
        {
            await Task.Delay(delay);
            tmp += let;
            displayLabel.SetMarkup(tmp);
            await Task.Yield();
            WindowTools.ScrollToEnd(scrolledWindow);
        }
        WindowTools.ScrollToEnd(scrolledWindow);
        isLocked = false;
    }

    public async Task AddToDisplay(string text, int delay=10)
    {
        if (isLocked) {
            stackForDisplay = stackForDisplay.Append((text, delay)).ToList();
            return;
        }
        isLocked = true;
        if (displayLabel.GetText() == "" && text == "") displayLabel.GetStyleContext().AddClass("display-text-deactivate");
        else displayLabel.GetStyleContext().AddClass("display-text-activate");
        string tmp = displayLabel.GetText();

        foreach (var let in text)
        {
            await Task.Delay(delay);
            tmp += let;
            displayLabel.SetMarkup(tmp);
            await Task.Yield();
            WindowTools.ScrollToEnd(scrolledWindow);
        }
        WindowTools.ScrollToEnd(scrolledWindow);
        isLocked = false;
        if (stackForDisplay.Count != 0) {
            var (t, d) = stackForDisplay[0];
            stackForDisplay.Remove((t, d));
            await AddToDisplay(t, d);
        }
    }

    /// <summary>Применение CSS стилей к текущему экрану</summary>
    private void ApplyCSS()
    {
        var cssProvider = new CssProvider();
        var css = @"
            .warning-stripe-black { background: black; }
            .warning-stripe-yellow { background: #f1b308ff; }

            .terminal-frame {
                border: 3px solid #444;
                border-radius: 8px;
                background: #444;
            }

            .terminal-header {
                color: #222;
                font-weight: bold;
                font-size: 14px;
                font-family: Monospace;
            }
            
            .display-frame {
                background: #140f0fff;
                border: 2px solid #222;
                color: #ff6666;
            }
            
            .display-text-deactivate {
                color: #ff6666;
                background: black;
                padding: 10px;
                font-family: Monospace;
            }

            .display-text-activate {
                color: #ff6666;
                background: #140f0fff;
                padding: 10px;
                font-family: Monospace;
            }

            .red-button {
                background: #e20b0bff;
                color: #490d0dff;
                border: 2px solid #490d0dff;
                border-radius: 60px;
                font-family: Monospace;
            }

            .red-button:hover {
                background: #490d0dff;
                color: #e20b0bff;
            }

            .background {
                background: #333333ff;
            }

            .warning-button {
                background: #444;
            }
        ";

        cssProvider.LoadFromData(css, css.Length);
        //! Possible exception -- null
        StyleContext.AddProviderForDisplay(Gdk.Display.GetDefault()!, cssProvider, 800);
    }

}