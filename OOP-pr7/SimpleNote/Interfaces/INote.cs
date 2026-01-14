namespace SimpleNote.Models
{
    public interface INote : IEntity
    {
        string Content { get; set; }
        string ToFileContent();
    }
}