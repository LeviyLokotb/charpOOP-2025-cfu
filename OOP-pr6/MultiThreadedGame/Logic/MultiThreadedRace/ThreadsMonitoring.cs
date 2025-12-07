
public class ThreadsMonitoring
{
    public List<Thread?> threads = [];
    public GameTime timer;
    public event Action<List<ThreadInfo>>? ThreadsInfoUpdate;

    public ThreadsMonitoring(GameTime? timer=null)
    {
        this.timer = timer ?? new();
        this.timer.TimeStep += (o, e) =>
        {
            if (threads == null) return;
            List<ThreadInfo> ThreadsInfo = [];
            // Console.WriteLine(threads.Count);
            foreach(Thread? thread in threads)
            {
                if (thread==null) continue;
                ThreadInfo ti = new()
                {
                    Name = thread.Name ?? "--",
                    Priority = thread.Priority,
                    State = thread.ThreadState
                };
                ThreadsInfo.Add(ti);
            }
            //Console.WriteLine(ThreadsInfo.Count);
            ThreadsInfoUpdate?.Invoke(ThreadsInfo);
        };
    }

    public void AddThreads(IEnumerable<Thread?> newThreads)
    {
        lock(threads) threads.AddRange(newThreads);
    }

    public void RemoveThreads(IEnumerable<Thread> remThreads)
    {
        lock(threads) threads.RemoveAll(t => remThreads.Contains(t) );
    }

    public void Clear()
    {
        lock(threads) threads.Clear();
    }

    public struct ThreadInfo
    {
        public string Name;
        public ThreadPriority Priority;
        public ThreadState State;
    }
}