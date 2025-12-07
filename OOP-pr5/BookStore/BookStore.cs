namespace BookStore;
public class BookStoreApp
{
    private readonly BookBase _catalog;
    private readonly List<IReader> _readers;
    
    public BookStoreApp()
    {
        _catalog = new BookBase([]);
        _readers = [];
    }
    
    public void Run()
    {
        InitializeSampleData();
    }
    
    private void InitializeSampleData()
    {
        var author = new Author(name: "Сергей Лукъяненко", books: null, nativeLanguage: Language.Russian, country: "Russia");
        var book = new Book( 
            name: "Ночной дозор",
            authors: new List<IAuthor> { author },
            writingYear: 1998,
            currentLanguage: Language.Russian,
            nativeLanguage: Language.Russian,
            price: 500,
            numberOfProducts: 42,
            salers: new List<ISaler> { },
            types: new List<BookType> { BookType.Book },
            genres: new List<BookGenre> { BookGenre.Novel, BookGenre.Fantasy }
        );
        author.Books.AddEntity(book);
        _catalog.AddEntity(author.Books);
    }
}