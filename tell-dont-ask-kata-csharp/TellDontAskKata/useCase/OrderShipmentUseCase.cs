public class OrderShipmentUseCase
{
    private readonly OrderRepository orderRepository;
    private readonly ShipmentService shipmentService;

    public OrderShipmentUseCase(OrderRepository orderRepository, ShipmentService shipmentService)
    {
        this.orderRepository = orderRepository;
        this.shipmentService = shipmentService;
    }

    public void Run(OrderShipmentRequest request)
    {
        Order order = orderRepository.GetById(request.GetOrderId());

        if (order.GetStatus().Equals(OrderStatus.CREATED) || order.GetStatus().Equals(OrderStatus.REJECTED))
        {
            throw new OrderCannotBeShippedException();
        }

        if (order.GetStatus().Equals(OrderStatus.SHIPPED))
        {
            throw new OrderCannotBeShippedTwiceException();
        }

        shipmentService.Ship(order);

        order.SetStatus(OrderStatus.SHIPPED);
        orderRepository.Save(order);
    }
}