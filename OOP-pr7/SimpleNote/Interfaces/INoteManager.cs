namespace SimpleNote.Models
{
    /// <summary>
    /// Интерфейс, описывающий класс для управления заметками
    /// </summary>
    public interface INoteManager
    {
        string NotesDirectory { get; }
        /// <summary>
        /// Новая заметка
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        INote CreateNewNote(string title, string content);

        /// <summary>
        /// Обновить содержимое заметки 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newContent"></param>
        void UpdateNote(string title, string newContent);
        /// <summary>
        /// Добавляет текст в конец заметки
        /// </summary>
        /// <param name="title"></param>
        /// <param name="newContent"></param>
        void AddToNote(string title, string newContent);

        void DeleteNote(string title);

        /// <summary>
        /// Получить заметку по UUID
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        INote GetNote(string title);

        /// <summary>
        /// Получить все заметки
        /// </summary>
        /// <returns></returns>
        IEnumerable<INote> GetAllNotes();
    }
}