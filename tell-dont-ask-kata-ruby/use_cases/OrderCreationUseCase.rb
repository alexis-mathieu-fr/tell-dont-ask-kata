require_relative "UnknownProductError"

class OrderCreationUseCase

    attr_accessor :order_database, :product_catalog

    def initialize(order_database, product_catalog)
        @order_database = order_database
        @product_catalog = product_catalog
    end

    def run(request)
        order = Order.new()
        order.status = OrderStatus::CREATED
        order.items = []
        order.currency = "EUR"
        order.total = 0.00
        order.tax = 0.00

        request.requests.each do | item_request |
            product = get_product(item_request)
            order_item = product.construct_order_item(item_request.quantity)

            order.add_item(order_item)
        end

        @order_database.save(order)
    end

    def get_product(item_request)
        product = @product_catalog.find_by_name(item_request.product_name)

        if (product.nil?)
            raise UnknownProductError.new()
        end

        return product
    end
end