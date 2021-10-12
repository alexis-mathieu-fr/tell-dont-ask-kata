using Xunit;

public class OrderShipmentUseCaseTest
{
    private readonly TestOrderRepository orderRepository = new TestOrderRepository();
    private readonly TestShipmentService shipmentService = new TestShipmentService();
    private readonly OrderShipmentUseCase useCase;

    public OrderShipmentUseCaseTest()
    {
        useCase = new OrderShipmentUseCase(orderRepository, shipmentService);
    }

    [Fact]
    public void ShipApprovedOrder()
    {
        Order initialOrder = new Order();
        initialOrder.SetId(1);
        initialOrder.SetStatus(OrderStatus.APPROVED);
        orderRepository.AddOrder(initialOrder);

        OrderShipmentRequest request = new OrderShipmentRequest();
        request.SetOrderId(1);

        useCase.Run(request);

        Assert.Equal(OrderStatus.SHIPPED, orderRepository.GetSavedOrder().GetStatus());
        Assert.Equal(initialOrder, shipmentService.GetShippedOrder());
    }

    [Fact]
    public void CreatedOrdersCannotBeShipped()
    {
        Order initialOrder = new Order();
        initialOrder.SetId(1);
        initialOrder.SetStatus(OrderStatus.CREATED);
        orderRepository.AddOrder(initialOrder);

        OrderShipmentRequest request = new OrderShipmentRequest();
        request.SetOrderId(1);

        Assert.Throws<OrderCannotBeShippedException>(() => useCase.Run(request));

        Assert.Null(orderRepository.GetSavedOrder());
        Assert.Null(shipmentService.GetShippedOrder());
    }

    [Fact]
    public void RejectedOrdersCannotBeShipped()
    {
        Order initialOrder = new Order();
        initialOrder.SetId(1);
        initialOrder.SetStatus(OrderStatus.REJECTED);
        orderRepository.AddOrder(initialOrder);

        OrderShipmentRequest request = new OrderShipmentRequest();
        request.SetOrderId(1);

        Assert.Throws<OrderCannotBeShippedException>(() => useCase.Run(request));

        Assert.Null(orderRepository.GetSavedOrder());
        Assert.Null(shipmentService.GetShippedOrder());
    }

    [Fact]
    public void ShippedOrdersCannotBeShippedAgain()
    {
        Order initialOrder = new Order();
        initialOrder.SetId(1);
        initialOrder.SetStatus(OrderStatus.SHIPPED);
        orderRepository.AddOrder(initialOrder);

        OrderShipmentRequest request = new OrderShipmentRequest();
        request.SetOrderId(1);

        Assert.Throws<OrderCannotBeShippedTwiceException>(() => useCase.Run(request));

        Assert.Null(orderRepository.GetSavedOrder());
        Assert.Null(shipmentService.GetShippedOrder());
    }
}