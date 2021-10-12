public class TestShipmentService : ShipmentService
{
    private Order shippedOrder;

    public Order GetShippedOrder()
    {
        return shippedOrder;
    }

    public void Ship(Order order)
    {
        shippedOrder = order;
    }
}