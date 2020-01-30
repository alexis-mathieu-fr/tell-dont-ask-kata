package main.java.it.gabrieletondi.telldontaskkata.domain

import java.math.BigDecimal

class Order {
    var total: BigDecimal? = null
    var currency: String? = null
    private var items: MutableList<OrderItem>? = null
    var tax: BigDecimal? = null
    var status: OrderStatus? = null
    var id: Int = 0

    fun getItems(): List<OrderItem>? {
        return items
    }

    fun setItems(items: MutableList<OrderItem>) {
        this.items = items
    }

    fun addItem(orderItem: OrderItem) {
        items!!.add(orderItem)
        total = total!!.add(orderItem.taxedAmount!!)
        tax = tax!!.add(orderItem.tax!!)
    }
}
