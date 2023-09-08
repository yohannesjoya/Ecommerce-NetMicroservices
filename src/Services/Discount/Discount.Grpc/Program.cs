using Discount.Grpc.Extensions;
using Discount.Grpc.repositories;
using System.Security.AccessControl;
using Microsoft.OpenApi.Any;
using Google.Protobuf.WellKnownTypes;
using Discount.Grpc.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddScoped<IDiscountRepository,DiscountRepository>();
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();
app.MigrateDb<AnyType>();

app.MapGrpcService<DiscountService>();
// Configure the HTTP request pipeline.
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
