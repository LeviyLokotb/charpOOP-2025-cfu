
using Gtk;

public abstract class MovableObject : IMovableObject
{
    public string? Icon;
    public event Action<double, double>? Move; 
    public event Action<double, double>? Put;
    protected Semaphore? Semaphore;
    public event EventHandler? Finished;

    protected double FinishLength;
    protected double x;
    protected double y;
    public double X 
    { 
        get => x;
        set
        {
            this.x = value;
            Move?.Invoke(value, y);
        }
    }
    public double Y
    { 
        get => y;
        set
        {
            this.y = value;
            Move?.Invoke(x, value);
        }
    }
    public (double, double) POS
    {
        get => (x, y);
        set
        {
            (this.x, this.y) = value;
            Move?.Invoke(this.x, this.y);
        }
    }

    public double Speed { get; protected set; }
    public bool IsFinished = true;

    protected void PutObject()
    {
        this.Put?.Invoke(x, y);
    }

    public void SetPosition(double x, double y)
    {
        this.POS = (x, y);
    }

    public void MoveBy(double x_delta=0, double y_delta=0)
    {
        // Движение регулируется семафором если он есть
        Semaphore?.WaitOne();
        try
        {
            this.POS = (x + x_delta, y + y_delta);
        }
        finally
        {
            Semaphore?.Release();
        }
    }

    public override string ToString()
    {
        return Icon ?? $"Object at ({X} ; {Y})";
    }

    // Метод для запуска движения в отдельном потоке
    public abstract void StartRace();

    protected abstract void Sleep(int milliseconds);

    // Движение
    protected void Race()
    {
        while (!IsFinished)
        {
            MoveBy(Speed);
            if (x >= FinishLength)
            {
                //IsFinished = true;
                //X = 0;
                Finished?.Invoke(this, null!);
                break;
            }
            Sleep(16);
            //Thread.Sleep(1000);
        }
    }
}