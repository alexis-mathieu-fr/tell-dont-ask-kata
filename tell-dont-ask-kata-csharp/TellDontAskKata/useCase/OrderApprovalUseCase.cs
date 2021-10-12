public class OrderApprovalUseCase
{
    private readonly OrderRepository orderRepository;

    public OrderApprovalUseCase(OrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public void Run(OrderApprovalRequest request)
    {
        Order order = orderRepository.GetById(request.GetOrderId());

        if (order.GetStatus().Equals(OrderStatus.SHIPPED))
        {
            throw new ShippedOrdersCannotBeChangedException();
        }

        if (request.IsApproved() && order.GetStatus().Equals(OrderStatus.REJECTED))
        {
            throw new RejectedOrderCannotBeApprovedException();
        }

        if (!request.IsApproved() && order.GetStatus().Equals(OrderStatus.APPROVED))
        {
            throw new ApprovedOrderCannotBeRejectedException();
        }

        order.SetStatus(request.IsApproved() ? OrderStatus.APPROVED : OrderStatus.REJECTED);
        orderRepository.Save(order);
    }
}