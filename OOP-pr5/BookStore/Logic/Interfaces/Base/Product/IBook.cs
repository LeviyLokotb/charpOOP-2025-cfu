namespace BookStore;
/// <summary>
/// Базовый интерфейс для книг
/// </summary>
public interface IBook : IProduct
{
    /// <summary>
    /// Авторы
    /// </summary>
    IEnumerable<IAuthor> Authors { get; }
    IEnumerable<ISaler> Salers { get; }

    /// <summary>
    /// Тип книги
    /// </summary>
    IEnumerable<BookType> Type { get; }
    /// <summary>
    /// Жанры книги
    /// </summary>
    IEnumerable<BookGenre> Genre { get; }
    /// <summary>
    /// Год написания
    /// </summary>
    int? WritingYear { get; }
    /// <summary>
    /// Язык оригинала
    /// </summary>
    Language? NativeLanguage { get; }
    /// <summary>
    /// Язык этой книги
    /// </summary>
    Language CurrentLanguage { get; }
    /// <summary>
    /// Возрастное ограничение
    /// </summary>
    int? AgeLimit { get; }
}