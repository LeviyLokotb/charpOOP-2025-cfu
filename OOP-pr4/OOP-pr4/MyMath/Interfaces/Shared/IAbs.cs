namespace MyMath
{
    /// <summary>Обобщённый тип для избежания боксинга</summary>
    /// <typeparam name="T"></typeparam>
    public interface IAbs<T> where T : IAbs<T>
    {
        public double Abs { get; }
    }
}