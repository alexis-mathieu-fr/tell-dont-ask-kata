package test.java.it.gabrieletondi.telldontaskkata.doubles



import main.java.it.gabrieletondi.telldontaskkata.domain.Order
import main.java.it.gabrieletondi.telldontaskkata.repository.OrderRepository
import java.util.ArrayList

class TestOrderRepository : OrderRepository {
    var savedOrder: Order? = null
        private set
    private val orders = ArrayList<Order>()

    override fun save(order: Order) {
        this.savedOrder = order
    }

    override fun getById(orderId: Int): Order {
        return orders.stream().filter { o -> o.id == orderId }.findFirst().get()
    }

    fun addOrder(order: Order) {
        this.orders.add(order)
    }
}
