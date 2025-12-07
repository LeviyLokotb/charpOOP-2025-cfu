
public interface IMovableObject
{
    event Action<double, double>? Move; 
    event Action<double, double>? Put;
    event EventHandler? Finished;
    void StartRace();
}