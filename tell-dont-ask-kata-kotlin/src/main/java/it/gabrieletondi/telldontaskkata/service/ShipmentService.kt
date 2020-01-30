package main.java.it.gabrieletondi.telldontaskkata.service

import main.java.it.gabrieletondi.telldontaskkata.domain.Order

interface ShipmentService {
    fun ship(order: Order)
}
