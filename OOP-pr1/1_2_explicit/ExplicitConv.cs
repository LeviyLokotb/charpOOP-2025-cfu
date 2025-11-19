/*
# Таблица явных преобразований
o================o============================================================================o
|     FROM       |                           TO                                               |
o================o============================================================================o
| sbyte          ->  byte, ushort, uint, ulong, char                                          |
| byte           ->  sbyte, char                                                              |
| short          ->  sbyte, byte, ushort, uint, ulong, char                                   |
| ushort         ->  sbyte, byte, short, char                                                 |
| int            ->  sbyte, byte, short, ushort, uint, ulong, char                            |
| uint           ->  sbyte, byte, short, ushort, int, char                                    |
| long           ->  sbyte, byte, short, ushort, int, uint, ulong, char                       |
| ulong          ->  sbyte, byte, short, ushort, int, uint, long, char                        |
| char           ->  sbyte, byte, short                                                       |
| float          ->  sbyte, byte, short, ushort, int, uint, long, ulong, char, decimal        |
| double         ->  sbyte, byte, short, ushort, int, uint, long, ulong, char, float, decimal |
| decimal        ->  sbyte, byte, short, ushort, int, uint, long, ulong, char, float, double  |
o================o============================================================================o
*/
public class ExplicitConv : Converter
{
    private static Dictionary<(Type, Type), bool> ExplicitConversions = new Dictionary<(Type, Type), bool>
    {
        // Sbyte
        {(Type.SByte, Type.Byte), true},
        {(Type.SByte, Type.UShort), true},
        {(Type.SByte, Type.UInt), true},
        {(Type.SByte, Type.ULong), true},
        {(Type.SByte, Type.Char), true},
        // Byte
        {(Type.Byte, Type.SByte), true},
        {(Type.Byte, Type.Char), true},
        // Short
        {(Type.Short, Type.SByte), true},
        {(Type.Short, Type.Byte), true},
        {(Type.Short, Type.UShort), true},
        {(Type.Short, Type.UInt), true},
        {(Type.Short, Type.ULong), true},
        {(Type.Short, Type.Char), true},
        // Ushort
        {(Type.UShort, Type.SByte), true},
        {(Type.UShort, Type.Byte), true},
        {(Type.UShort, Type.Short), true},
        {(Type.UShort, Type.Char), true},
        // Int
        {(Type.Int, Type.SByte), true},
        {(Type.Int, Type.Byte), true},
        {(Type.Int, Type.Short), true},
        {(Type.Int, Type.UShort), true},
        {(Type.Int, Type.UInt), true},
        {(Type.Int, Type.ULong), true},
        {(Type.Int, Type.Char), true},
        // Uint
        {(Type.UInt, Type.SByte), true},
        {(Type.UInt, Type.Byte), true},
        {(Type.UInt, Type.Short), true},
        {(Type.UInt, Type.UShort), true},
        {(Type.UInt, Type.Int), true },
        {(Type.UInt, Type.Char), true },
        // Long
        {(Type.Long, Type.SByte), true},
        {(Type.Long, Type.Byte), true},
        {(Type.Long, Type.Short), true},
        {(Type.Long, Type.UShort), true},
        {(Type.Long, Type.Int), true},
        {(Type.Long, Type.UInt), true},
        {(Type.Long, Type.ULong), true},
        {(Type.Long, Type.Char), true},
        // Ulong
        {(Type.ULong, Type.SByte), true},
        {(Type.ULong, Type.Byte), true},
        {(Type.ULong, Type.Short), true},
        {(Type.ULong, Type.UShort), true},
        {(Type.ULong, Type.Int), true},
        {(Type.ULong, Type.UInt), true},
        {(Type.ULong, Type.Long), true},
        {(Type.ULong, Type.Char), true},
        // Char
        {(Type.Char, Type.SByte), true},
        {(Type.Char, Type.Byte), true},
        {(Type.Char, Type.Short), true},
        // Float
        {(Type.Float, Type.SByte), true},
        {(Type.Float, Type.Byte), true},
        {(Type.Float, Type.Short), true},
        {(Type.Float, Type.UShort), true},
        {(Type.Float, Type.Int), true},
        {(Type.Float, Type.UInt), true},
        {(Type.Float, Type.Long), true},
        {(Type.Float, Type.ULong), true},
        {(Type.Float, Type.Char), true},
        {(Type.Float, Type.Decimal), true},
        // Double
        {(Type.Double, Type.SByte), true},
        {(Type.Double, Type.Byte), true},
        {(Type.Double, Type.Short), true},
        {(Type.Double, Type.UShort), true},
        {(Type.Double, Type.Int), true},
        {(Type.Double, Type.UInt), true},
        {(Type.Double, Type.Long), true},
        {(Type.Double, Type.ULong), true},
        {(Type.Double, Type.Char), true},
        {(Type.Double, Type.Float), true},
        {(Type.Double, Type.Decimal), true},
        // Decimal
        {(Type.Decimal, Type.SByte), true},
        {(Type.Decimal, Type.Byte), true},
        {(Type.Decimal, Type.Short), true},
        {(Type.Decimal, Type.UShort), true},
        {(Type.Decimal, Type.Int), true},
        {(Type.Decimal, Type.UInt), true},
        {(Type.Decimal, Type.Long), true},
        {(Type.Decimal, Type.ULong), true},
        {(Type.Decimal, Type.Char), true},
        {(Type.Decimal, Type.Float), true},
        {(Type.Decimal, Type.Double), true},
        // Ссылочный тип (классы)
        {(Type.ChildClass1, Type.ChildClass2), true},
        {(Type.ChildClass2, Type.ChildClass1), true},
        {(Type.ParrentClass, Type.ChildClass1), true},
        {(Type.ParrentClass, Type.ChildClass2), true},
        {(Type.ChildClass1, Type.ParrentClass), true},
        {(Type.ChildClass2, Type.ParrentClass), true},
    };

