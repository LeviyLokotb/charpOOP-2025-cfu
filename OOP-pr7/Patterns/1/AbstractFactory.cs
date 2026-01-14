public interface ITeaPartyFactory
{
    ITeaPot CreateTeaPot();
    ITeaCup CreateTeaCup();
}

////////////////////////////

public interface ITeaPot{}
public interface ITeaCup{}