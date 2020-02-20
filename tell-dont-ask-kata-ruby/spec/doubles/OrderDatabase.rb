class OrderDatabase

    attr_accessor :inserted_order, :orders

    def initialize
        @orders = []
    end

    def save(order)
        @inserted_order = order
    end

    def find_by_id(order_id)
        @orders.select{ |order| order.id = order_id }.first
    end

    def add_order(order)
        @orders << order
    end
end