class SellItemRequest

  attr_accessor :quantity, :product_name

  def initialize(quantity: 0, product_name: 'Product Name')
      @quantity = quantity
      @product_name = product_name
  end
end