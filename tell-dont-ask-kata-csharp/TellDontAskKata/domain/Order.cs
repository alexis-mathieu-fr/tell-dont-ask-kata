using System;
using System.Collections.Generic;

public class Order
{
    private decimal total;
    private string currency;
    private List<OrderItem> items;
    private decimal tax;
    private OrderStatus status;
    private int id;

    public decimal GetTotal()
    {
        return total;
    }

    public void SetTotal(decimal total)
    {
        this.total = total;
    }

    public string GetCurrency()
    {
        return currency;
    }

    public void SetCurrency(string currency)
    {
        this.currency = currency;
    }

    public List<OrderItem> GetItems()
    {
        return items;
    }

    public void SetItems(List<OrderItem> items)
    {
        this.items = items;
    }

    public decimal GetTax()
    {
        return tax;
    }

    public void SetTax(decimal tax)
    {
        this.tax = tax;
    }

    public OrderStatus GetStatus()
    {
        return status;
    }

    public void SetStatus(OrderStatus status)
    {
        this.status = status;
    }

    public int GetId()
    {
        return id;
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    public void AddItem(OrderItem orderItem)
    {
        items.Add(orderItem);
        total += orderItem.GetTaxedAmount();
        tax += orderItem.GetTax();
    }
}