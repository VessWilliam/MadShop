using Discount.GRPC.Data;
using Discount.GRPC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<DiscountContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddGrpcReflection();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMigrations();


app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints");
app.MapGrpcReflectionService();

app.Run();
