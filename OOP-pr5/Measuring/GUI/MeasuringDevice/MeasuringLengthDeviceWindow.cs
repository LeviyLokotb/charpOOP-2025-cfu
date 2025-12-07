using MeasuringDevice;
using Gtk;

public class MeasuringLengthDeviceWindow : baseWindow
{
    private ComboBoxText unitCombo;
    private ComboBoxText typeCombo;
    private Button createDeviceButton;
    private Button startButton;
    private Button stopButton;
    private TextView lastMeasureDisplay;
    private Button metricButton;
    private Button imperialButton;
    private TextView valueDisplay;
    private TextView rawValuesDisplay;
    private Button rawValuesButton;
    private Label deviceStatusLabel = Label.New("Device: ⭕");

    public MeasuringLengthDeviceWindow() : base()
    {
        // Widgets
        Frame configFrame = new();
        Box configBox = WindowTools.AddPrettyBox();
        typeCombo = WindowTools.AddComboBox(["📏 Length", "🪨 Mass"]);
        unitCombo = WindowTools.AddComboBox(["🔬 Metric", "🎩 Imperial"]);
        createDeviceButton = WindowTools.AddButton("Create Measure Length Device", OnCreateDeviceButtonActivate);
        
        configBox.Append(Label.New("Тип устройства:"));
        configBox.Append(typeCombo);
        configBox.Append(Label.New("Единицы измерения:"));
        configBox.Append(unitCombo);
        configBox.Append(createDeviceButton);
        configBox.Append(deviceStatusLabel);
        configFrame.Child = configBox;

        Frame startingFrame = new();
        Box startingBox = WindowTools.AddPrettyBox();
        Box startingButtonsBox = WindowTools.AddPrettyBox(Orientation.Horizontal, true, 0, 20);
        startButton = WindowTools.AddButton("🚩 Start collecting", OnStartButtonActivate);
        stopButton = WindowTools.AddButton("🏁 Stop collecting", OnStopButtonActivate);
        lastMeasureDisplay = WindowTools.AddDisplay();

        startingButtonsBox.Append(startButton);
        startingButtonsBox.Append(stopButton);
        startingBox.Append(startingButtonsBox);
        startingBox.Append(lastMeasureDisplay);
        startingFrame.Child = startingBox;

        Frame getValueFrame = new();
        Box getValueBox = WindowTools.AddPrettyBox();
        Box getValueButtonsBox = WindowTools.AddPrettyBox(Orientation.Horizontal, true, 0, 20);
        metricButton = WindowTools.AddButton("🔬 Metric value", OnMetricButtonActivate);
        imperialButton = WindowTools.AddButton("🎩 Imperial value", OnImperialButtonActivate);
        valueDisplay = WindowTools.AddDisplay();

        getValueButtonsBox.Append(metricButton);
        getValueButtonsBox.Append(imperialButton);
        getValueBox.Append(getValueButtonsBox);
        getValueBox.Append(valueDisplay);
        getValueFrame.Child = getValueBox;

        Frame rawValueFrame = new();
        Box rawValueBox = WindowTools.AddPrettyBox();
        rawValuesButton = WindowTools.AddButton("Получить данные", OnRawValueButtonActivate);
        rawValuesDisplay = WindowTools.AddDisplay(10);

        rawValueBox.Append(rawValuesButton);
        rawValueBox.Append(rawValuesDisplay);
        rawValueFrame.Child = rawValueBox;

        // mainBox
        mainBox.Append(Label.New("⚙️ Конфигурация"));
        mainBox.Append(configFrame);
        mainBox.Append(Label.New("🧰 Управление сбором данных"));
        mainBox.Append(startingFrame);
        mainBox.Append(Label.New("📝 Получение значений"));
        mainBox.Append(getValueFrame);
        mainBox.Append(Label.New("🥩 Необработанные данные"));
        mainBox.Append(rawValueFrame);
        
        // controlPanel
        controlPanel.Append(Label.New("Measuring"));
        controlPanel.Append(closeButton);
    }
    MeasureDataDevice? device = null;
    // Создание устройства c заданными единицами измерения
    private void OnCreateDeviceButtonActivate(object sender, EventArgs e)
    {
        string? unitS = unitCombo.GetActiveText();
        Units units = unitS switch
        {
            "🔬 Metric" => Units.Metric,
            "🎩 Imperial" => Units.Imperial,
            _ => throw new NotImplementedException(), // Этого не будет, но на всякий
        };

        string? typeS = typeCombo.GetActiveText();
        device = typeS switch
        {
            "📏 Length" => new MeasuringLengthDevice(units),
            "🪨 Mass" => new MeasuringMassDevice(units),
            _ => throw new NotImplementedException(), // Этого не будет, но на всякий
        };

        deviceStatusLabel.SetText($"Device: ✅ ({typeS}, {unitS})");
    }

    bool isCollectingActive = false;
    // Запуск сбора измерений
    private void OnStartButtonActivate(object sender, EventArgs e)
    {
        if (isCollectingActive || device == null) return;
        isCollectingActive = true;
        device?.StartCollecting();
        System.Threading.ThreadPool.QueueUserWorkItem((dummy) =>
        {
            while (isCollectingActive)
            {
                System.Threading.Thread.Sleep(500);
                var n = device?.NativeValue();
                lastMeasureDisplay.Buffer!.Text = n==null ? "--" : n.ToString();
                OnRawValueButtonActivate(null!, null!);
            }
        });
    }
    // Остановка сбора изменений
    private void OnStopButtonActivate(object sender, EventArgs e)
    {
        isCollectingActive = false;
        device?.StopCollecting();
    }

    // Получить данные в метрической системе
    private void OnMetricButtonActivate(object sender, EventArgs e)
    {
        valueDisplay.Buffer!.Text = device?.MetricValue().ToString();
    }
    // Получить данные в имперской системе
    private void OnImperialButtonActivate(object sender, EventArgs e)
    {
        valueDisplay.Buffer!.Text = device?.ImperialValue().ToString();
    }
    // Получить сырой список данных
    private void OnRawValueButtonActivate(object sender, EventArgs e)
    {
        rawValuesDisplay.Buffer!.Text = string.Join( '\n', device?.GetRawData() ?? [] );
    }
}
