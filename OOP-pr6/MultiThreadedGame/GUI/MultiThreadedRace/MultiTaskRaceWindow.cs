
using Gtk;

namespace MultiThreadedRace
{
    public class MultiTaskRaceWindow : MultiRaceWindow<TaskMovableObject>
    {
        public MultiTaskRaceWindow() : base()
        {
            startButton.OnClicked += (o, e) =>
            {
                List<Task> tasks = []; 
                foreach(var car in Cars) {
                    car.Finished += OnFinished;
                    car.StartRace();
                    if (car.MoveTask != null) tasks.Add(car.MoveTask);
                }
                Console.WriteLine("Meeeeeooooowwww^^~");
                Task.WhenAll( tasks );
            };

            // controlPanel
            controlPanel.Append(Label.New("⏳ Асинхронная гонка 🏁"));
            controlPanel.Append(closeButton);
        }

        protected override void CreateCars()
        {
            foreach(var w in CarsLabels) RaceArea.Remove(w);
            
            Cars = [];
            string[] Icons = ["🧌", "🗡️", "🔮", "🌹", "👑"];
            for (int i=0; i<5; i++)
            {
                int heigth = RaceHeigth/(Icons.Length+1) * (i+1);
                Label widget = Label.New(Icons[i]);

                TaskMovableObject car = new(
                        Icons[i],
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