require_relative "../../use_cases/OrderShipmentUseCase"
require_relative "../../use_cases/OrderShipmentRequest"

describe OrderShipmentUseCase do
  let(:order_database) { OrderDatabase.new() }
  let(:shipment_service) { ShipmentService.new() }
  let(:use_case) { OrderShipmentUseCase.new(:order_database => order_database, :shipment_service => shipment_service) }

  it "ships approved orders" do
    initial_order = Order.new(:id => 1, :status => OrderStatus::APPROVED)
    order_database.add_order(:order => initial_order)

    order_shipment_request = OrderShipmentRequest.new(:order_id => 1)

    use_case.run(:request => order_shipment_request)

    expect(order_database.inserted_order.status).to be(OrderStatus::SHIPPED)
    expect(shipment_service.shipped_order).to be(initial_order)
  end

  it "will not ship a created order" do
    initial_order = Order.new(:id => 1)
    order_database.add_order(:order => initial_order)

    order_shipment_request = OrderShipmentRequest.new(:order_id => 1)

    expect {use_case.run(:request => order_shipment_request)}.to raise_error("OrderCannotBeShippedError")

  end

  it "will not ship a rejected order" do
    initial_order = Order.new(:id => 1, :status => OrderStatus::REJECTED)
    order_database.add_order(:order => initial_order)

    order_shipment_request = OrderShipmentRequest.new(:order_id => 1)

    expect {use_case.run(:request => order_shipment_request)}.to raise_error("OrderCannotBeShippedError")

  end

  it "will not ship an already shipped order" do
    initial_order = Order.new(:id => 1, :status => OrderStatus::SHIPPED)
    order_database.add_order(:order => initial_order)

    order_shipment_request = OrderShipmentRequest.new(:order_id => 1)

    expect {use_case.run(:request => order_shipment_request)}.to raise_error("OrderCannotBeShippedTwiceError")

  end
end