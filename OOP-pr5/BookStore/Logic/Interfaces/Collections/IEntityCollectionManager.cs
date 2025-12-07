namespace BookStore;
/// <summary>
/// Интерфейс описывающий CRUD методы управления коллекцией Entity
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEntityCollectionManager<T> where T : IEntity
{
    void AddEntity(T entity);
    void RemoveEntity(T entity);
    void RemoveEntityById(int entityID);
    bool IsCollectionContainEnity(T entity);
    bool IsCollectionContainEnityByID(int entityID);
    void Clear();
    T? GetEntityByID(int entityID);
    IEnumerable<T> GetAllBooks();
    IEnumerable<T> FindEntity(Func<T, bool> key);
}