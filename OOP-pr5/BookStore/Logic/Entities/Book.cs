
namespace BookStore;

public class Book : IBook
{
    public IEnumerable<IAuthor> Authors {get;} = [];
    public IEnumerable<ISaler> Salers {get;} = [];
    public IEnumerable<BookType> Type {get;} = [];
    public IEnumerable<BookGenre> Genre { get; private set; } = [];
    public int? WritingYear { get; private set; }
    public Language? NativeLanguage { get; private set; }
    public Language CurrentLanguage { get; private set; }
    public int? AgeLimit { get; private set; }
    public double Price { get; set; }
    public int NumberOfProducts { get; set; }
    public string Description { get; set; } = "";
    public DateTime PublicationDateTime { get; private set; }

    public long ID { get; private set; }

    public string Name { get; set; } = "";

    public Book(
        string name, 
        IEnumerable<IAuthor> authors, 
        int? writingYear, 
        Language currentLanguage, 
        double price,
        int numberOfProducts,
        IEnumerable<ISaler> salers,
        int? ageLimit = null,
        IEnumerable<BookType>? types = null, 
        IEnumerable<BookGenre>? genres = null, 
        string?  description = null,
        Language? nativeLanguage = null,
        long? ID = null
    )
    {
        this.ID = ID ?? GUID.UniqID;
        this.Name = name;
        this.Authors = authors;
        this.WritingYear = writingYear;
        this.CurrentLanguage = currentLanguage;
        this.Price = price;
        this.NumberOfProducts = numberOfProducts;
        this.Salers = salers;
        this.AgeLimit = ageLimit;
        this.Type = types ?? [];
        this.Genre = genres ?? [];
        this.Description = description ?? "";
        this.NativeLanguage = nativeLanguage;
        this.PublicationDateTime = DateTime.Now;
    }
}