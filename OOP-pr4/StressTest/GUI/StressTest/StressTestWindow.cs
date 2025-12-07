namespace StressTest
{
    using System.Text;
    using Gtk;
    public class StressTestWindow : baseWindow
    {
        private ComboBoxText MaterialList;
        private ComboBoxText CrossSectionList;
        private ComboBoxText TestResultList;
        private TextView ResultDisplay;
        public StressTestWindow() : base()
        {
            // Arrange
            string[] MaterialValues = Enum.GetNames<Material>();
            string[] CrossSectionValues = Enum.GetNames<CrossSection>();
            string[] TestResultValues = Enum.GetNames<TestResult>();

            this.GetDefaultSize(out int WindowWidth, out int _);

            // Widgets
            MaterialList = WindowTools.AddComboBox(MaterialValues);
            Box MaterialBox = WindowTools.AddPrettyBox(Orientation.Horizontal, homogeneus: true);
            MaterialBox.Append(Label.New("Material: "));
            MaterialBox.Append(MaterialList);

            CrossSectionList = WindowTools.AddComboBox(CrossSectionValues);
            Box CrossSectionBox = WindowTools.AddPrettyBox(Orientation.Horizontal, homogeneus: true);
            CrossSectionBox.Append(Label.New("Cross-section: "));
            CrossSectionBox.Append(CrossSectionList);

            TestResultList = WindowTools.AddComboBox(TestResultValues);
            Box TestResultBox = WindowTools.AddPrettyBox(Orientation.Horizontal, homogeneus: true);
            TestResultBox.Append(Label.New("Test result: "));
            TestResultBox.Append(TestResultList);

            Label new_br() => Label.New(new String('_', WindowWidth / 7));;
            //Label br = Label.New(new String('_', WindowWidth / 7));

            Button TestingButton = WindowTools.AddButton("🔬   Test   🔨", OnTestingButtonClicked);

            ResultDisplay = WindowTools.AddDisplay();
            
            Button ClearButton = WindowTools.AddButton("∅ Очистить", (sender, e) => ResultDisplay.Buffer!.Text = "");

            // Main Box
            mainBox.Append(MaterialBox);
            mainBox.Append(CrossSectionBox);
            mainBox.Append(TestResultBox);
            //mainBox.Append(new_br());
            mainBox.Append(TestingButton);
            mainBox.Append(new_br());
            mainBox.Append(ResultDisplay);

            // Control Panel
            controlPanel.Append(ClearButton);
            controlPanel.Append(Label.New("Stress Test"));
            controlPanel.Append(closeButton);
        }

        private void OnTestingButtonClicked(object sender, EventArgs e)
        {
            string material = MaterialList.GetActiveText()!;
            string beauty_material = "";
            bool first = true;
            foreach(var c in material) 
            {
                if (Char.IsUpper(c) && !first) beauty_material += " ";
                first = false;
                beauty_material += c;
            }

            string crossSection = CrossSectionList.GetActiveText()!;
            string beauty_crossSection = "";
            first = true;
            foreach(var c in crossSection) 
            {
                if (Char.IsUpper(c) && !first) beauty_crossSection += "-";
                first = false;
                beauty_crossSection += c;
            }

            string testResult = TestResultList.GetActiveText()!;

            string materialString = $"Material: {beauty_material}";
            string crossSectionString = $"Cross-section: {beauty_crossSection}";
            string testResultStrring = $"Result: {testResult}";

            StringBuilder testDetails = new();
            testDetails.AppendJoin(", ", materialString, crossSectionString, testResultStrring);

            ResultDisplay.Buffer!.Text = testDetails.ToString();
        }
    }
}