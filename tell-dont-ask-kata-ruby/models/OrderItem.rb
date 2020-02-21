class OrderItem
    
    attr_accessor :product, :quantity, :taxed_amount, :tax

    def initialize(product: 'Unknown', quantity: 0, taxed_amount: 0.00, tax: 0.00)
        @product = product
        @quantity = quantity
        @taxed_amount = taxed_amount
        @tax = tax
    end
end
