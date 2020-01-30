package main.java.it.gabrieletondi.telldontaskkata.repository

import main.java.it.gabrieletondi.telldontaskkata.domain.Product

interface ProductCatalog {
    fun getByName(name: String): Product?
}
