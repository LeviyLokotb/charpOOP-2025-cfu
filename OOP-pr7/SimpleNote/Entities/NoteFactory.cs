using System.Globalization;
using System.Text.RegularExpressions;
using SimpleNote.Models;

namespace SimpleNote.Services
{
    /// <summary>
    /// Интерфейс, описывающий класс для создания заметок
    /// </summary>
    public class NoteFactory : INoteFactory
    {
        /// <summary>
        /// Новая заметка
        /// </summary>
        /// <param name="title">Название</param>
        /// <param name="content">Содержимое</param>
        /// <returns></returns>
        public static INote CreateNote(string title, string content)
        {
            return new Scitilla(title, content);
        }

        /// <summary>
        /// Заметка из сырого содержимого файла
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public static INote CreateFromFileContent(string fileContent)
        {
            // ^[#]+ -- один # и больше в начале
            // \s+(.+?)\s* -- один пробел и больше, любой заголовок, любое количество пробелов
            // >\s+(\d{8})\s* -- один пробел и больше, 8 цифр, любое количество пробелов
            // (.*) -- любое количество любых символов
            var pattern = @"^#+" + @"\s+(.+?)\s*\n" + @">\s+(\d{8})\s*\n\n?" + @"(.*)";
            var match = Regex.Match(fileContent, pattern, RegexOptions.Singleline);

            if (!match.Success)
                throw new FormatException("Invalid file format");

            var title = match.Groups[1].Value.Trim();
            var dateString = match.Groups[2].Value;
            var content = match.Groups[3].Value;

            DateTime? datetimeNullable;
            if (DateTime.TryParseExact(
                dateString, "MMddyyyy", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var datetime))
            {
                datetimeNullable = datetime;
            }
            else
            {
                datetimeNullable = null;
            }

            return new Scitilla(title, content, datetimeNullable);
        }
    }
}