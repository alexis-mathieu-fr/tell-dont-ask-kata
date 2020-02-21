require_relative "../doubles/OrderDatabase"
require_relative "../doubles/ProductCatalog"
require_relative "../../models/OrderStatus"
require_relative "../../models/Product"
require_relative "../../models/Category"
require_relative "../../use_cases/OrderCreationUseCase"
require_relative "../../use_cases/SellItemRequest"
require_relative "../../use_cases/SellItemsRequest"

describe OrderCreationUseCase do

  let(:food) { Category.new(:name => "food", :tax_percentage => 10) }
  let(:order_database) { OrderDatabase.new() }
  let(:product_catalog) {
    ProductCatalog.new(
      :products => [
          Product.new(:name => "salad", :price => 3.56, :category => food),
          Product.new(:name => "tomato", :price => 4.65, :category => food)
      ]
    )
  }
  let(:use_case) {
    OrderCreationUseCase.new(
        :order_database => order_database,
        :product_catalog => product_catalog
    )
  }

  it "sets up an order with multiple items correctly" do
    salad_request = SellItemRequest.new(:product_name => "salad", :quantity => 2)
    tomato_request = SellItemRequest.new(:product_name => "tomato", :quantity => 3)

    sell_items_request = SellItemsRequest.new(:requests => [salad_request, tomato_request])

    use_case.run(:request => sell_items_request)

    inserted_order = order_database.inserted_order

    expect(inserted_order.status).to eq(OrderStatus::CREATED)
    expect(inserted_order.total).to eq(23.20)
    expect(inserted_order.tax).to eq(2.13)
    expect(inserted_order.currency).to eq("EUR")
    expect(inserted_order.items.size).to eq(2)
    expect(inserted_order.items[0].product.name).to eq("salad")
    expect(inserted_order.items[0].product.price).to eq(3.56)
    expect(inserted_order.items[0].quantity).to eq(2)
    expect(inserted_order.items[0].taxed_amount).to eq(7.84)
    expect(inserted_order.items[0].tax).to eq(0.72)
    expect(inserted_order.items[1].product.name).to eq("tomato")
    expect(inserted_order.items[1].product.price).to eq(4.65)
    expect(inserted_order.items[1].quantity).to eq(3)
    expect(inserted_order.items[1].taxed_amount).to eq(15.36)
    expect(inserted_order.items[1].tax).to eq(1.41)
  end

  it "throws unknown product error if the product is not in the catalog" do
    unknown_item_request = SellItemRequest.new(:product_name => "unknown product")

    sell_items_request = SellItemsRequest.new(:requests => [unknown_item_request])

    expect { use_case.run(:request => sell_items_request)}.to raise_error("UnknownProductError")

  end
end