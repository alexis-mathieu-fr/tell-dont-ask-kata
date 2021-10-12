using System.Collections.Generic;
using Xunit;

public class OrderCreationUseCaseTest
{
    private readonly TestOrderRepository orderRepository;

    private readonly Category food;

    private readonly ProductCatalog productCatalog;
    private readonly OrderCreationUseCase useCase;

    public OrderCreationUseCaseTest()
    {
        orderRepository = new TestOrderRepository();

        food = new Category();
        food.SetName("food");
        food.SetTaxPercentage(10m);

        var salad = new Product();
        salad.SetName("salad");
        salad.SetPrice(3.56m);
        salad.SetCategory(food);

        var tomato = new Product();
        tomato.SetName("tomato");
        tomato.SetPrice(4.65m);
        tomato.SetCategory(food);

        productCatalog = new InMemoryProductCatalog(new List<Product>() { salad, tomato });
        useCase = new OrderCreationUseCase(orderRepository, productCatalog);
    }


    [Fact]
    public void SellMultipleItems()
    {
        SellItemRequest saladRequest = new SellItemRequest();
        saladRequest.SetProductName("salad");
        saladRequest.SetQuantity(2);

        SellItemRequest tomatoRequest = new SellItemRequest();
        tomatoRequest.SetProductName("tomato");
        tomatoRequest.SetQuantity(3);

        SellItemsRequest request = new SellItemsRequest();
        request.SetRequests(new List<SellItemRequest>());
        request.GetRequests().Add(saladRequest);
        request.GetRequests().Add(tomatoRequest);

        useCase.Run(request);

        Order insertedOrder = orderRepository.GetSavedOrder();
        Assert.Equal(OrderStatus.CREATED, insertedOrder.GetStatus());
        Assert.Equal(23.20m, insertedOrder.GetTotal());
        Assert.Equal(2.13m, insertedOrder.GetTax());
        Assert.Equal("EUR", insertedOrder.GetCurrency());
        Assert.Equal(2, insertedOrder.GetItems().Count);
        Assert.Equal("salad", insertedOrder.GetItems()[0].GetProduct().GetName());
        Assert.Equal(3.56m, insertedOrder.GetItems()[0].GetProduct().GetPrice());
        Assert.Equal(2, insertedOrder.GetItems()[0].GetQuantity());
        Assert.Equal(7.84m, insertedOrder.GetItems()[0].GetTaxedAmount());
        Assert.Equal(0.72m, insertedOrder.GetItems()[0].GetTax());
        Assert.Equal("tomato", insertedOrder.GetItems()[1].GetProduct().GetName());
        Assert.Equal(4.65m, insertedOrder.GetItems()[1].GetProduct().GetPrice());
        Assert.Equal(3, insertedOrder.GetItems()[1].GetQuantity());
        Assert.Equal(15.36m, insertedOrder.GetItems()[1].GetTaxedAmount());
        Assert.Equal(1.41m, insertedOrder.GetItems()[1].GetTax());
    }

    [Fact]
    public void UnknownProduct()
    {
        SellItemsRequest request = new SellItemsRequest();
        request.SetRequests(new List<SellItemRequest>());
        SellItemRequest unknownProductRequest = new SellItemRequest();
        unknownProductRequest.SetProductName("unknown product");
        request.GetRequests().Add(unknownProductRequest);

        Assert.Throws<UnknownProductException>(() =>useCase.Run(request));
    }
}