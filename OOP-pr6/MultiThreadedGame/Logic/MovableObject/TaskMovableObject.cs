
public class TaskMovableObject : MovableObject, IMovableObject
{
    private Task? movementTask;
    public Task? MoveTask => movementTask;

    public TaskMovableObject(
        string? icon,
        Action<double, double>? OnPut,
        Action<double, double>? OnMove,
        Semaphore? semaphore=null,
        double speed=1, double FinishLength=800,
        double x=0, double y=0
    ) : base()
    {
        this.Icon = icon;
        this.Semaphore = semaphore;

        this.Speed = speed;
        this.x = x;
        this.y = y;
        this.FinishLength = FinishLength;

        this.Put += OnPut;
        this.Move += OnMove;

        PutObject();
    }

    public async override void StartRace()
    {
        if (!IsFinished) return;
        
        IsFinished = false;
        movementTask = Task.Run(Race);
        await movementTask;
    }

    protected override void Sleep(int timeout)
    {
        Thread.Sleep(timeout);
    }
}