using System.Collections.Generic;
using System.Linq;

public class InMemoryProductCatalog : ProductCatalog
{
    private readonly List<Product> products;

    public InMemoryProductCatalog(List<Product> products)
    {
        this.products = products;
    }

    public Product GetByName(string name)
    {
        return products.FirstOrDefault(p => p.GetName().Equals(name));
    }
}