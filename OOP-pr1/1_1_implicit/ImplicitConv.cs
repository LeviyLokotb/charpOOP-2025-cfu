/*
# Таблица неявных преобразований
o================o============================================================o
|     FROM       |                           TO                               |
o================o============================================================o
| sbyte          ->  short, int, long, float, double, decimal                 |
| byte           ->  short, ushort, int, uint, long, float, double, decimal   |
| short          ->  int, long, float, double, decimal                        |
| ushort         ->  int, uint, long, ulong, float, double, decimal           |
| int            ->  long, float, double, decimal                             |
| uint           ->  long, ulong, float, double, decimal                      |
| long           ->  float, double, decimal                                   |
| char           ->  ushort, int, uint, long, ulong, float, double, decimal   |
| float          ->  double                                                   |
| ulong          ->  float, double, decimal                                   |
| Дочерний класс ->  Базовый класс                                            |
o================o============================================================o
*/
public class ImplicitConv : Converter
{
    private static Dictionary<(Type, Type), bool> ImplicitConversions = new Dictionary<(Type, Type), bool>
    {
        // Sbite
        {(Type.SByte, Type.Short), true},
        {(Type.SByte, Type.Int), true},
        {(Type.SByte, Type.Long), true},
        {(Type.SByte, Type.Float), true},
        {(Type.SByte, Type.Double), true},
        {(Type.SByte, Type.Decimal), true},
        // Bite
        {(Type.Byte, Type.Short), true},
        {(Type.Byte, Type.UShort), true},
        {(Type.Byte, Type.Int), true},
        {(Type.Byte, Type.UInt), true},
        {(Type.Byte, Type.Long), true},
        {(Type.Byte, Type.ULong), true},
        {(Type.Byte, Type.Float), true},
        {(Type.Byte, Type.Double), true},
        {(Type.Byte, Type.Decimal), true},
        // Short
        {(Type.Short, Type.Int), true},
        {(Type.Short, Type.Long), true},
        {(Type.Short, Type.Float), true},
        {(Type.Short, Type.Double), true},
        {(Type.Short, Type.Decimal), true},
        // Ushort
        {(Type.UShort, Type.Int), true},
        {(Type.UShort, Type.UInt), true},
        {(Type.UShort, Type.Long), true},
        {(Type.UShort, Type.ULong), true},
        {(Type.UShort, Type.Float), true},
        {(Type.UShort, Type.Double), true},
        {(Type.UShort, Type.Decimal), true},
        // Int
        {(Type.Int, Type.Long), true},
        {(Type.Int, Type.Float), true},
        {(Type.Int, Type.Double), true},
        {(Type.Int, Type.Decimal), true},
        // Uint
        {(Type.UInt, Type.Long), true},
        {(Type.UInt, Type.ULong), true},
        {(Type.UInt, Type.Float), true},
        {(Type.UInt, Type.Double), true},
        {(Type.UInt, Type.Decimal), true },
        // Long
        {(Type.Long, Type.Float), true},
        {(Type.Long, Type.Double), true},
        {(Type.Long, Type.Decimal), true},
        // Ulong
        {(Type.ULong, Type.Float), true},
        {(Type.ULong, Type.Double), true},
        {(Type.ULong, Type.Decimal), true},
        // Char
        {(Type.Char, Type.UShort), true},
        {(Type.Char, Type.Int), true},
        {(Type.Char, Type.UInt), true},
        {(Type.Char, Type.Long), true},
        {(Type.Char, Type.ULong), true},
        {(Type.Char, Type.Float), true},
        {(Type.Char, Type.Double), true},
        {(Type.Char, Type.Decimal), true},
        // Float
        {(Type.Float, Type.Double), true},
        // Ссылочный тип (классы)
        {(Type.ChildClass1, Type.ParrentClass), true},
        {(Type.ChildClass2, Type.ParrentClass), true},
    };

    public static bool CanConvertImplicit(Type inType, Type outType)
    {
        return ImplicitConversions.ContainsKey((inType, outType));
    }

