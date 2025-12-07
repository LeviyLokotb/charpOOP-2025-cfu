
static public class GUID
{
    private static long GlobalCounter = 0;

    public static long UniqID => GlobalCounter++;
}