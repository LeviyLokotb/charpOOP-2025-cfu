using SimpleNote.Models;
using SimpleNote.Services;

namespace SimpleNote.Utils
{
    public class Sandglass : INoteStorage
    {
        private readonly string notesDirectory;
        public string NotesDirectory => notesDirectory;

        public Sandglass(string notesDirectory)
        {
            this.notesDirectory = notesDirectory ?? throw new ArgumentNullException(nameof(notesDirectory));

            if (!Directory.Exists(notesDirectory))
            {
                Directory.CreateDirectory(notesDirectory);
            }
        }

        public void Delete(INote note)
        {
            var filePath = GetFilePath(note);
            
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                UUIDGiver.ReleaseUUID(note.UUID);
            }
        }

        public IEnumerable<string> GetAllNoteTitles()
        {
            return Directory.GetFiles(notesDirectory, "*.md")
                    .Select(Path.GetFileNameWithoutExtension)
                    .Where(s => s!=null)!;
        }

        public bool IsNoteExist(INote note)
        {
            var filePath = GetFilePath(note);
            return File.Exists(filePath);
        }

        public INote Load(string title)
        {
            var filePath = GetFilePath(title);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File with title \"{title}\" not found", filePath);
            }

            var fileContent = File.ReadAllText(filePath);
            return NoteFactory.CreateFromFileContent(fileContent);
        }

        public void Save(INote note)
        {
            if (note == null) throw new ArgumentNullException(nameof(note));

            var filePath = GetFilePath(note);
            File.WriteAllText(filePath, note.ToFileContent());
        }

        public string GetFilePath(string title)
        {
            // Используем .md расширение для Markdown файлов
            return Path.Combine(notesDirectory, $"{title}.md");
        }
        private string GetFilePath(INote note)
        {
            // Используем .md расширение для Markdown файлов
            return GetFilePath(note.Title);
        }
    }
}