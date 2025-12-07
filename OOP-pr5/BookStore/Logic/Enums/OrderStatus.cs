namespace BookStore;
/// <summary>
/// Статус заказа
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// В ожидании
    /// </summary>
    Pending,
    /// <summary>
    /// Подтверждён
    /// </summary>
    Confirmed,
    /// <summary>
    /// Оплачен
    /// </summary>
    Paid,
    /// <summary>
    /// Отправлен
    /// </summary>
    Shipped,
    /// <summary>
    /// Доставлен
    /// </summary>
    Delivered,
    /// <summary>
    /// Отменён
    /// </summary>
    Canceled,
    /// <summary>
    /// Возвращён
    /// </summary>
    Refunded,
}