    // Генерирует значение указанного типа и преобразует в указанный
    public static string ImplicitConvThis(Type inType, Type outType)
    {
        if (inType == outType)
        {
            return "Выбран один и тот же тип, преобразование не выполняется.";
        }
        if (!CanConvertImplicit(inType, outType))
        {
            return $"Неявное преобразование из {inType} в {outType} недопустимо!";
        }

        // Создаём i указанного типа
        dynamic i;
        switch (inType)
        {
            case Type.SByte:
                sbyte example = 100;
                i = example;
                break;
            case Type.Byte:
                byte example2 = 100;
                i = example2;
                break;
            case Type.Short:
                short example3 = 100;
                i = example3;
                break;
            case Type.UShort:
                ushort example4 = 100;
                i = example4;
                break;
            case Type.Int:
                int example5 = 100;
                i = example5;
                break;
            case Type.UInt:
                uint example6 = 100U;
                i = example6;
                break;
            case Type.Long:
                long example7 = 100L;
                i = example7;
                break;
            case Type.ULong:
                ulong example8 = 100UL;
                i = example8;
                break;
            case Type.Char:
                char example9 = 'F';
                i = example9;
                break;
            case Type.Float:
                float example0 = 100.0F;
                i = example0;
                break;
            case Type.ChildClass1:
                Mage example00 = new Mage("Merlin");
                i = example00;
                break;
            case Type.ChildClass2:
                Fighter example01 = new Fighter("Artur");
                i = example01;
                break;
            default:
                return $"Тип {inType} не имеет неявных преобразований.";
        }
        dynamic N;
        //Player P;
        switch (outType)
        {
            case Type.SByte:
                sbyte n = i;
                N = n;
                break;
            case Type.Byte:
                byte n1 = i;
                N = n1;
                break;
            case Type.Short:
                short n2 = i;
                N = n2;
                break;
            case Type.UShort:
                ushort n3 = i;
                N = n3;
                break;
            case Type.Int:
                int n4 = i;
                N = n4;
                break;
            case Type.UInt:
                uint n5 = i;
                N = n5;
                break;
            case Type.Long:
                long n6 = i;
                N = n6;
                break;
            case Type.ULong:
                ulong n7 = i;
                N = n7;
                break;
            case Type.Char:
                char n8 = i;
                N = n8;
                break;
            case Type.Float:
                float n9 = i;
                N = n9;
                break;
            case Type.Double:
                double n0 = i;
                N = n0;
                break;
            case Type.Decimal:
                decimal n00 = i;
                N = n00;
                break;

            case Type.ParrentClass:
                // Преобразуем к базовому
                // Определено позже для удобства
                N = new Player();
                break;
            default:
                return $"Преобразование в тип {outType} не определено.";
        }
        string result = $"Успешно выполнено преобразование из {inType} в {outType} \n";
        if((inType == Type.ChildClass1 || inType == Type.ChildClass2) && outType == Type.ParrentClass)
        {
            // dynamic почему-то не хочет хранить объекты классов как надо, так что преобразуем здесь
            Player P = i;
            result += $":: В качестве базового используем класс Player, в качестве дочернего - класс {i.Class}\n";
            result += $"   {i.Class} m = new {i.Class}()\n";
            result += $"   {P.Class} p = m\n";
        }
        else
        {
            result += $"   {inType.ToString().ToLower()} a = " + (inType==Type.Char ? $"'{i}'\n" : $"{i}\n");
            result += $"   {outType.ToString().ToLower()} b = a    " + (inType==Type.Char ? $"// '{N}'\n" : $"// {N}\n");    
        }
        
    
        return result;
    }

    // Пример преобразований
    public static string ImplicitConvDemo(string inTypeS, string outTypeS)
    {
        Type inType = Type.Int;
        Type outType =  Type.Int;
        foreach (Type t in Enum.GetValues(typeof(Type)))
        {
            if ($"{t}" == inTypeS) inType = t;
            if ($"{t}" == outTypeS) outType = t;
        }
        return ImplicitConvThis(inType, outType);
    }
}
