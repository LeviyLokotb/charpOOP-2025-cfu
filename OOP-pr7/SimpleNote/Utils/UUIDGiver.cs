using SimpleNote.Models;

namespace SimpleNote.Utils
{
    /// <summary>
    /// Класс, отвечающий за выдачу уникальных ID сущностям.
    /// <para>
    /// UUID представляет собой 10-значное число в виде строки
    /// </para>
    /// </summary>
    public static class UUIDGiver
    {
        /// <summary>
        /// Генератор случайных чисел для ID
        /// </summary>
        private static Random generator = new();
        /// <summary>
        /// Список уже использованных ID
        /// </summary>
        private static List<string> usedUUID = [];
        public static string GetUUID()
        {
            string id;
            do
                id = ((long)(generator.NextDouble() * 1e10)).ToString();
            while (usedUUID.Contains(id));
            
            usedUUID.Add(id);
            return id;
        }
        public static void ReleaseUUID(string id)
        {
            usedUUID.Remove(id);
        }
        public static void ReleaseUUID(IEntity entity)
        {
            ReleaseUUID(entity.UUID);
        }
    }
}