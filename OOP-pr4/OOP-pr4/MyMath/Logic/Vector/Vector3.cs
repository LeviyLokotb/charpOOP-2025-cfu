namespace MyMath
{
    using System.Text;
    public struct Vector3:
        IVector3Base<Vector3>,
        IBaseMath<Vector3>,
        IBaseMathEqual<Vector3>,
        IVectorMath<Vector3>,
        IAbs<Vector3>,
        IStrings<Vector3>
    {
        /// <summary>Массив представляющий собой трёхмерный вектор</summary>
        private double[] _vector = [0, 0, 0];
        /// <summary>Прослойка чтобы избежать null при создании объекта без конструктора</summary>
        private double[] vector { get => _vector ?? new double[3]; set => _vector = value; }
        private double abs;
        private void UpdateAbs()
            => abs = Math.Sqrt( vector.Select(n => n*n).Sum() );
        /// <summary>Индексатор</summary>
        /// <returns>null если индекс за пределами массива</returns>
        public double this[int i]
        {
            get => i < 0 || i > vector.Length ? throw new IndexOutOfRangeException() : vector[i] ;
            set
            {
                vector[i] = value;
                UpdateAbs();
            }
        }
        public double X { get => this[0]; set => this[0] = value; }
        public double Y { get => this[1]; set => this[1] = value; }
        public double Z { get => this[2]; set => this[2] = value; }
        public double Abs { get => abs; }
        public int Lenth { get => vector.Length; }
        /// <summary>Новый 3-мерный вектор</summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(double x = 0, double y = 0, double z = 0) => (X, Y, Z) = (x, y, z);
        /// <summary>Новый 3-мерный вектор из массива</summary>
        /// <param name="vec"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public Vector3(double[] vec)
        {
            if(vec.Length != vector.Length) throw new IndexOutOfRangeException();
            for(int i=0; i<vec.Length; i++) this[i] = vec[i];
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
            => new Vector3([.. a.ToArray().Select((_, i) => a[i] + b[i])]);
        public static Vector3 operator -(Vector3 a)
            => new Vector3([.. a.ToArray().Select(n => -n)]);
        public static Vector3 operator -(Vector3 a, Vector3 b)
            => a + -b;
        /// <summary>Векторное произведение</summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 a, Vector3 b)
            => new Vector3(
                a[1]*b[2] - a[2]*b[1],
                a[2]*b[0] - a[0]*b[2],
                a[0]*b[1] - a[1]*b[0]
            );
        /// <summary>Скалярное произведение</summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double operator &(Vector3 a, Vector3 b)
            => Enumerable.Sum([.. a.ToArray().Select((_, i) => a[i] * b[i])]);
        /// <summary>Обратное векторное произведение</summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 a, Vector3 b)
            => b*a;
        public static Vector3 operator *(Vector3 a, double b)
            => new Vector3([.. a.ToArray().Select(n => n*b)]);
        public static Vector3 operator *(double a, Vector3 b)
            => b*a;
        public static Vector3 operator /(Vector3 a, double b)
            => new Vector3([.. a.ToArray().Select(n => n/b)]);
        
        public static bool operator ==(Vector3 a, Vector3 b)
            => a.ToArray().Select( (_, i) => a[i] == b[i]).Aggregate((aa, bb) => aa && bb);
        public static bool operator !=(Vector3 a, Vector3 b) 
            => !(a==b);

        public static explicit operator string(Vector3 a)
            => $"<{string.Join(", ", a.vector.Select(n => n.ToString().Replace(",", ".")))}>";
        public override string ToString()
            => (string)this;
        
        public double[] ToArray()
            => vector;

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is Vector3 n) return n ==  this;

            try 
            { 
                Vector3 v = (Vector3)obj; 
                return v == this;
            }
            catch { return false; }
        }

        public override int GetHashCode()
        {
            var Sha = System.Security.Cryptography.SHA256.Create();
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes((string)this);
            byte[] hash_bytes = Sha.ComputeHash(bytes);
            return BitConverter.ToInt32(hash_bytes);
        }
    }
}