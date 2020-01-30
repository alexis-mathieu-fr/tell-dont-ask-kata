package test.java.it.gabrieletondi.telldontaskkata.useCase


import main.java.it.gabrieletondi.telldontaskkata.domain.Category
import main.java.it.gabrieletondi.telldontaskkata.domain.OrderStatus
import main.java.it.gabrieletondi.telldontaskkata.domain.Product
import main.java.it.gabrieletondi.telldontaskkata.useCase.OrderCreationUseCase
import main.java.it.gabrieletondi.telldontaskkata.useCase.SellItemRequest
import main.java.it.gabrieletondi.telldontaskkata.useCase.SellItemsRequest
import main.java.it.gabrieletondi.telldontaskkata.useCase.UnknownProductException
import org.junit.Test

import java.math.BigDecimal
import java.util.ArrayList
import java.util.Arrays

import org.hamcrest.Matchers.hasSize
import org.hamcrest.Matchers.`is`
import org.junit.Assert.assertThat
import test.java.it.gabrieletondi.telldontaskkata.doubles.InMemoryProductCatalog
import test.java.it.gabrieletondi.telldontaskkata.doubles.TestOrderRepository

class OrderCreationUseCaseTest {
    private val orderRepository = TestOrderRepository()
    private val food = object : Category() {
        init {
            name = "food"
            taxPercentage = BigDecimal("10")
        }
    }
    private val productCatalog = InMemoryProductCatalog(
        Arrays.asList<Product>(
            object : Product() {
                init {
                    name = "salad"
                    price = BigDecimal("3.56")
                    setCategory(food)
                }
            },
            object : Product() {
                init {
                    name = "tomato"
                    price = BigDecimal("4.65")
                    setCategory(food)
                }
            }
        )
    )
    private val useCase = OrderCreationUseCase(orderRepository, productCatalog)

    @Test
    @Throws(Exception::class)
    fun sellMultipleItems() {
        val saladRequest = SellItemRequest()
        saladRequest.productName = "salad"
        saladRequest.quantity = 2

        val tomatoRequest = SellItemRequest()
        tomatoRequest.productName = "tomato"
        tomatoRequest.quantity = 3

        val request = SellItemsRequest()

        (request.requests as ArrayList<SellItemRequest>).add(saladRequest)
        (request.requests as ArrayList<SellItemRequest>).add(tomatoRequest)

        useCase.run(request)

        val insertedOrder = orderRepository.savedOrder
        assertThat(insertedOrder!!.status, `is`(OrderStatus.CREATED))
        assertThat(insertedOrder.total, `is`(BigDecimal("23.20")))
        assertThat(insertedOrder.tax, `is`(BigDecimal("2.13")))
        assertThat(insertedOrder.currency, `is`("EUR"))
        assertThat(insertedOrder.getItems(), hasSize(2))
        assertThat(insertedOrder.getItems()!![0].product!!.name, `is`("salad"))
        assertThat(insertedOrder.getItems()!![0].product!!.price, `is`(BigDecimal("3.56")))
        assertThat(insertedOrder.getItems()!![0].quantity, `is`(2))
        assertThat(insertedOrder.getItems()!![0].taxedAmount, `is`(BigDecimal("7.84")))
        assertThat(insertedOrder.getItems()!![0].tax, `is`(BigDecimal("0.72")))
        assertThat(insertedOrder.getItems()!![1].product!!.name, `is`("tomato"))
        assertThat(insertedOrder.getItems()!![1].product!!.price, `is`(BigDecimal("4.65")))
        assertThat(insertedOrder.getItems()!![1].quantity, `is`(3))
        assertThat(insertedOrder.getItems()!![1].taxedAmount, `is`(BigDecimal("15.36")))
        assertThat(insertedOrder.getItems()!![1].tax, `is`(BigDecimal("1.41")))
    }

    @Test(expected = UnknownProductException::class)
    @Throws(Exception::class)
    fun unknownProduct() {
        val request = SellItemsRequest()
        request.requests = ArrayList()
        val unknownProductRequest = SellItemRequest()
        unknownProductRequest.productName = "unknown product"
        (request.requests as ArrayList<SellItemRequest>).add(unknownProductRequest)

        useCase.run(request)
    }
}
