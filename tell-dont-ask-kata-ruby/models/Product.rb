require_relative 'OrderItem'

class Product

    attr_accessor :name, :price, :category

    def initialize(name, price, category)
        @name = name
        @price = price
        @category = category
    end

    def get_unitary_tax
        return ((@price / 100.0) * category.tax_percentage).round(2)
    end

    def get_unitary_taxed_amount
        return @price + get_unitary_tax.round(2)
    end

    def get_taxed_amount(quantity)
        return (get_unitary_taxed_amount * quantity).round(2)
    end

    def construct_order_item(item_quantity)
        order_item = OrderItem.new()

        order_item.product = self
        order_item.quantity = item_quantity
        order_item.tax = get_unitary_tax * item_quantity
        order_item.taxed_amount = get_taxed_amount(item_quantity)

        return order_item
    end
end
