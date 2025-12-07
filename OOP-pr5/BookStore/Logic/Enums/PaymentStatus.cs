namespace BookStore;
/// <summary>
/// Статус платежа
/// </summary>
public enum PaymentStatus
{
    Pending,
    Processing,
    Completed,
    Failed,
    Refunded
}