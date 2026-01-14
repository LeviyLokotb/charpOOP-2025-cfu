namespace SimpleNote
{
    public class FileAlreadyExistException : Exception
    {
        public FileAlreadyExistException() : base() {}
        public FileAlreadyExistException(string message) : base(message) {}
    }
}