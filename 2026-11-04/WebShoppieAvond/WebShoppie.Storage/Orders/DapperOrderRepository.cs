using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using WebShoppie.Storage.DataModel;
using WebShoppie.Storage.DataModel.Entities;

namespace WebShoppie.Storage.Orders;

public class DapperOrderRepository(IOptions<DapperRepositoryOptions> options) : IOrderRepository
{
    private readonly string _connectionString = options.Value.ConnectionString;

    public OrderDataModel Create(OrderDataModel order, IEnumerable<OrderproductDataModel> orderProducts)
    {
        const string insertOrderSql =
            """
            INSERT INTO storefront.orders(orderdate, customerid, totalprice)
            VALUES (@OrderDate, @CustomerId, @TotalPrice)
            RETURNING OrderId
            """;

        const string insertOrderProductsSql =
            """
            INSERT INTO storefront.orderproducts(orderid, productid, quantity, price)
            VALUES (@OrderId, @ProductId, @Quantity, @Price)
            """;

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var transaction = connection.BeginTransaction();
        var orderId = connection.ExecuteScalar<int>(insertOrderSql, order, transaction);
        var orderProductParameters = orderProducts.Select(orderProduct => new
        {
            OrderId = orderId,
            orderProduct.Productid,
            orderProduct.Quantity,
            orderProduct.Price
        });

        connection.Execute(insertOrderProductsSql, orderProductParameters, transaction);
        transaction.Commit();

        return Get(orderId) ?? throw new InvalidOperationException(
            $"Order {orderId} could not be created");
    }

    public OrderDataModel? Get(int id)
    {
        const string sql = "SELECT * FROM storefront.orders WHERE OrderId = @id";

        using var connection = new NpgsqlConnection(_connectionString);
        return connection.QuerySingleOrDefault<OrderDataModel>(sql, new { id });
    }

    public IEnumerable<OrderproductDataModel> GetOrderProducts(int orderId)
    {
        const string sql = "SELECT * FROM storefront.orderproducts WHERE OrderId = @orderId";

        using var connection = new NpgsqlConnection(_connectionString);
        return connection.Query<OrderproductDataModel>(sql, new { orderId });
    }

    public IEnumerable<OrderDataModel> GetAll()
    {
        const string sql = "SELECT * FROM storefront.orders";

        using var connection = new NpgsqlConnection(_connectionString);
        return connection.Query<OrderDataModel>(sql);
    }
}