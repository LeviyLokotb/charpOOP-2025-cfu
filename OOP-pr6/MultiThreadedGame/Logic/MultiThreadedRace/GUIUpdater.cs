using Gtk;
public class GUIUpdater
{
    private Queue<Action> GUIUpdateQueue = [];
    public GameTime? timer;
    Mutex mutex = new();
    public GUIUpdater(GameTime? timer=null)
    {
        this.timer = timer ?? new();
        this.timer.TimeStep += (o, e) =>
        {
            if (!mutex.WaitOne(timer?.Delay ?? 100)) return;
            try
            {
                // Специальный метод для обновлений GUI
                GLib.Functions.IdleAdd(0, () =>
                {
                    lock (GUIUpdateQueue)
                    {
                        while (GUIUpdateQueue.Count > 0){
                            var action = GUIUpdateQueue.Dequeue();
                            action?.Invoke();
                        }
                    }
                    return false;
                });
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        };
    }

    public void Add(Action element)
    {
        lock(GUIUpdateQueue) GUIUpdateQueue.Enqueue(element);
    }

    public bool IsQueueEmpty()
    {
        //Console.WriteLine(GUIUpdateQueue.Count);
        return GUIUpdateQueue.Count == 0;
    }
}

