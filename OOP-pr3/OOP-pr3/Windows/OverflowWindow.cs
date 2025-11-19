using Gtk;

class OverflowWindow : baseWindow
{
    private TextView NumEntry1;
    private TextView NumEntry2;
    private TextView NumEntryResult;
    public OverflowWindow() : base()
    {
        // Создание виджетов
        NumEntry1 = WindowTools.AddDisplay();
        WindowTools.AddValidation(NumEntry1, "-0123456789");
        NumEntry1.Buffer!.Text = "2147483647";

        NumEntry2 = WindowTools.AddDisplay();
        WindowTools.AddValidation(NumEntry2, "-0123456789");
        NumEntry2.Buffer!.Text = "2";

        NumEntryResult = WindowTools.AddDisplay();

        Button multipyButton = WindowTools.AddButton( "=", OnMultipyButtonClicked);

        // mainBox
        mainBox.Append(NumEntry1);
        mainBox.Append(Label.New("x"));
        mainBox.Append(NumEntry2);
        mainBox.Append(multipyButton);
        mainBox.Append(NumEntryResult);

        // controlBox
        WindowTools.AddButton("↻ Default", controlPanel, (sender, e) =>
        {
            NumEntry1.Buffer!.Text = "2147483647";
            NumEntry2.Buffer!.Text = "2";
            // Да, это работает
            OnMultipyButtonClicked(null!, null!);
        });
        controlPanel.Append(Label.New("Переполнение типа при умножении"));
        controlPanel.Append(exitButton);
    }

    private void OnMultipyButtonClicked(object sender, EventArgs e)
    {
            if ( !int.TryParse(NumEntry1.Buffer!.Text, out int num1))
            {
                NumEntryResult.Buffer!.Text = "An invalid input in 1st number";
                return;
            }
            if ( !int.TryParse(NumEntry2.Buffer!.Text, out int num2))
            {
                NumEntryResult.Buffer!.Text = "An invalid input in 2nd number";
                return;
            }

            try
            {
                checked
                {
                    int numResult = num1 * num2;
                    NumEntryResult.Buffer!.Text = $"{numResult}";
                }
            }
            catch (OverflowException exc)
            {
                NumEntryResult.Buffer!.Text = $"Result is Overflowed: {exc.Message}";
            }
        }
}