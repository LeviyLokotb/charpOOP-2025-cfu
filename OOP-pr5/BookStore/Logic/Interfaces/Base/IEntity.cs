namespace BookStore;
/// <summary>
/// Базовый интерфейс для всех сущностей в проекте
/// </summary>
public interface IEntity
{
    /// <summary>
    /// ID сущности
    /// </summary>
    long ID { get; }
    /// <summary>
    /// Наименование сущности
    /// </summary>
    string Name { get; set; }
}