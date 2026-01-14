public abstract class AutoBarista
{
    public abstract ICofee BrewCofee();

    public void Serving()
    {
        var coffee = BrewCofee();
        coffee.PourIntoCup();
        coffee.FoamDrawing();
        coffee.GiveToCustomer();
    }
}

//////////////////////////// 
public interface ICofee
{
    void PourIntoCup();
    void FoamDrawing();
    void GiveToCustomer();
}