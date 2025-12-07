namespace MyMath
{
    /// <summary>Обобщённый тип для избежания боксинга</summary>
    /// <typeparam name="T"></typeparam>
    public interface IVectorMath<T> where T : IVectorMath<T>
    {
        /// <summary>Скалярное произведение</summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static abstract double operator &(T a, T b);
        /// <summary>Векторное</summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static abstract T operator *(T a, T b);
        public static abstract T operator *(T a, double b);
        public static abstract T operator *(double a, T b);
    }
}