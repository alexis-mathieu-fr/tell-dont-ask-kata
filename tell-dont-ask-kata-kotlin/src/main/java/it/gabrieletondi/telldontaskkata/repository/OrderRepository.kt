package main.java.it.gabrieletondi.telldontaskkata.repository

import main.java.it.gabrieletondi.telldontaskkata.domain.Order

interface OrderRepository {
    fun save(order: Order)

    fun getById(orderId: Int): Order
}
