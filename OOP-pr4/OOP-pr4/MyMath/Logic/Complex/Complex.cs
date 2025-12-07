namespace MyMath
{
    using System.Text;
    using System.Text.RegularExpressions;

    public struct Complex :
        IComplexBase<Complex>,
        IAbs<Complex>,
        IComplexExp<Complex>,
        IBaseMath<Complex>,
        IComplexExpMath<Complex>,
        IStrings<Complex>
    {
        private double real = 0;
        private void UpdateRe() => real = abs * Math.Cos(arg);
        private double imag = 0;
        private void UpdateIm() => imag = abs * Math.Sin(arg);
        private double abs = 0;
        private void UpdateAbs() => abs = Math.Sqrt(imag*imag + real*real);
        private double arg;
        private void UpdateArg() => arg = Math.Atan(imag / real);

        // Алuебраическая форма: Re+Im*i
        /// <summary>Реальная часть</summary>
        public double Re 
        { 
            get => real; 
            set
            {
                real = value;
                UpdateAbs(); UpdateArg();
            } 
        }
        /// <summary>Мнимая часть</summary>
        public double Im 
        { 
            get => imag;
            set
            {
                imag = value;
                UpdateAbs(); UpdateArg();
            } 
        }

        // Экспоненциальная форма: Abs*exp(i * Arg)
        /// <summary>Модуль комплексного числа</summary>
        public double Abs { get => abs;}
        /// <summary>Аргумент комплексного числа</summary>
        public double Arg 
        { 
            get => arg;
            set
            {
                arg = value;
                UpdateIm(); UpdateRe();
                // UpdateIm();
                // UpdateRe();
            } 
        }

        /// <summary>Новое комплексное число из алгебраического представления</summary>
        /// <param name="real"></param>
        /// <param name="imagine"></param>
        public Complex(double real, double imagine) => (Re, Im) = (real, imagine);
        /// <summary>
        /// Новое комплексное число из экспоненциального представления
        /// <para>
        /// abs*e^(arg*i)
        /// </para>
        /// </summary>
        /// <param name="abs"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Complex CreateExpComplex(double abs, double arg)
        {
            Complex c = new(0, 0);
            c.abs = abs; // Буквально единственное место где позволительно изменить abs вручную
            c.Arg = arg;
            return c;
        }
        /// <summary>Деконструктор</summary>
        /// <param name="re"></param>
        /// <param name="im"></param>
        public void Deconstruct(out double re, out double im) => (re, im) = (Re, Im);
    
        ////////// Операторы
        // +
        public static Complex operator +(Complex a, Complex b) 
            => new Complex(a.Re + b.Re, a.Im + b.Im);
        // -
        public static Complex operator -(Complex a) 
            => new Complex(-a.Re, -a.Im);
        public static Complex operator -(Complex a, Complex b) 
            => a + -b;
        // *
        public static Complex operator *(Complex a, Complex b)
            => new Complex(a.Re*b.Re - a.Im*b.Im, a.Im*b.Re + b.Im*a.Re);
        /// <summary>Умножение в экспоненциальной форме</summary>
        public static Complex ExpMultipy(Complex a, Complex b)
            => CreateExpComplex(a.Abs*b.Abs, a.Arg+b.Arg);
        // /
        public static Complex operator /(Complex a, Complex b){
            return new Complex( 
                // div by 0 здесь не бываает
                (a.Re*b.Re + a.Im*b.Im) / (b.Re*b.Re + b.Im*b.Im), 
                (a.Im*b.Re - a.Re*b.Im) / (b.Re*b.Re +  b.Im*b.Im)
            );
        }
        /// <summary>Деление в экспоненциальной форме</summary>
        public static Complex ExpDivision(Complex a, Complex b)
            => CreateExpComplex(a.Abs/b.Abs, a.Arg-b.Arg);
        // ==
        public static bool operator ==(Complex a, Complex b) 
            => a.Re == b.Re && a.Im == b.Im;
        // !=
        public static bool operator !=(Complex a, Complex b) 
            => !(a == b);
        // exp
        /// <summary>
        /// Экспонента комплексного числа: 
        /// <para>e^a = e^{a.Re}( cos(a.Im) + i*sin(a.Im) )</para>
        /// </summary>
        public static Complex Exp(Complex a)
        {
            double eR = Math.Exp(a.Re);
            return new Complex(eR * Math.Cos(a.Im), eR * Math.Sin(a.Im));
        }
    
        ////////// Преобразования
        public static implicit operator Complex(int n) => new Complex(n, 0);
        public static implicit operator Complex(double n) => new Complex(n, 0);

        public static explicit operator Complex?(string s)
        {
            // [+-]? -- один +/- или ничего
            // [0-9.,]+ -- одна и больше цифра
            // [+-]{1} -- обязательный один +/-
            // [0-9.,]+
            // [ij]{1} -- обязательна одна i/j
            Regex rvalid = new(@"([+-]?[0-9.,]+)([+-]{1}[0-9.,]+)[ij]{1}");
            var matches = rvalid.Matches(s);
            if (matches.Count != 1) return null;
            var match = matches[0];

            var sR = match.Groups[1].Value;
            var sI = match.Groups[2].Value;

            if(!double.TryParse(sR, out double re)) 
                if(!double.TryParse(sR.Replace(".", ","), out re)) return null;
            if(!double.TryParse(sI, out double im)) 
                if(!double.TryParse(sI.Replace(".", ","), out im)) return null;

            return new Complex(re, im); 
        }

        public static explicit operator string(Complex a)
        {
            // 0
            if (a.Re == 0 && a.Im == 0) return "0";
            // Реальная часть: если 0 пустая, иначе число
            string R = $"{(a.Re==0?"":a.Re)}";
            // Если мнимой нет
            if (a.Im == 0) return R;
            // Знак: если <0 минус, если больше 0 +, если R нету и + то ничего
            string sign = a.Im<0?"-": R==""?"":"+";
            // Мнимая часть: если 1/-1 то ничего (будет просто i), так как знак уже учли Abs
            string I = $"{((a.Im*a.Im)==1?"":Math.Abs(a.Im))}";
            // Собираем результат, унифицируем (будет только с .)
            return (R+sign+I+"i").Replace(",", ".");
        }

        public override string ToString()
        {
            return $"({(string)this})";
        }
        /// <summary>Строковое прдставление</summary>
        /// <param name="expMode"></param>
        /// <returns></returns>
        public string ToString(bool expMode)
        {
            //=> expMode ? $"{(Abs==0?"0":($"{(Abs==1?"":Abs)}e^({(Arg==0?"0":$"{(Arg==1 ? "":Arg)}i")})"))}".Replace(",", "."): ToString();
            if (!expMode) return ToString();
            if (Abs==0) return "0";
            string Abs_ = $"{(Abs==1?"":Abs)}";
            if (Arg == 0) return Abs_;
            string sign = Arg<0?"-":"";
            string Arg_ = $"{sign}{(Arg*Arg==1?"":Math.Abs(Arg))}";
            return $"{Abs_}e^({Arg_}i)".Replace(",", ".");
        }

        // Потребовало из-за перегрузки == 
        // Определяет является ли object нашим типом
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is Complex n) return n == this;

            try 
            { 
                Complex? c = (Complex?)obj; 
                return c == this;
            }
            catch { return false; }
        }
        // Потребовало из-за перегрузки == 
        public override int GetHashCode()
        {
            var Sha = System.Security.Cryptography.SHA256.Create();
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes((string)this);
            byte[] hash_bytes = Sha.ComputeHash(bytes);
            return BitConverter.ToInt32(hash_bytes);
        }

        /// <summary>Мнимая единица</summary>
        public static readonly Complex i = new(0, 1);
    }
    
}