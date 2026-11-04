using WebShoppie.Storage.DataModel;
using WebShoppie.Storage.DataModel.Entities;

namespace WebShoppie.Storage.Orders;

public interface IOrderRepository
{
    OrderDataModel Create(OrderDataModel order, IEnumerable<OrderproductDataModel> orderProducts);
    OrderDataModel? Get(int id);
    IEnumerable<OrderproductDataModel> GetOrderProducts(int orderId);
    IEnumerable<OrderDataModel> GetAll();
}