package main.java.it.gabrieletondi.telldontaskkata.useCase

import main.java.it.gabrieletondi.telldontaskkata.domain.OrderStatus
import main.java.it.gabrieletondi.telldontaskkata.repository.OrderRepository
import main.java.it.gabrieletondi.telldontaskkata.service.ShipmentService


class OrderShipmentUseCase(private val orderRepository: OrderRepository, private val shipmentService: ShipmentService) {

    fun run(request: OrderShipmentRequest) {
        val order = orderRepository.getById(request.orderId)

        if (order.status!! == OrderStatus.CREATED || order.status!! == OrderStatus.REJECTED) {
            throw OrderCannotBeShippedException()
        }

        if (order.status!! == OrderStatus.SHIPPED) {
            throw OrderCannotBeShippedTwiceException()
        }

        shipmentService.ship(order)

        order.status = OrderStatus.SHIPPED
        orderRepository.save(order)
    }
}
