using FluentValidation;
using InternetShop.Data;
using InternetShop.DTOs.Validators.Categories;
using InternetShop.DTOs.Validators.Orders;
using InternetShop.DTOs.Validators.Products;
using InternetShop.Middleware;
using InternetShop.Repositories;
using InternetShop.Repositories.Interfaces;
using InternetShop.Services;
using InternetShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseSnakeCaseNamingConvention());

builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
