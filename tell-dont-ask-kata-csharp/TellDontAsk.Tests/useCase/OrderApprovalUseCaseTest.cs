using Xunit;

public class OrderApprovalUseCaseTest
{
    private readonly TestOrderRepository orderRepository;
    private readonly OrderApprovalUseCase useCase;

    public OrderApprovalUseCaseTest()
    {
        orderRepository = new TestOrderRepository();
        useCase = new OrderApprovalUseCase(orderRepository);
    }

    [Fact]
    public void ApprovedExistingOrder()
    {
        Order initialOrder = new Order();
        initialOrder.SetStatus(OrderStatus.CREATED);
        initialOrder.SetId(1);
        orderRepository.AddOrder(initialOrder);

        OrderApprovalRequest request = new OrderApprovalRequest();
        request.SetOrderId(1);
        request.SetApproved(true);

        useCase.Run(request);

        Order savedOrder = orderRepository.GetSavedOrder();
        Assert.Equal(OrderStatus.APPROVED, savedOrder.GetStatus());
    }

    [Fact]
    public void RejectedExistingOrder()
    {
        Order initialOrder = new Order();
        initialOrder.SetStatus(OrderStatus.CREATED);
        initialOrder.SetId(1);
        orderRepository.AddOrder(initialOrder);

        OrderApprovalRequest request = new OrderApprovalRequest();
        request.SetOrderId(1);
        request.SetApproved(false);

        useCase.Run(request);

        Order savedOrder = orderRepository.GetSavedOrder();
        Assert.Equal(OrderStatus.REJECTED, savedOrder.GetStatus());
    }

    [Fact]
    public void CannotApproveRejectedOrder()
    {
        Order initialOrder = new Order();
        initialOrder.SetStatus(OrderStatus.REJECTED);
        initialOrder.SetId(1);
        orderRepository.AddOrder(initialOrder);

        OrderApprovalRequest request = new OrderApprovalRequest();
        request.SetOrderId(1);
        request.SetApproved(true);

        Assert.Throws<RejectedOrderCannotBeApprovedException>(() => useCase.Run(request));

        Assert.Null(orderRepository.GetSavedOrder());
    }

    [Fact]
    public void CannotRejectApprovedOrder()
    {
        Order initialOrder = new Order();
        initialOrder.SetStatus(OrderStatus.APPROVED);
        initialOrder.SetId(1);
        orderRepository.AddOrder(initialOrder);

        OrderApprovalRequest request = new OrderApprovalRequest();
        request.SetOrderId(1);
        request.SetApproved(false);

        Assert.Throws<ApprovedOrderCannotBeRejectedException>(() => useCase.Run(request));

        Assert.Null(orderRepository.GetSavedOrder());
    }

    [Fact]
    public void ShippedOrdersCannotBeApproved()
    {
        Order initialOrder = new Order();
        initialOrder.SetStatus(OrderStatus.SHIPPED);
        initialOrder.SetId(1);
        orderRepository.AddOrder(initialOrder);

        OrderApprovalRequest request = new OrderApprovalRequest();
        request.SetOrderId(1);
        request.SetApproved(true);

        Assert.Throws<ShippedOrdersCannotBeChangedException>(() => useCase.Run(request));

        Assert.Null(orderRepository.GetSavedOrder());
    }

    [Fact]
    public void ShippedOrdersCannotBeRejected()
    {
        Order initialOrder = new Order();
        initialOrder.SetStatus(OrderStatus.SHIPPED);
        initialOrder.SetId(1);
        orderRepository.AddOrder(initialOrder);

        OrderApprovalRequest request = new OrderApprovalRequest();
        request.SetOrderId(1);
        request.SetApproved(false);

        Assert.Throws<ShippedOrdersCannotBeChangedException>(() => useCase.Run(request));

        Assert.Null(orderRepository.GetSavedOrder());
    }
}