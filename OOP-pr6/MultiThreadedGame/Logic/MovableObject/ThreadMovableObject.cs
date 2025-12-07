
public class ThreadMovableObject : MovableObject, IMovableObject
{
    private Thread? movementThread;
    public Thread? Thread => movementThread;
    public ThreadPriority Priority { get; set; } = ThreadPriority.Normal;

    public ThreadMovableObject(
        string? icon,
        ThreadPriority priority, 
        Action<double, double>? OnPut,
        Action<double, double>? OnMove,
        Semaphore? semaphore=null,
        double speed=1, double FinishLength=800,
        double x=0, double y=0
    )
    {
        this.Icon = icon;
        this.Priority = priority;
        this.Semaphore = semaphore;

        this.Speed = speed;
        this.x = x;
        this.y = y;
        this.FinishLength = FinishLength;

        this.Put += OnPut;
        this.Move += OnMove;

        PutObject();
    }

    public override void StartRace()
    {
        if (!IsFinished) return;
        
        IsFinished = false;
        
        movementThread = new Thread(Race)
        {
            Name = this.ToString(),
            Priority = this.Priority
        };
        movementThread.Start();
    }

    protected override void Sleep(int timeout)
    {
        Thread.Sleep(timeout);
    }
}