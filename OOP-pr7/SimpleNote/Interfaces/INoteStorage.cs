namespace SimpleNote.Models
{
    public interface INoteStorage
    {
        string NotesDirectory { get; }
        /// <summary>
        /// Сохранить заметку в хранилище
        /// </summary>
        /// <param name="note"></param>
        void Save(INote note);

        /// <summary>
        /// Загрузить заметку по названию
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        INote Load(string title);

        /// <summary>
        /// Удалить заметку
        /// </summary>
        /// <param name="id"></param>
        void Delete(INote note);

        /// <summary>
        /// Получить все заметки в хранилище
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetAllNoteTitles();

        /// <summary>
        /// Есть ли заметка с таким id в хранилище 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsNoteExist(INote note);

        /// <summary>
        /// Полный путь к файлу
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        string GetFilePath(string title);
    }
}