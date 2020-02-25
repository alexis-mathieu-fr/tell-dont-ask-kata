class OrderDatabase

    attr_accessor :inserted_order, :orders

    def initialize(orders: [], inserted_order: nil)
        @orders = orders
        @inserted_order = inserted_order
    end

    def save(order:)
        @inserted_order = order
    end

    def find_by_id(order_id:)
        @orders.select{ |order| order.id = order_id }.first
    end

    def add_order(order:)
        @orders << order
    end
end