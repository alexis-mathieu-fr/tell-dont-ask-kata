require_relative 'OrderItem'

class Product

    attr_accessor :name, :price, :category

    def initialize(name: 'Product Name', price: 1.25, category: 'Category')
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

    def get_taxed_amount(quantity:)
        return (get_unitary_taxed_amount * quantity).round(2)
    end

    def construct_order_item(item_quantity:)
        order_item = OrderItem.new(
            :product => self,
            :quantity => item_quantity,
            :tax => get_unitary_tax * item_quantity,
            :taxed_amount => get_taxed_amount(:quantity => item_quantity)
        )

        return order_item
    end
end
