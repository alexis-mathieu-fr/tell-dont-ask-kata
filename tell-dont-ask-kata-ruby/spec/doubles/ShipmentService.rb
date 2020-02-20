class ShipmentService
    attr_accessor :shipped_order

    def ship(order)
        @shipped_order = order
    end
end