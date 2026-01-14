using SimpleNote.Utils;

namespace SimpleNote.Models
{
    public class Scitilla : INote
    {
        // IEntity
        public string UUID { get; private set; }
        public string Title { get; set; }

        // INote
        public string Content { get; set; }
        public DateTime DateTimeCreation { get; private set; }

        // Конструктор
        public Scitilla(string title, string content, DateTime? dateTime = null, string? id = null)
        {
            UUID = id ?? UUIDGiver.GetUUID();
            Title = title;
            Content = content;
            DateTimeCreation = dateTime ?? DateTime.UtcNow;
        }

        public string ToFileContent()
        {
            var time = $"{DateTimeCreation:MMddyyyy}";
            //if (Title == "-now" || Title == "--now") Title = time;
            return $"# {Title}\n> {time}\n{Content}";
        }
    }
}