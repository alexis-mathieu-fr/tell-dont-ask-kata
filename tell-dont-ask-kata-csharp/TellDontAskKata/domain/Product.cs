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
        return price / 100 * Math.Round(category.GetTaxPercentage(), 2, MidpointRounding.ToEven);
    }

    public decimal GetUnitaryTaxedAmount()
    {
        return price + GetUnitaryTax();
    }

    public decimal GetTaxedAmount(int quantity)
    {
        return GetUnitaryTaxedAmount() * Math.Round(Convert.ToDecimal(quantity), 2, MidpointRounding.ToEven);
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