using Gtk;

public static class HashAlg
{
    /// <summary>
    /// Хэширует строку заданным алгоритмом
    /// 
    /// <para>
    /// Example:
    /// </para>
    /// 
    /// <para>
    /// string hashed_str = HashAlg.Hash<System.Security.Cryptography.SHA256>("admin123");
    /// </para>
    /// 
    /// </summary>
    /// <param name="s">Строка</param>
    /// <returns></returns>
    public static string? Hash<TAlgo>(string s) where TAlgo : System.Security.Cryptography.HashAlgorithm
    {
        object Alg;
        try
        { 
            // Create, public static, без параметров
            var Create = typeof(TAlgo).GetMethod("Create", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static, null, 
                Type.EmptyTypes, null
            ); 
            if (Create == null) throw new ArgumentException("У типа нет метода Create()");
            Alg = Create.Invoke(null, [])!;
            if (Alg == null) throw new ArgumentException("Проблема при вызове метода Create()");
        }
        catch { return null; }

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);

        object hash_bytes;
        try
        { 
            // Метод ComputeHash, public static, принимает byte[]
            var ComputeHash = typeof(TAlgo).GetMethod("ComputeHash", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, null, 
                [typeof(byte[])], null
            ); 
            if (ComputeHash == null) throw new ArgumentException("У типа нет метода ComputeHash()");
            hash_bytes = ComputeHash.Invoke(Alg, [bytes])!;
            if (hash_bytes == null) throw new ArgumentException("Проблема при вызове метода ComputeHash(byte[])");
        }
        catch { return null;}

        string hash_string = Convert.ToHexString((byte[])hash_bytes);
        return hash_string;
    }
}