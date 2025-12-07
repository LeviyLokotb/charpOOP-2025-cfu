namespace BookStore;

public class Author : IAuthor
{
    public int? BirthYear { get; private set; }
    public int? DeathYear { get; private set; }
    public string? Country { get; private set; }
    public IBookShelf Books { get; set; }
    public Language NativeLanguage { get; private set; }
    public string Description { get; set; }

    public long ID { get; }

    public string Name { get; set; }

    public Author(
        string name,
        IBookShelf? books,
        Language nativeLanguage,
        string? country = null,
        int? birthYear = null,
        int? deathYear = null,
        string? description = null,
        long? ID = null
    )
    {
        this.ID = ID ?? GUID.UniqID;
        this.Name = name;
        this.Books = books ?? new BookShelf([]);
        this.NativeLanguage = nativeLanguage;
        this.Country = country;
        this.BirthYear = birthYear;
        this.DeathYear = deathYear;
        this.Description = description ?? "";
    }
}