    public static bool CanConvertExplicit(Type inType, Type outType)
    {
        return ExplicitConversions.ContainsKey((inType, outType));
    }

    // Генерирует значение указанного типа и преобразует в указанный
    public static string ExplicitConvThis(Type inType, Type outType)
    {
        string result = "";
        if (inType == outType)
        {
            return "Выбран один и тот же тип, преобразование не выполняется.";
        }
        if (!CanConvertExplicit(inType, outType))
        {
            return $"Явное преобразование из {inType} в {outType} недопустимо!";
        }
        if ((inType == Type.ChildClass1 && outType == Type.ChildClass2) || (inType == Type.ChildClass2 && outType == Type.ChildClass1))
        {
            result += "Приведение этих типов может оказаться недопустимым!\n";
        }

        // Создаём i указанного типа
        dynamic? i = inType switch {
            Type.SByte        => (sbyte)100,
            Type.Byte         => (byte)100,
            Type.Short        => (short)100,
            Type.UShort       => (ushort)100,
            Type.Int          => (int)100,
            Type.UInt         => (uint)100,
            Type.Long         => (long)100,
            Type.ULong        => (ulong)100,
            Type.Char         => (char)100,
            Type.Float        => (float)100,
            Type.Double       => (double)100,
            Type.Decimal      => (decimal)100,
            Type.ChildClass1  => new Mage("Dumbledoor"),
            Type.ChildClass2  => new Fighter("Don Kihot"),
            Type.ParrentClass => new Player(),
            _                 => null
        };
        if(i == null) return $"Тип {inType} не имеет явных преобразований.";

        dynamic? N = outType switch {
            Type.SByte        => (sbyte)i,
            Type.Byte         => (byte)i,
            Type.Short        => (short)i,
            Type.UShort       => (ushort)i,
            Type.Int          => (int)i,
            Type.UInt         => (uint)i,
            Type.Long         => (long)i,
            Type.ULong        => (ulong)i,
            Type.Char         => (char)i,
            Type.Float        => (float)i,
            Type.Double       => (double)i,
            Type.Decimal      => (decimal)i,
            _                 => null
        };
        switch (outType)
        {
            case Type.ChildClass1:
                try
                {
                    N = (Mage)i;
                }
                catch (Exception e)
                {
                    return result + $"Преобразование вызвало исключение: {e.GetBaseException()}\n";
                }
                break;
            case Type.ChildClass2:
                try
                {
                    N = (Fighter)i;
                }
                catch (Exception e)
                {
                    return result + $"Преобразование вызвало исключение: {e.GetBaseException()}\n";
                }
                break;
            case Type.ParrentClass:
                // Явное преобразование в базовый класс всегда доступно
                N = (Player)i;
                break;
            default:
                if (N == null) return $"Преобразование в тип {outType} не определено.";
                break;
        }
        result += $"Успешно выполнено преобразование из {inType} в {outType} \n";
        string? ClassinType = inType switch
        {
            Type.ChildClass1 => "Mage",
            Type.ChildClass2 => "Fighter",
            Type.ParrentClass => "Player",
            _ => null,
        };
        if (inType == Type.ChildClass1 || inType == Type.ChildClass2 || inType == Type.ParrentClass)
        {
            result += $"   {i.Class} a = new {i.Class}()\n";
        }
        else
        {
            result += $"   {inType.ToString().ToLower()} a = " + ((inType == Type.Char) ? $"'{i}'\n" : $"{i}\n");
        }

        string? ClassoutType = outType switch
        {
            Type.ChildClass1 => "Mage",
            Type.ChildClass2 => "Fighter",
            Type.ParrentClass => "Player",
            _ => null,
        };
        if (outType == Type.ChildClass1 || outType == Type.ChildClass2 || outType == Type.ParrentClass)
        {
            result += $"   {ClassoutType} b = ({ClassoutType})a\n";
        }
        else
        {
            result += $"   {outType.ToString().ToLower()} b = ({outType.ToString().ToLower()})a   " + ((outType == Type.Char) ? $" // '{N}'\n" : $" // {N}\n");
        }
            
            
        return result;
    }

    // Пример преобразований
    public static string ExplicitConvDemo(string inTypeS, string outTypeS)
    {
        Type inType = Type.Int;
        Type outType = Type.Int;
        foreach (Type t in Enum.GetValues(typeof(Type)))
        {
            if ($"{t}" == inTypeS) inType = t;
            if ($"{t}" == outTypeS) outType = t;
        }
        return ExplicitConvThis(inType, outType);
    }
}
