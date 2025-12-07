namespace BookStore;
/// <summary>
/// Интерфейс, содержащий функции для платежей
/// </summary>
public interface IPaymentService : IEntity
{
    bool MakePayment(IOrder order, double amount);
    PaymentStatus GetPaymentStatus(IOrder order);
}