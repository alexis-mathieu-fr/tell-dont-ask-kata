class Category

    attr_accessor :name, :tax_percentage

    def initialize(name, tax_percentage)
        @name = name
        @tax_percentage = tax_percentage
    end

end
