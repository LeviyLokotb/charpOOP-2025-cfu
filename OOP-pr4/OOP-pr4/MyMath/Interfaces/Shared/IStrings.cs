namespace MyMath
{
    /// <summary>Обобщённый тип для избежания боксинга</summary>
    /// <typeparam name="T"></typeparam>
    public interface IStrings<T> where T : IStrings<T>
    {
        public static abstract explicit operator string(T a);
        public string ToString();
    }
}