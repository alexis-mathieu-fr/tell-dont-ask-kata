using System.Collections.Generic;

public class OrderCreationUseCase
{
    private readonly OrderRepository orderRepository;
    private readonly ProductCatalog productCatalog;

    public OrderCreationUseCase(OrderRepository orderRepository, ProductCatalog productCatalog)
    {
        this.orderRepository = orderRepository;
        this.productCatalog = productCatalog;
    }

    public void Run(SellItemsRequest request)
    {
        Order order = new Order();
        order.SetStatus(OrderStatus.CREATED);
        order.SetItems(new List<OrderItem>());
        order.SetCurrency("EUR");
        order.SetTotal(0.00m);
        order.SetTax(0.00m);

        foreach (SellItemRequest itemRequest in request.GetRequests())
        {
            Product product = GetProduct(itemRequest);
            OrderItem orderItem = product.ConstructOrderItem(itemRequest.GetQuantity());

            order.AddItem(orderItem);
        }

        orderRepository.Save(order);
    }

    private Product GetProduct(SellItemRequest itemRequest)
    {
        Product product = productCatalog.GetByName(itemRequest.GetProductName());

        if (product == null)
        {
            throw new UnknownProductException();
        }

        return product;
    }
}