using InternetShop.Models;
using InternetShop.Repositories;
using InternetShop.Services;

var productRepository = new ProductRepository();
var orderRepository = new OrderRepository();

var paymentService = new PaymentService();

var warehouseService = new WarehouseService(productRepository);

var emailService = new EmailService();

var analyticsService = new AnalyticsService();

var orderService = new OrderService(
    orderRepository,
    warehouseService,
    paymentService);


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IProductRepository<Product>, ProductRepository>();

builder.Services.AddSingleton<IProductService<Product>, ProductService>();

var app = builder.Build();


orderService.OrderCreated += emailService.SendEmail;
orderService.OrderCreated += analyticsService.AddOrderToStatistics;

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
