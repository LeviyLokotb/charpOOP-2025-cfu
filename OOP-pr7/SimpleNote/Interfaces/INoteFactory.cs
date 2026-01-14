namespace SimpleNote.Models
{
    /// <summary>
    /// Интерфейс, описывающий класс для создания заметок
    /// </summary>
    public interface INoteFactory
    {
        /// <summary>
        /// Новая заметка
        /// </summary>
        /// <param name="title">Название</param>
        /// <param name="content">Содержимое</param>
        /// <returns></returns>
        abstract static INote CreateNote(string title, string content);

        /// <summary>
        /// Заметка из сырого содержимого файла
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        abstract static INote CreateFromFileContent(string fileContent);
    }
}