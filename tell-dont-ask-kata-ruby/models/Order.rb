class Order
    attr_accessor :total, :currency, :items, :tax, :status, :id

    def initialize(total: 0.00, currency: 'Unknown', items: [], tax: 0.00, status: OrderStatus::CREATED, id: nil)
        @total = total
        @currency = currency
        @items = items
        @tax = tax
        @status = status
        @id = id
    end

    def add_item(order_item:)
        @items << order_item
        @total = total + order_item.taxed_amount
        @tax = tax + order_item.tax
    end

end
