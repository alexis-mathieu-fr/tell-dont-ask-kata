class Order
    attr_accessor :total, :currency, :items, :tax, :status, :id

    def add_item(order_item)
        @items << order_item
        @total = total + order_item.taxed_amount
        @tax = tax + order_item.tax
    end

end
