public class Category
{
    private string name;
    private decimal taxPercentage;

    public string GetName()
    {
        return name;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public decimal GetTaxPercentage()
    {
        return taxPercentage;
    }

    public void SetTaxPercentage(decimal taxPercentage)
    {
        this.taxPercentage = taxPercentage;
    }
}