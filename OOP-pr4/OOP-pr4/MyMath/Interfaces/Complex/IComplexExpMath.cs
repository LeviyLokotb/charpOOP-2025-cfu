namespace MyMath
{
    /// <summary>Обобщённый тип для избежания боксинга</summary>
    /// <typeparam name="T"></typeparam>
    public interface IComplexExpMath<T> where T : IComplexExpMath<T>
    {
        public static abstract T ExpMultipy(T a, T b);
        public static abstract T ExpDivision(T a, T b);
        public static abstract T Exp(T a);
    }
}