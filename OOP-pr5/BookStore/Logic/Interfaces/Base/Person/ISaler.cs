namespace BookStore;
/// <summary>
/// Интерфейс, описывающий продавца
/// </summary>
public interface ISaler : IPerson
{
    /// <summary>
    /// Описание продавца
    /// </summary>
    string Description { get; set; }
    BookShelf BooksForSale { get; set; } 
    BookShelf OwnBooks { get; set; } 
}