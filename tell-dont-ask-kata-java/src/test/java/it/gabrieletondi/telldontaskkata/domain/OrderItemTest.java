package it.gabrieletondi.telldontaskkata.domain;

import org.junit.Test;

import java.math.BigDecimal;

import static org.junit.Assert.*;


public class OrderItemTest {
    @Test
    public void shouldComputeTax() {
        Category food = new Category();
        food.setTaxPercentage(BigDecimal.valueOf(10));

        Product prod1 = new Product();
        prod1.setPrice(BigDecimal.valueOf(3.56));
        prod1.setCategory(food);

        OrderItem item = new OrderItem();
        item.setQuantity(2);
        item.setProduct(prod1);
        BigDecimal tax = item.computeTax();

        assertEquals(BigDecimal.valueOf(0.72), tax);
    }
}