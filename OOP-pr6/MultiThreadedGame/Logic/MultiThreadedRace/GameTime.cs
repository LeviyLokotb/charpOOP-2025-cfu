
using System.ComponentModel;

public class GameTime
{
    public int Delay { get; private set; } 
    BackgroundWorker? timer;
    public event EventHandler? TimeStep;

    public GameTime(int delay = 5)
    {
        Delay = delay;
    }
    public void StartTime()
    {
        timer = new();
        timer.WorkerSupportsCancellation = true;
        
        timer.DoWork += (o, e) =>
        {
            while (!timer.CancellationPending)
            {
                //Console.WriteLine($"mew^^~");
                Thread.Sleep(Delay);
                TimeStep?.Invoke(null, new EventArgs());
            }
        };

        timer.RunWorkerAsync();
    }

    public void StopTime()
    {
        timer?.CancelAsync();
    }
}