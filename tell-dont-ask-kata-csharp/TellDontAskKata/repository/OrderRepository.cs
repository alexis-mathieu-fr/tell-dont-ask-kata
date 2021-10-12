public interface OrderRepository
{
    void Save(Order order);

    Order GetById(int orderId);
}