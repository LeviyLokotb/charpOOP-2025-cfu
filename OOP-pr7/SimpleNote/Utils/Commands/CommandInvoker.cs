namespace SimpleNote.Utils
{
    public class CommandProcessor
    {
        private readonly Stack<ICommand> _history = new();
        
        public CommandProcessor? _commandProcessor;
        private CommandProcessor(){}

        public CommandProcessor GetCommandProcessor{
            get
            {
                if (_commandProcessor == null)
                    return new CommandProcessor();
                return _commandProcessor;
            }
        }

        public void Execute(ICommand command)
        {
            command.Execute();
            if (command.CanUndo)
                _history.Push(command);
        }
        
        public void UndoLast()
        {
            if (_history.Count > 0)
                _history.Pop().Undo();
        }
    }
}