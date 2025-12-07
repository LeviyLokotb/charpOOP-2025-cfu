namespace BookStore;
/// <summary>
/// Интерфейс, описывающий Читателя (клиента)
/// </summary>
public interface IReader : IPerson
{
    /// <summary>
    /// Год рождения читателя
    /// </summary>
    int BirthYear { get; set; }
    /// <summary>
    /// Возраст читателя
    /// </summary>
    int Age { get; }
    /// <summary>
    /// Родной язык читателя
    /// </summary>
    Language NativeLanguage { get; set; }
    /// <summary>
    /// Предпочитаемые языки читателя
    /// </summary>
    ICollection<Language> PreferedLanguages { get; set; }
    /// <summary>
    /// Список желаемых книг
    /// </summary>
    IBookShelf WishList { get; set; }
    /// <summary>
    /// Книги во владении
    /// </summary>
    IBookShelf OwnBooks { get; set; }
}