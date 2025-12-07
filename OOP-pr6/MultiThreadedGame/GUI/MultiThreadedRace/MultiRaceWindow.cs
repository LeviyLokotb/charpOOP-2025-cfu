using Gtk;

namespace MultiThreadedRace
{
    public abstract class MultiRaceWindow<T> : baseWindow where T : MovableObject
    {
        protected Button startButton;
        protected Button resetButton;
        protected Fixed RaceArea;
        protected TextView winnersDisplay;
        //protected TextView threadMonitoringDisplay;
        protected GUIUpdater RaceUpdater;
        //protected ThreadsMonitoring ThreadsAnalizer;
        protected Label RaceStateLabel = Label.New("Race: Ready");
        protected List<T> Cars = [];
        protected List<Widget> CarsLabels = [];
        protected const int RaceWidth = 820;
        protected const int RaceHeigth = 300;
        protected List<T> Winners = [];
        protected GameTime? GameTimer;
        protected GameTime? OtherTimer;
        protected Barrier FinishBarrier;
        protected Semaphore MovingSemaphore;
        
        protected MultiRaceWindow() : base()
        {
            // Init utility things
            GameTimer = new GameTime();
            RaceUpdater = new GUIUpdater(GameTimer);

            OtherTimer = new GameTime(20);

            // Widgets
            Frame RaceFrame = new();
            Box TopBox = WindowTools.AddPrettyBox(Orientation.Vertical, homogeneus: false, 5, 5);
            Box TopButtonsBox = WindowTools.AddPrettyBox(Orientation.Horizontal, homogeneus: true);
            startButton = WindowTools.AddButton("Start", OnStartButtonActivate);
            resetButton = WindowTools.AddButton("Reset", OnResetButtonActivate);

            TopButtonsBox.Append(startButton);
            TopButtonsBox.Append(resetButton);

            //RaceArea = Fixed.New();
            RaceArea = new Fixed();
            RaceArea.SetSizeRequest(RaceWidth, RaceHeigth);

            var RaceBox = WindowTools.AddPrettyBox(homogeneus:true, margins:5);
            RaceBox.Append(RaceArea);

            Frame RaceAreaFrame = new();
            RaceAreaFrame.Child = RaceBox;

            // Add objects on RaceArea
            var finishLine = new Frame();
            finishLine.SetSizeRequest(25, RaceHeigth-10);

            var inFinishBox = WindowTools.AddPrettyBox(homogeneus: true, margins: 0, spaces: 0);
            for (int i=0; i<=((RaceHeigth-10) / 30 * 2); i++)
            {
                if ((i % 2) == 0) inFinishBox.Append(Label.New("⬜⬛"));
                else inFinishBox.Append(Label.New("⬛⬜"));
            }
            finishLine.Child = inFinishBox;

            RaceArea.Put(finishLine, RaceWidth-35, 0);

            CreateCars();

            TopBox.Append(TopButtonsBox);
            TopBox.Append(RaceAreaFrame);
            TopBox.Append(RaceStateLabel);
            RaceFrame.Child = TopBox;


            Frame WinnersFrame = new();
            Box WinnersBox = WindowTools.AddPrettyBox(Orientation.Vertical, homogeneus: true);
            winnersDisplay = WindowTools.AddDisplay( Cars.Count()*2 );

            WinnersBox.Append(winnersDisplay);
            WinnersFrame.Child = WinnersBox;

            // Configure utility things
            FinishBarrier = new Barrier(Cars.Count(), OnAllCarsFinished );
            // Пусть двигаются одновременно только 2
            MovingSemaphore = new Semaphore(2, 2);

            // mainBox
            //mainBox.Append(TopButtonsBox);
            mainBox.Append(RaceFrame);

            mainBox.Append(Label.New("🏆 Порядок прибытия:"));
            mainBox.Append(WinnersFrame);

            // Close Window Event
            WindowClosed += (o, e) => OnResetButtonActivate(null!, null!);
        }

        // Начало
        protected void OnStartButtonActivate(object sender, EventArgs e)
        {
            OnResetButtonActivate(null!, null!);

            GameTimer?.StartTime();
            OtherTimer?.StartTime();

            if (winnersDisplay.Buffer == null) return;
            winnersDisplay.Buffer.Text = "";

            RaceStateLabel.SetText("Race: Started");
        }

        // Некто финишировал
        protected void OnFinished(object? sender, EventArgs e)
        {
            // Отчитываемся баръеру
            FinishBarrier.SignalAndWait(20);

            if (winnersDisplay.Buffer == null || sender == null) return;

            T? winner = sender as T;
            if (winner == null) return;

            winner.IsFinished = true;

            int n;
            lock (Winners)
            {    
                Winners.Add(winner);
                n = Winners.Count();
            }

            GLib.Functions.IdleAdd(0, () =>
            {
                lock(winnersDisplay.Buffer) 
                { 
                    string text = winnersDisplay.Buffer.Text ?? "";
                    winnersDisplay.Buffer.Text = text + $"{n}. {winner}\n\n";
                }
                return false;
            });

        }

        protected void OnAllCarsFinished(Barrier? b)
        {
            RaceStateLabel.SetText("Race: Finished");
            GameTimer?.StopTime();
            OtherTimer?.StopTime();
        }

        // Сброс
        protected void OnResetButtonActivate(object sender, EventArgs e)
        {
            Winners = [];
            foreach(T car in Cars) 
            { 
                car.IsFinished = true;
            }
            CreateCars();
            GameTimer?.StopTime();
            OtherTimer?.StopTime();

            RaceStateLabel.SetText("Race: Ready");
        }

        protected abstract void CreateCars();
    }
}