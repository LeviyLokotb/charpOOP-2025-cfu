namespace MyMath
{
    /// <summary>Обобщённый тип для избежания боксинга</summary>
    /// <typeparam name="T"></typeparam>
    public interface IVector3Base<T> where T : IVector3Base<T>
    {
        public double this[int i] { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int Lenth { get; }
        public double[] ToArray();
    }
}