package main.java.it.gabrieletondi.telldontaskkata.domain

import java.math.BigDecimal

import java.math.BigDecimal.valueOf
import java.math.RoundingMode.HALF_UP

open class Product {
    var name: String? = null
    var price: BigDecimal? = null
    private var category: Category? = null

    val unitaryTax: BigDecimal
        get() = price!!.divide(valueOf(100))
            .multiply(category!!.taxPercentage!!).setScale(2, HALF_UP)

    val unitaryTaxedAmount: BigDecimal
        get() = price!!.add(unitaryTax).setScale(2, HALF_UP)

    fun setCategory(category: Category) {
        this.category = category
    }

    fun getTaxedAmount(quantity: Int): BigDecimal {
        return unitaryTaxedAmount.multiply(BigDecimal.valueOf(quantity.toLong())).setScale(2, HALF_UP)
    }

    fun constructOrderItem(quantity: Int): OrderItem {
        val orderItem = OrderItem()
        orderItem.product = this
        orderItem.quantity = quantity
        orderItem.tax = unitaryTax.multiply(BigDecimal.valueOf(quantity.toLong()))
        orderItem.taxedAmount = getTaxedAmount(quantity)
        return orderItem
    }
}
