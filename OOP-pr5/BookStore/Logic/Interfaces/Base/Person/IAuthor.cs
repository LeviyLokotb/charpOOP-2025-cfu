namespace BookStore;
/// <summary>
/// Интерфейс, описывающий Автора книги
/// </summary>
public interface IAuthor : IPerson
{
    /// <summary>
    /// Год рождения
    /// </summary>
    int? BirthYear { get; } 
    /// <summary>
    /// Год смерти
    /// </summary>
    int? DeathYear { get; } 
    /// <summary>
    /// Страна автора
    /// </summary>
    string? Country { get; }
    /// <summary>
    /// Книги этого автора
    /// </summary>
    IBookShelf Books { get; set; }
    /// <summary>
    /// Родной язык автора
    /// </summary>
    Language NativeLanguage { get; }
    /// <summary>
    /// Об авторе
    /// </summary>
    string Description { get; set; }
}