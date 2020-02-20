class OrderApprovalRequest

    attr_accessor :order_id, :approved

    def is_approved?
        return @approved
    end
end