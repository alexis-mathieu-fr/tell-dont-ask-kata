require_relative "../doubles/OrderDatabase"
require_relative "../../models/OrderStatus"
require_relative "../../models/Order"
require_relative "../../use_cases/OrderApprovalUseCase"
require_relative "../../use_cases/OrderApprovalRequest"

describe OrderApprovalUseCase do

  let(:order_database) { OrderDatabase.new() }
  let(:use_case) { OrderApprovalUseCase.new(:order_database => order_database) }

  it "approves existing un-approved orders" do
    initial_order = Order.new(:id => 1)
    order_database.add_order(:order => initial_order)

    order_approval_request = OrderApprovalRequest.new(:order_id => 1, :approved => true)

    use_case.run(:request => order_approval_request)

    saved_order = order_database.inserted_order
    expect(saved_order.status).to eq(OrderStatus::APPROVED)
  end

  it "rejects an order approval request that is not approved" do
    initial_order = Order.new(:id => 1)
    order_database.add_order(:order => initial_order)

    order_approval_request = OrderApprovalRequest.new(:order_id => 1, :approved => false)

    use_case.run(:request => order_approval_request)

    saved_order = order_database.inserted_order
    expect(saved_order.status).to eq(OrderStatus::REJECTED)
  end

  it "does not approve rejected orders" do
    initial_order = Order.new(:id => 1, :status => OrderStatus::REJECTED)
    order_database.add_order(:order => initial_order)

    order_approval_request = OrderApprovalRequest.new(:order_id => 1, :approved => true)

    expect { use_case.run(:request => order_approval_request) }.to raise_error("RejectedOrderCannotBeApprovedError")
  end

  it "does not reject approved orders" do
    initial_order = Order.new(:id => 1, :status => OrderStatus::APPROVED)
    order_database.add_order(:order => initial_order)

    order_approval_request = OrderApprovalRequest.new(:order_id => 1, :approved => false)

    expect { use_case.run(:request => order_approval_request) }.to raise_error("ApprovedOrderCannotBeRejectedError")
  end

  it "does not approve shipped orders" do
    initial_order = Order.new(:status => OrderStatus::SHIPPED, :id => 1)
    order_database.add_order(:order => initial_order)

    order_approval_request = OrderApprovalRequest.new(:order_id => 1, :approved => true)

    expect { use_case.run(:request => order_approval_request) }.to raise_error("ShippedOrdersCannotBeChangedError")
  end

  it "does not reject shipped orders" do
    initial_order = Order.new(:status => OrderStatus::SHIPPED, :id => 1)
    order_database.add_order(:order => initial_order)

    order_approval_request = OrderApprovalRequest.new(:order_id => 1, :approved => false)

    expect { use_case.run(:request => order_approval_request) }.to raise_error("ShippedOrdersCannotBeChangedError")
  end
end