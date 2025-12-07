namespace MyMath
{
    /// <summary>Обобщённый тип для избежания боксинга</summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseMath<T> :
        IBaseMathSum<T>,
        IBaseMathSub<T>,
        IBaseMathMultipy<T>,
        IBaseMathDivision<T>,
        IBaseMathEqual<T>
    where T : IBaseMath<T> {}

    public interface IBaseMathSum<T> where T : IBaseMathSum<T>
    {
        public static abstract T operator +(T a, T b);
    }
    public interface IBaseMathSub<T> where T : IBaseMathSub<T>
    {
        public static abstract T operator -(T a, T b);
    }
    public interface IBaseMathMultipy<T> where T : IBaseMathMultipy<T>
    {
        public static abstract T operator *(T a, T b);
    }
    public interface IBaseMathDivision<T> where T : IBaseMathDivision<T>
    {
        public static abstract T operator /(T a, T b);
    }
    public interface IBaseMathEqual<T> where T : IBaseMathEqual<T>
    {
        public static abstract bool operator ==(T a, T b);
        public static abstract bool operator !=(T a, T b);
    }
}