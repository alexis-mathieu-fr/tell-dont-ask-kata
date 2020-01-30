package it.gabrieletondi.telldontaskkata.useCase


import it.gabrieletondi.telldontaskkata.domain.Order
import it.gabrieletondi.telldontaskkata.domain.OrderItem
import it.gabrieletondi.telldontaskkata.domain.OrderStatus
import it.gabrieletondi.telldontaskkata.domain.Product
import it.gabrieletondi.telldontaskkata.repository.OrderRepository
import it.gabrieletondi.telldontaskkata.repository.ProductCatalog
import java.math.BigDecimal
import java.math.BigDecimal.valueOf
import java.math.RoundingMode.HALF_UP
import java.util.ArrayList

class OrderCreationUseCase(private val orderRepository: OrderRepository, private val productCatalog: ProductCatalog) {

    fun run(request: SellItemsRequest) {
        val order = Order()
        order.status = OrderStatus.CREATED
        order.items =ArrayList()
        order.currency = "EUR"
        order.total = BigDecimal("0.00")
        order.tax = BigDecimal("0.00")

        for (itemRequest in request.requests!!) {
            val product = getProduct(itemRequest)
            val orderItem = OrderItem()
            orderItem.product = product
            orderItem.quantity = itemRequest.quantity
            orderItem.tax = product.price!!.divide(valueOf(100))
                .multiply(product.category!!.taxPercentage!!).setScale(2, HALF_UP).multiply(BigDecimal.valueOf(itemRequest.quantity.toLong()))
            orderItem.taxedAmount =
                product.price!!.add(
                    product.price!!.divide(valueOf(100))
                        .multiply(product.category!!.taxPercentage!!).setScale(2, HALF_UP)
                ).setScale(2, HALF_UP).multiply(BigDecimal.valueOf(itemRequest.quantity.toLong())).setScale(2, HALF_UP)

            order.items!!.add(orderItem)
            order.total = order.total!!.add(orderItem.taxedAmount!!)
            order.tax = order.tax!!.add(orderItem.tax!!)
        }

        orderRepository.save(order)
    }

    private fun getProduct(itemRequest: SellItemRequest): Product {

        return productCatalog.getByName(itemRequest.productName!!) ?: throw UnknownProductException()
    }
}
