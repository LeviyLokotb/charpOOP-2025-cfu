namespace SimpleNote.Utils
{
    public interface ICommand
    {
        void Execute();
        bool CanUndo { get; }
        void Undo();
    }
}