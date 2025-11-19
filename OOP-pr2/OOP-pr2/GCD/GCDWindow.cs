using System.IO.Pipelines;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using Gtk;
using static WindowTools;

/// <summary>
/// Окно для демонстрации вычислений НОД
/// </summary>
public class GCDWindow : baseWindow
{
    public GCDWindow() : base()
    {

        mainBox.Append(Label.New("Введите 2 числа:"));

        // Поле ввода числа 1
        Box box1 = new()
        {
            Homogeneous = true
        };
        mainBox.Append(box1);
        ConfigureBox(box1, Orientation.Horizontal);
        box1.Append(Label.New("Число 1:"));
        var entry1 = AddDisplay(box1);
        AddValidation(entry1, "1234567890");

        // Поле ввода числа 2
        Box box2 = new()
        {
            Homogeneous = true
        };
        mainBox.Append(box2);
        ConfigureBox(box2, Orientation.Horizontal);

        box2.Append(Label.New("Число 2:"));
        var entry2 = AddDisplay(box2);
        AddValidation(entry2, "1234567890");

        // Рассчёт результата
        var result12 = AddDisplay(3);
        AddButton("Вычислить:", mainBox, (sender, e) =>
        {
            (string s1, string s2) = (entry1.Buffer!.Text ?? "0", entry2.Buffer!.Text ?? "0");
            s1 = (s1 == "" ? "0" : s1);
            entry1.Buffer!.Text = s1;
            s2 = (s2 == "" ? "0" : s2);
            entry2.Buffer!.Text = s2;
            // Ввод уже валидирован, можем не бояться
            (int i1, int i2) = (int.Parse(s1), int.Parse(s2));

            (int resultEuclid, string errorEuclid, long timeEuclid) = GCDalg.TimerThis<int>(GCDalg.GCD, i1, i2);
            (int resultStein, string errorStein, long timeStein) = GCDalg.TimerThis<int>(GCDalg.GCDStein, i1, i2);
            result12.Buffer!.Text = $"Эвклид: {(errorEuclid == "" ? resultEuclid : errorEuclid)} ({timeEuclid} тиков)\n";
            result12.Buffer!.Text += $"Штейн:  {(errorStein == "" ? resultStein : errorStein)} ({timeStein} тиков)\n";
            result12.Buffer!.Text += $"Разница: {timeEuclid - timeStein} тиков (примерно в {Math.Round((double)(timeEuclid / timeStein))} раз)";
        });

        mainBox.Append(Label.New("Результат:"));
        mainBox.Append(result12);

        mainBox.Append(new Box());
        // Ввод N параметров
        mainBox.Append(Label.New("или несколько чисел через пробел:"));
        var entry3 = AddDisplay(mainBox);
        AddValidation(entry3, "123456789 0");

        var result3 = AddDisplay(3);
        AddButton("Вычислить:", mainBox, (sender, e) =>
        {
            string s3 = entry3.Buffer!.Text ?? "";
            (int resultEuclid, string errorEuclid, long timeEuclid) = GCDalg.TimerThis<string>(GCDalg.GCD, s3);
            (int resultStein, string errorStein, long timeStein) = GCDalg.TimerThis<string>(GCDalg.GCDStein, s3);
            result3.Buffer!.Text = $"Эвклид: {(errorEuclid == "" ? resultEuclid : errorEuclid)} ({timeEuclid} тиков)\n";
            result3.Buffer!.Text += $"Штейн:  {(errorStein == "" ? resultStein : errorStein)} ({timeStein} тиков)\n";
            result3.Buffer!.Text += $"Разница: {timeEuclid - timeStein} тиков (примерно в {Math.Round((double)(timeEuclid / timeStein))} раз)";          
        });

        mainBox.Append(Label.New("Результат:"));
        mainBox.Append(result3);


        // Панель управления
        AddButton("? Пример", controlPanel, (sender, e) =>
        {
            entry1.Buffer!.Text = "2806";
            entry2.Buffer!.Text = "345";
            result12.Buffer!.Text = "Попробуйте. Правильный ответ: 23";
            entry3.Buffer!.Text = "24 36 48 60";
            result3.Buffer!.Text = "Попробуйте. Правильный ответ: 12";
        });
        controlPanel.Append(Label.New("Вычисление НОД"));
        controlPanel.Append(closeButton);
    }
}