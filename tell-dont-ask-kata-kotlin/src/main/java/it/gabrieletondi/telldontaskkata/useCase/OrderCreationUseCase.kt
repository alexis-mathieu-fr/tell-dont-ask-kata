package main.java.it.gabrieletondi.telldontaskkata.useCase


import main.java.it.gabrieletondi.telldontaskkata.domain.Order
import main.java.it.gabrieletondi.telldontaskkata.domain.OrderStatus
import main.java.it.gabrieletondi.telldontaskkata.domain.Product
import main.java.it.gabrieletondi.telldontaskkata.repository.OrderRepository
import main.java.it.gabrieletondi.telldontaskkata.repository.ProductCatalog

import java.math.BigDecimal
import java.util.ArrayList

class OrderCreationUseCase(private val orderRepository: OrderRepository, private val productCatalog: ProductCatalog) {

    fun run(request: SellItemsRequest) {
        val order = Order()
        order.status = OrderStatus.CREATED
        order.setItems(ArrayList())
        order.currency = "EUR"
        order.total = BigDecimal("0.00")
        order.tax = BigDecimal("0.00")

        for (itemRequest in request.requests!!) {
            val product = getProduct(itemRequest)
            val orderItem = product.constructOrderItem(itemRequest.quantity)

            order.addItem(orderItem)
        }

        orderRepository.save(order)
    }

    private fun getProduct(itemRequest: SellItemRequest): Product {

        return productCatalog.getByName(itemRequest.productName!!) ?: throw UnknownProductException()
    }
}
