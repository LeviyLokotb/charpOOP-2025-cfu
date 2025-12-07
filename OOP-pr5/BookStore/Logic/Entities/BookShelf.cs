namespace BookStore;
/// <summary>
/// Книжная полка
/// </summary>
public class BookShelf : IBookShelf
{
    /// <summary>
    /// Основная структура данных
    /// </summary>
    private List<IBook> data = [];
    public string CollectionDescription { get; set; }

    public int EntityCount => data.ToArray().Length;

    public DateTime CollectionCreatedDateTime { get; private set; }
    public DateTime CollectionUpdatedDateTime { get; private set; }
    public long ID { get; private set; }
    public string Name { get; set; }

    public int Count => EntityCount;

    public bool IsSynchronized => false;

    public object SyncRoot => this;

    public BookShelf(IEnumerable<IBook> init, string Name="Untitled", string Description="", long? ID=null)
    {
        this.ID = ID ?? GUID.UniqID;
        this.Name = Name;
        this.CollectionDescription = Description;
        this.CollectionCreatedDateTime = DateTime.Now;
        this.CollectionUpdatedDateTime = DateTime.Now;
        this.data.AddRange(init);
    }

    public void AddEntity(IBook entity)
    {
        data.Add(entity);
        this.CollectionUpdatedDateTime = DateTime.Now;
    }

    public void Clear()
    {
        data = [];
        this.CollectionUpdatedDateTime = DateTime.Now;
    }

    public void CopyTo(Array array, int index)
    {
        if (array is IEnumerable<IBook> books)
            data.InsertRange(index, books);
        else
            throw new ArgumentException("Array must be IEnumerable<IBookShelf>");
    }

    public IEnumerable<IBook> FindEntity(Func<IBook, bool> key)
    {
        return (IEnumerable<IBook>)data.Select( key );
    }

    public IEnumerable<IBook> GetAllBooks()
    {
        return data;
    }

    public IBook? GetEntityByID(int entityID)
    {
        var books = data.Where((book) => book.ID == entityID);
        if (books == null || books.ToArray().Length == 0)
            return null;
        return books.ElementAt(0);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => data.GetEnumerator();

    public bool IsCollectionContainEnity(IBook entity)
    {
        return data.Contains(entity);
    }

    public bool IsCollectionContainEnityByID(int entityID)
    {
        return GetEntityByID(entityID) != null;
    }

    public void RemoveEntity(IBook entity)
    {
        data.Remove(entity);
    }

    public void RemoveEntityById(int entityID)
    {
        var entity = GetEntityByID(entityID);
        if (entity == null) return;
        data.Remove(entity);
    }
}