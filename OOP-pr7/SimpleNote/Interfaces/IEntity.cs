namespace SimpleNote.Models
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// Уникальный ID
        /// </summary>
        string UUID { get; }
    }
}