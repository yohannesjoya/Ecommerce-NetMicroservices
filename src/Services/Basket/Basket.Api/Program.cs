using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Discount.Grpc.Protos;
using static Discount.Grpc.Protos.DiscountProtoService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Console.WriteLine($"***************** {builder.Configuration.GetValue<string>("CashSettings:ConnectionString")}");
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CashSettings:ConnectionString");
});

builder.Services.AddScoped<ICartRepository, CartRepository>();


string url = builder.Configuration["GrpcSettings:DiscountUrl"];

Console.WriteLine($"***************** {url}");

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
                       (o => o.Address = new Uri(url));
builder.Services.AddScoped<DiscountGrpcServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
