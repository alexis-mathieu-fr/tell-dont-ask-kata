package it.gabrieletondi.telldontaskkata.domain


import java.math.BigDecimal

import java.math.BigDecimal.valueOf
import java.math.RoundingMode.HALF_UP

open class Product {
    var name: String? = null
    var price: BigDecimal? = null
    var category: Category? = null


}
