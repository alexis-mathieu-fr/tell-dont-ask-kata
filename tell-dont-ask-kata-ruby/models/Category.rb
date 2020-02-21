class Category

    attr_accessor :name, :tax_percentage

    def initialize(name: 'Name', tax_percentage: 0.00)
        @name = name
        @tax_percentage = tax_percentage
    end

end
