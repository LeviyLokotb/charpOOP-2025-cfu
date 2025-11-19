try
{
    int i = 0;
    try
    {   
        int x = 5;
        var y = x / i;
        Console.WriteLine("x={0}, y={1}", x, y);
    }
    catch (Exception e)
    {
        Console.WriteLine($"Exception: {e.Message}");
    }
    finally
    {
        Console.WriteLine("Выполнили блок finally");
    }
}
catch (Exception ex)
{
    Console.WriteLine("> Message:\n" + ex.Message);
    Console.WriteLine("> StackTrace:\n" + ex.StackTrace);
    Console.WriteLine("> TargetSize:\n" + ex.TargetSite);
    Console.WriteLine("> InnerException:\n" + ex.InnerException);
    Console.WriteLine("> Source:\n" + ex.Source);
    Console.WriteLine("> Data:\n" + ex.Data);
    Console.WriteLine("> HelpLink:\n" + ex.HelpLink);
}