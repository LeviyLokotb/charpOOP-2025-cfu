namespace MyMath
{
    /// <summary>Обобщённый тип для избежания боксинга</summary>
    /// <typeparam name="T"></typeparam>
    public interface IComplexExp<T> : IAbs<T> where T : IComplexExp<T>
    {
        // тут есть Abs
        public double Arg { get; set; }
        public static abstract T CreateExpComplex(double abs, double arg);
    }
}