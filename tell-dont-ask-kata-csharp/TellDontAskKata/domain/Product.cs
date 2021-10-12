using System;

public class Product
{
    private string name;
    private decimal price;
    private Category category;

    public string GetName()
    {
        return name;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public decimal GetPrice()
    {
        return price;
    }

    public void SetPrice(decimal price)
    {
        this.price = price;
    }

    public void SetCategory(Category category)
    {
        this.category = category;
    }

    public decimal GetUnitaryTax()
    {
        return Math.Round(price / 100 * category.GetTaxPercentage(), 2, MidpointRounding.AwayFromZero);
    }

    public decimal GetUnitaryTaxedAmount()
    {
        return Math.Round(price + GetUnitaryTax(),2, MidpointRounding.AwayFromZero);
    }

    public decimal GetTaxedAmount(int quantity)
    {
        return Math.Round(GetUnitaryTaxedAmount() * Convert.ToDecimal(quantity), 2, MidpointRounding.AwayFromZero);
    }

    public OrderItem ConstructOrderItem(int quantity)
    {
        OrderItem orderItem = new OrderItem();
        orderItem.SetProduct(this);
        orderItem.SetQuantity(quantity);
        orderItem.SetTax(GetUnitaryTax() * quantity);
        orderItem.SetTaxedAmount(GetTaxedAmount(quantity));
        return orderItem;
    }
}