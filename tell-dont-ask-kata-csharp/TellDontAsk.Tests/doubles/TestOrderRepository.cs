using System.Collections.Generic;
using System.Linq;

public class TestOrderRepository : OrderRepository
{
    private Order insertedOrder;
    private List<Order> orders = new List<Order>();

    public Order GetSavedOrder()
    {
        return insertedOrder;
    }

    public void Save(Order order)
    {
        insertedOrder = order;
    }

    public Order GetById(int orderId)
    {
        return orders.First(o => o.GetId() == orderId);
    }

    public void AddOrder(Order order)
    {
        orders.Add(order);
    }
}