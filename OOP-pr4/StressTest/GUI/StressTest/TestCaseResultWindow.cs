namespace StressTest
{
    using System.Text;
    using Gtk;
    public class TestCaseResultWindow : baseWindow
    {
        TestCaseResult[] Results = [];
        TextView PassCountDisplay;
        int PassCount = 0;
        TextView FailCountDisplay;
        int FailCount = 0;
        TextView ReasonsListDisplay;
        public TestCaseResultWindow() : base()
        {
            // Arrange


            // Widgets
            Button RunButton = WindowTools.AddButton("🚩   Run Tests   💥", OnRunButtonClicked);

            Box CountersBox = WindowTools.AddPrettyBox(Orientation.Horizontal, true);

            Box PassCounterBox = WindowTools.AddPrettyBox(Orientation.Vertical, true);
            PassCountDisplay = WindowTools.AddDisplay();
            PassCountDisplay.Buffer!.Text = "--";
            PassCounterBox.Append(Label.New("Passes"));
            PassCounterBox.Append(PassCountDisplay);

            Box FailCounterBox = WindowTools.AddPrettyBox(Orientation.Vertical, true);
            FailCountDisplay = WindowTools.AddDisplay();
            FailCountDisplay.Buffer!.Text = "--";
            FailCounterBox.Append(Label.New("Failures"));
            FailCounterBox.Append(FailCountDisplay);

            CountersBox.Append(PassCounterBox);
            CountersBox.Append(FailCounterBox);

            ReasonsListDisplay = WindowTools.AddDisplay(lines: 10);

            Button ClearButton = WindowTools.AddButton("∅ Очистить", OnClearButtonClicked);

            // Main Box
            mainBox.Append(RunButton);
            mainBox.Append(CountersBox);
            mainBox.Append(Label.New("Reasons for failures: "));
            mainBox.Append(ReasonsListDisplay);


            // Control Panel
            controlPanel.Append(ClearButton);
            controlPanel.Append(Label.New("Stress Test"));
            controlPanel.Append(closeButton);
        }

        private void OnRunButtonClicked(object sender, EventArgs e)
        {
            OnClearButtonClicked(null!, null!);

            for (int i=0; i<10; i++)
            {
                TestCaseResult cur_res = TestManager.GenerateResult();
                Results = [.. Results, cur_res];
                if (cur_res.Result == TestResult.Pass) PassCount++;
                else
                {
                    FailCount++;
                    ReasonsListDisplay.Buffer!.Text += $"Failure: {cur_res.ReasonForFailure}\n";
                }
                PassCountDisplay.Buffer!.Text = PassCount.ToString();
                FailCountDisplay.Buffer!.Text = FailCount.ToString();
            }
        }

        private void OnClearButtonClicked(object sender, EventArgs e)
        {
            Results = [];
            PassCountDisplay.Buffer!.Text = "--";
            FailCountDisplay.Buffer!.Text = "--";
            ReasonsListDisplay.Buffer!.Text = "";
            PassCount = 0;
            FailCount = 0;
        }
    }
}