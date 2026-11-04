using WebShoppie.Api.Contracts.Customers;
using WebShoppie.Api.Contracts.Orders;
using WebShoppie.Api.Contracts.Products;
using WebShoppie.Storage.DataModel;
using WebShoppie.Storage.DataModel.Entities;

namespace WebShoppie.Domain.Services.Orders;

public static class MappingExtensions
{
    public static OrderResponseContract AsContract(
        this OrderDataModel order,
        IEnumerable<OrderProductResponseContract> orderProducts,
        CustomerResponseContract customer)
    {
        return new OrderResponseContract
        {
            OrderId = order.Orderid,
            OrderDate = order.Orderdate,
            CustomerId = order.Customerid,
            TotalPrice = order.Totalprice,
            CustomerEmail = customer.Email,
            OrderProducts = orderProducts
        };
    }

    public static OrderProductResponseContract AsContract(
        this OrderproductDataModel orderProduct,
        string productName)
    {
        return new OrderProductResponseContract
        {
            OrderProductId = orderProduct.Orderproductid,
            OrderId = orderProduct.Orderid,
            ProductId = orderProduct.Productid,
            ProductName = productName,
            Quantity = orderProduct.Quantity,
            Price = orderProduct.Price
        };
    }
}