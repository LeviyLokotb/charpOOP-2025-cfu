public class GodOfCats
{
    private static Tail? _GodsTail;
    private GodOfCats(){}

    public static Tail GodsTail 
    {
        get
        {
            if (_GodsTail == null)
                _GodsTail = new Tail();
            return _GodsTail;
        }
    }
}

//////////////
public class Tail {}