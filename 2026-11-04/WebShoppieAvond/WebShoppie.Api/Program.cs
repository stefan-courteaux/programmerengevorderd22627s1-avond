using Microsoft.EntityFrameworkCore;
using WebShoppie.Domain.Services.Customers;
using WebShoppie.Domain.Services.Orders;
using WebShoppie.Domain.Services.Products;
using WebShoppie.Storage.Customers;
using WebShoppie.Storage.DataModel;
using WebShoppie.Storage.Orders;
using WebShoppie.Storage.Products;

var builder = WebApplication.CreateBuilder(args);

// nodig voor Dapper variant, niet voor ef core
//builder.Services.Configure<DapperRepositoryOptions>(
//    builder.Configuration.GetSection(nameof(DapperRepositoryOptions)));

builder.Services.AddDbContext<WebShoppieDbContext>(
    optionsBuilder => optionsBuilder.UseNpgsql(
        builder.Configuration.GetConnectionString(
            "Postgres")) );

// AddSingleton - 1 object instantie die hergebruikt wordt zolang
// server runt voor elke vermelding interface in constructor
// AddScoped() - 1 instantie die hergebruikt wordt voor elke vermelding
// gedurende dit http request
// AddTransient() - elke keer interface vermeld wordt in constructor
// een nieuwe instantie

builder.Services.AddControllers();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICustomerRepository, EfCoreCustomerRepository>();
builder.Services.AddScoped<IOrderRepository, EfCoreOrderRepository>();
builder.Services.AddScoped<IProductRepository, EfCoreProductRepository>();

// Nodig voor dapper variant, niet voor ef core
// builder.Services.AddSingleton<ProductQueries>();

var app = builder.Build();
app.MapControllers();

app.Run();

