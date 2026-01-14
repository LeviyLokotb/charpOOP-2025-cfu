using SimpleNote.Services;
namespace SimpleNote.Models
{
    public class NoteCollection : IDisposable
    {
        private readonly List<INote> notes = [];
        private readonly string collectionPath;

        public NoteCollection(string collectionPath)
        {
            this.collectionPath = collectionPath;
            LoadNotes();
        }

        private void LoadNotes()
        {
            foreach (var file in Directory.GetFiles(collectionPath, "*.md"))
            {
                var content = File.ReadAllText(file);
                var note = NoteFactory.CreateFromFileContent(content);
                notes.Add(note); // Композиция: заметка не существует без коллекции
            }
        }

        public void Dispose()
        {
            notes.Clear();
        }
    }
}