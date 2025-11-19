using GObject;

public class Converter
{
    public enum Type
    {
        SByte,
        Byte,
        Short,
        UShort,
        Int,
        UInt,
        Long,
        ULong,
        Char,
        Float,
        Double,
        Decimal,
        ChildClass1,
        ChildClass2,
        ParrentClass,
    }
    public enum RPGClass
    {
        Player,
        Mage,
        Fighter,
        Dragonborn,
    }
    // Базовый класс для преобразования ссылочных типов
    public class Player
    {
        public string Name { get; set; }
        public Player(string name = "Nobody") => Name = name;
        public string Class { get; set; } = "Player";
        public virtual string Attack() => $"{Name} бьёт кулаком!";
    }

    public class Mage : Player
    {
        public Mage(string name) : base(name) { }
        public new string Class { get; set; } = "Mage";
        public override string Attack() => $"{Name} запускает магический снаряд!";
        public string Fireball() => $"{Name} кастует огненный шар!";
    }

    public class Fighter : Player
    {
        public Fighter(string name) : base(name) { }
        public new string Class { get; set; } = "Fighter";
        public override string Attack() => $"{Name} бьёт мечом!";
        public string Dismember() => $"{Name} отсекает конечности!";
    }

    public class Dragonborn : Player
    {
        public Dragonborn(string name) : base(name) { }
        public new string Class { get; set; } = "Dragonborn";
        public override string Attack() => $"{Name} бьёт хвостом!";
        public string DragonBreath() => $"{Name} испепеляет всё вокруг огненным дыханием!";
    }

    // Пользовательские преобразования
    public class Dist
    {
        protected decimal value;
        protected const decimal ft_to_m_coeff = 0.3048m;
        protected const decimal m_to_ft_coeff = 1m / ft_to_m_coeff;
        protected Dist(double n) => value = n < 0 ? 0 : (decimal)n;
        protected Dist(decimal n) => value = n < 0 ? 0 : n;
    }
    public class Meters : Dist
    {
        public Meters(double n) : base(n) { }
        public Meters(decimal n) : base(n) { }
        public static implicit operator double(Meters meter)
        {
            return (double)meter.value;
        }
        public static implicit operator decimal(Meters meter)
        {
            return meter.value;
        }
        public static explicit operator Feet(Meters meter)
        {
            return new Feet(m_to_ft_coeff * meter.value);
        }
        public static explicit operator string(Meters meter)
        {
            return $"{meter.value}м";
        }
    }

    public class Feet : Dist
    {
        public Feet(double n) : base(n) { }
        public Feet(decimal n) : base(n) { }
        public static implicit operator double(Feet feet)
        {
            return (double)feet.value;
        }
        public static implicit operator decimal(Feet feet)
        {
            return feet.value;
        }
        public static explicit operator Meters(Feet feet)
        {
            return new Meters(ft_to_m_coeff * feet.value);
        }
        public static explicit operator string(Feet feet)
        {
            return $"{feet.value}фт";
        }
    }
}

