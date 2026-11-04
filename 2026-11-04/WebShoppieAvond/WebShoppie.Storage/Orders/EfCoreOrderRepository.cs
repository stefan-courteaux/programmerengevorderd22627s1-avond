using Microsoft.EntityFrameworkCore;
using WebShoppie.Storage.DataModel;
using WebShoppie.Storage.DataModel.Entities;

namespace WebShoppie.Storage.Orders;

public class EfCoreOrderRepository(WebShoppieDbContext _dbContext) : IOrderRepository
{
    public OrderDataModel Create(OrderDataModel order, IEnumerable<OrderproductDataModel> orderProducts)
    {
        order.Orderproducts = orderProducts.ToList();
        
        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
        
        return order;
    }

    public OrderDataModel? Get(int id)
    {
        return _dbContext.Orders.Find(id);
    }

    public IEnumerable<OrderproductDataModel> GetOrderProducts(int orderId)
    {
        return _dbContext.Orderproducts
            .Where(op => op.Orderid == orderId);
    }

    public IEnumerable<OrderDataModel> GetAll()
    {
        return _dbContext.Orders;
    }
    
    // Ter demonstratie: alles ineens uitlezen met Ef Core
    public OrderDataModel? GetFullOrder(int orderId)
    {
        var fullOrder = _dbContext.Orders
            .Include(o => o.CustomerDataModel)
            .Include(o => o.Orderproducts)
            .ThenInclude(op => op.ProductDataModel)
            .Single(o => o.Orderid == orderId);
        return fullOrder;
    }
    
}