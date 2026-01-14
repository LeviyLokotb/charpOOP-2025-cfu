using SimpleNote.Models;
using SimpleNote.Utils;

namespace SimpleNote.Services
{
    public class NoteManager : INoteManager
    {
        private readonly INoteStorage noteStorage;

        public string NotesDirectory => noteStorage.NotesDirectory;


        public NoteManager(INoteStorage noteStorage)
        {
            this.noteStorage = noteStorage ?? throw new ArgumentNullException(nameof(noteStorage));
        }

        public INote CreateNewNote(string title, string content)
        {
            var note = NoteFactory.CreateNote(title, content);
            if (noteStorage.IsNoteExist(note)) throw new FileAlreadyExistException();
            noteStorage.Save(note);
            return note;
        }

        public void DeleteNote(string title)
        {
            var note = noteStorage.Load(title);
            if (noteStorage.IsNoteExist(note))
            {
                noteStorage.Delete(note);
            }
        }

        public IEnumerable<INote> GetAllNotes()
        {
            var titles = noteStorage.GetAllNoteTitles();

            INote? note = null;
            foreach(var title in titles)
            {
                try
                {
                    note = noteStorage.Load(title);
                }
                catch (FileNotFoundException)
                {
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error loading note: {e}");
                }

                if (note != null)
                {
                    yield return note;
                }
            }
        }

        public INote GetNote(string title)
        {
            return noteStorage.Load(title);
        }

        public void UpdateNote(string title, string newContent)
        {
            INote note = GetNote(title);
            if (noteStorage.IsNoteExist(note))
            {
                
            }
            note.Content = newContent;
            noteStorage.Save(note);
        }

        public void AddToNote(string title, string newContent)
        {
            // var oldContent = GetNote(title).Content;
            // INote note = GetNote(title);
            // note.Content = oldContent + "\n" + newContent;
            // noteStorage.Save(note);

            var filePath = noteStorage.GetFilePath(title);
    
            using var writer = new StreamWriter(filePath, append: true);
            writer.WriteLine(); // Добавляем пустую строку
            writer.Write(newContent);
        }
    }
}