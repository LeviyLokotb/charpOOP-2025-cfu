interface A
{
    void IsOK()
    {
        bool False = true;
    }
}

interface B
{
    void IsOK()
    {
        bool True = false;
    }
}

class Unit : A, B
{
    public void IsOK()
    {
        Console.WriteLine("Karas' != Okun'");
    }
}