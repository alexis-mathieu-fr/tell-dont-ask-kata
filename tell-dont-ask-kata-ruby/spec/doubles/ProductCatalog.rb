class ProductCatalog

    attr_accessor :products

    def initialize(products)
        @products = products
    end

    def find_by_name(name)
        products.select { |product| product.name == name }.first
    end
end