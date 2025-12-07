using Gtk;

namespace MultiThreadedRace
{
    public class MultiThreadedRaceWindow : MultiRaceWindow<ThreadMovableObject>
    {
        private TextView threadMonitoringDisplay;
        private ThreadsMonitoring ThreadsAnalizer;

        public MultiThreadedRaceWindow() : base()
        {
            var ThreadMonitorTextUpdater = new GUIUpdater(GameTimer);
            ThreadsAnalizer = new ThreadsMonitoring(OtherTimer);

            // 
            Frame ThreadMonitoringFrame = new();
            Box ThreadMonitoringBox = WindowTools.AddPrettyBox(Orientation.Vertical, homogeneus: true);
            threadMonitoringDisplay = WindowTools.AddDisplay( Cars.Count()*2 );

            ThreadMonitoringBox.Append(threadMonitoringDisplay);
            ThreadMonitoringFrame.Child = ThreadMonitoringBox;

            ThreadsAnalizer.AddThreads( from car in Cars where car.Thread != null select car.Thread );
            ThreadsAnalizer.ThreadsInfoUpdate += (threadsInfo) =>
            {
                GLib.Functions.IdleAdd(0, () =>
                {
                    bool locked1 = false;
                    try
                    {
                        Monitor.Enter(threadMonitoringDisplay, ref locked1);

                        if (threadMonitoringDisplay.Buffer == null) return false;
                        threadMonitoringDisplay.Buffer.Text = "";
                        foreach (var info in threadsInfo)
                        {
                            string res = $"Thread: {info.Name}, Priority: {info.Priority}, State: {info.State}\n\n";
                            //Console.WriteLine(res);
                            threadMonitoringDisplay.Buffer.Text += res;
                        }
                    }
                    finally
                    {
                        if (locked1) Monitor.Exit(threadMonitoringDisplay);
                    }

                    lock(ThreadsAnalizer)
                    {   
                        ThreadsAnalizer.Clear();
                        ThreadsAnalizer.AddThreads( Cars.Select(car => car.Thread ) );
                    }
                    return false;
                });
            };

            // Buttons
            startButton.OnClicked += (o, e) => 
            {
                foreach(var car in Cars) {
                    car.Finished += ownOnFinished;
                    car.Finished += OnFinished;
                    car.StartRace();
                }
            };
            resetButton.OnClicked += (o, e) => 
            {
                ThreadsAnalizer.Clear();
                foreach(var car in Cars) {
                    car.Finished -= ownOnFinished;
                }
                foreach(var car in Cars) car.Thread?.Join(100);
            };

            // mainBox
            mainBox.Append(Label.New("📊 Мониторинг потоков:"));
            mainBox.Append(ThreadMonitoringFrame);

            // controlPanel
            controlPanel.Append(Label.New("⏳ Многопоточная гонка 🏁"));
            controlPanel.Append(closeButton);
        }

        private void ownOnFinished(object? sender, EventArgs e)
        {
            ThreadMovableObject? winner = sender as ThreadMovableObject;
            if (winner == null) return;
            winner.Thread?.Join(100);
        }

        protected override void CreateCars()
        {
            foreach(var w in CarsLabels) RaceArea.Remove(w);
            
            Cars = [];
            string[] Icons = ["🧌", "🗡️", "🔮", "🌹", "👑"];
            ThreadPriority[] Priorities = [
                ThreadPriority.Lowest, 
                ThreadPriority.AboveNormal, 
                ThreadPriority.Normal, 
                ThreadPriority.BelowNormal, 
                ThreadPriority.Highest
            ];
            for (int i=0; i<5; i++)
            {
                int heigth = RaceHeigth/(Icons.Length+1) * (i+1);
                Label widget = Label.New(Icons[i]);

                ThreadMovableObject car = new(
                        Icons[i],
                        Priorities[i], 
                        (x, y) => RaceArea.Put(widget, x, y),
                        (x, y) => {
                            lock(RaceUpdater) RaceUpdater.Add(() => RaceArea.Move(widget, x, y));
                            //GUIMutexUpdater.DoWithMutex( () => RaceArea.Move(widget, x, y) );
                        },
                        MovingSemaphore,
                        speed: 5, RaceWidth,
                        0, heigth 
                );

                CarsLabels.Add( widget );
                Cars.Add( car );
                
            }
        }
    }
}