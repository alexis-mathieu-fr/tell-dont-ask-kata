package test.java.it.gabrieletondi.telldontaskkata.doubles

import main.java.it.gabrieletondi.telldontaskkata.domain.Order
import main.java.it.gabrieletondi.telldontaskkata.service.ShipmentService


class TestShipmentService : ShipmentService {
    var shippedOrder: Order? = null
        private set

    override fun ship(order: Order) {
        this.shippedOrder = order
    }
}
