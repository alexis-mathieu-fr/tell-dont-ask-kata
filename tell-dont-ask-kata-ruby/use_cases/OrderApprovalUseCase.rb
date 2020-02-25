require_relative "RejectedOrderCannotBeApprovedError"
require_relative "ShippedOrdersCannotBeChangedError"
require_relative "ApprovedOrderCannotBeRejectedError"

class OrderApprovalUseCase

    attr_accessor :order_database

    def initialize(order_database:)
        @order_database = order_database
    end

    def run(request:)
        order = @order_database.find_by_id(:order_id => request.order_id)

        if (order.status == OrderStatus::SHIPPED)
            raise ShippedOrdersCannotBeChangedError.new
        end

        if (request.is_approved? && order.status == OrderStatus::REJECTED)
            raise RejectedOrderCannotBeApprovedError.new
        end

        if (!request.is_approved? && order.status == OrderStatus::APPROVED)
            raise ApprovedOrderCannotBeRejectedError.new
        end

        order.status = request.is_approved? ? OrderStatus::APPROVED : OrderStatus::REJECTED
        @order_database.save(:order => order)
    end
end