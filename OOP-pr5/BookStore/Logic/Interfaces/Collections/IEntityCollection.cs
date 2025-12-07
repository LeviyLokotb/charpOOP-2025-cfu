namespace BookStore;
using System.Collections;
/// <summary>
/// Коллекция для хранения разных Entity
/// </summary>
public interface IEntityCollection : IEntity, ICollection
{
    /// <summary>
    /// Описание коллекции
    /// </summary>
    string CollectionDescription { get; set; }
    /// <summary>
    /// Число элементов в коллекции
    /// </summary>
    int EntityCount { get; }
    /// <summary>
    /// Дата создания коллекции
    /// </summary>
    DateTime CollectionCreatedDateTime { get; }
    /// <summary>
    /// Дата модификации коллекции
    /// </summary>
    DateTime CollectionUpdatedDateTime { get; }
}