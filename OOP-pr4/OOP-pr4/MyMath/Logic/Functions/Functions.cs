namespace MyMath
{
    public static class Functions
    {
        public static Vector3 CariolisForce(double ObjectMass, Vector3 ObjectVelocity, Vector3 SystemAngularVelocity)
            => -2 * ObjectMass * (ObjectVelocity * SystemAngularVelocity);
        
    // Пример функции с интерфейсом без боксинга 
    // ( тут становится понятно почему это работает )
    public static T Double<T>(T n) where T : IBaseMath<T>
    {
        return n + n;
    }
    }
}