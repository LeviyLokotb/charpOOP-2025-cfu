using System.Collections;

namespace BookStore;
/// <summary>
/// База со списками книг (полками)
/// </summary>
public class BookBase : IEntityCollection, IEntityCollectionManager<IBookShelf>
{
    /// <summary>
    /// Основная структура данных
    /// </summary>
    private List<IBookShelf> data = [];
    public string CollectionDescription { get; set; }

    public int EntityCount => data.ToArray().Length;

    public DateTime CollectionCreatedDateTime { get; private set; }
    public DateTime CollectionUpdatedDateTime { get; private set; }
    public long ID { get; private set; }
    public string Name { get; set; }

    public int Count => EntityCount;

    public bool IsSynchronized => false;

    public object SyncRoot => this;

    public BookBase(IEnumerable<IBookShelf> init, string Name="Untitled", string Description="", long? ID=null)
    {
        this.ID = ID ?? GUID.UniqID;
        this.Name = Name;
        this.CollectionDescription = Description;
        this.CollectionCreatedDateTime = DateTime.Now;
        this.CollectionUpdatedDateTime = DateTime.Now;
        this.data.AddRange(init);
    }

    public void AddEntity(IBookShelf entity)
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
        if (array is IEnumerable<IBookShelf> books)
            data.InsertRange(index, books);
        else
            throw new ArgumentException("Array must be IEnumerable<IBookShelf>");
    }

    public IEnumerable<IBookShelf> FindEntity(Func<IBookShelf, bool> key)
    {
        return (IEnumerable<IBookShelf>)data.Select( key );
    }

    public IEnumerable<IBookShelf> GetAllBooks()
    {
        return data;
    }

    public IBookShelf? GetEntityByID(int entityID)
    {
        var shelfs = data.Where((shelf) => shelf.ID == entityID);
        if (shelfs == null || shelfs.ToArray().Length == 0)
            return null;
        return shelfs.ElementAt(0);
    }

    public IEnumerator GetEnumerator() => data.GetEnumerator();

    public bool IsCollectionContainEnity(IBookShelf entity)
    {
        return data.Contains(entity);
    }

    public bool IsCollectionContainEnityByID(int entityID)
    {
        return GetEntityByID(entityID) != null;
    }

    public void RemoveEntity(IBookShelf entity)
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