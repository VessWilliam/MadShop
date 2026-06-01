using Basket.API.Data.Service;
using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//Add service to the container  
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();


builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});


builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShopCart>().Identity(x => x.UserName);
}).UseLightweightSessions();


builder.Services.AddScoped<IBasketRespository, BasketRespository>();
builder.Services.Decorate<IBasketRespository, CachedBasketRespository>();

//Grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
{
    opt.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});


//Cross Cutting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
     .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
     .AddRedis(builder.Configuration.GetConnectionString("Redis")!);


builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis")!;
    //opt.InstanceName = "BasketAPI";
});


var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(opts => { });

// health Endpoint
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
