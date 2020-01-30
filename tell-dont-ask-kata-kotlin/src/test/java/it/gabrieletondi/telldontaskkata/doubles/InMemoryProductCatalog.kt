package test.java.it.gabrieletondi.telldontaskkata.doubles


import main.java.it.gabrieletondi.telldontaskkata.domain.Product
import main.java.it.gabrieletondi.telldontaskkata.repository.ProductCatalog

class InMemoryProductCatalog(private val products: List<Product>) : ProductCatalog {

    override fun getByName(name: String): Product? {
        return products.stream().filter { p -> p.name.equals(name) }.findFirst().orElse(null)
    }
}
