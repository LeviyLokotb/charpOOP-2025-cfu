
public class GUIMutexUpdater
{
    static Mutex GUIMutex = new Mutex();

    public static void DoWithMutex(Action action)
    {
        if (!GUIMutex.WaitOne()) return;
        try
        {
            action.Invoke();
        }
        finally
        {
            GUIMutex.ReleaseMutex();
        }
    }

}