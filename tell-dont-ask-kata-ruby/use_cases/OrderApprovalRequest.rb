class OrderApprovalRequest

    attr_accessor :order_id, :approved
    
    def initialize(order_id: nil, approved: false)
        @order_id = order_id
        @approved = approved
    end

    def is_approved?
        return @approved
    end
end