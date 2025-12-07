namespace BookStore;

public interface IOrderSingle : IEntity
{
    IBook Book { get; set; }
    int NumberOfProducts { get; set; }
    double TotalPrice { get; }
}