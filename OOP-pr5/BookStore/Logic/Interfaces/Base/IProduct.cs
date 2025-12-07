namespace BookStore;
/// <summary>
/// Базовый интерфейс для продуктов
/// </summary>
public interface IProduct : IEntity
{
    /// <summary>
    /// Цена товара
    /// </summary>
    double Price { get; set; }
    /// <summary>
    /// Доступное количество продуктов
    /// </summary>
    int NumberOfProducts { get; set; }
    /// <summary>
    /// Описание товара
    /// </summary>
    string Description { get; set; }
    /// <summary>
    /// Дата и время размешения товара
    /// </summary>
    DateTime PublicationDateTime { get; }
}