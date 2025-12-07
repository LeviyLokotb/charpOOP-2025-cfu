namespace BookStore;

public interface IOrder : IEntity
{
    IReader Customer { get; set; }
    ISaler Sender { get; set; }
    IEnumerable<IOrderSingle> Items { get; set; }
    double TotalPrice { get; }
    OrderStatus Status { get; set; }
    DateTime OrderDateTime { get; set; }
    DateTime DeliveryDateTime { get; set; }
    string ShippingAddress { get; set; }
}