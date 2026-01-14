using SimpleNote.Models;

namespace SimpleNote.Utils
{
    public class CreateNoteCommand : ICommand
    {
        private readonly INoteManager manager;
        private readonly string title;
        private readonly string content;
        private INote? createdNote; 

        public CreateNoteCommand(INoteManager noteManager, string title, string content)
        {
            manager = noteManager;
            this.title = title;
            this.content = content;
        }

        public void Execute()
        {
            createdNote = manager.CreateNewNote(title, content);
        }

        public bool CanUndo => createdNote != null;

        public void Undo()
        {
            if (CanUndo)
                manager.DeleteNote(createdNote!.Title);
        }
    }
}