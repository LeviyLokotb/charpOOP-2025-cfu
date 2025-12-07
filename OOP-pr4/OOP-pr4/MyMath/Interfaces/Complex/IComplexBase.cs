namespace MyMath
{
    /// <summary>Обобщённый тип для избежания боксинга</summary>
    /// <typeparam name="T"></typeparam>
    public interface IComplexBase<T> where T : IComplexBase<T>
    {
        public double Re { get; set; }
        public double Im { get; set; }
    }
}