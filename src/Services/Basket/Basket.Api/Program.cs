using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Discount.Grpc.Protos;
using MassTransit;
using System.Reflection;
using static Discount.Grpc.Protos.DiscountProtoService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);


// REDIS  --------------------------------------------------------------
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CashSettings:ConnectionString");
});

// Repo Config  --------------------------------------------------------------
builder.Services.AddScoped<ICartRepository, CartRepository>();


//GRPC --------------------------------------------------------------
string url = builder.Configuration["GrpcSettings:DiscountUrl"];

//Console.WriteLine($"***************** {url}");

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
                       (o => o.Address = new Uri(url));
builder.Services.AddScoped<DiscountGrpcServices>();

// RabbitMQ  --------------------------------------------------------------

string rabbitMqHost = builder.Configuration["EventBusSettings:HostAddress"];

Console.WriteLine("=================================================");
Console.WriteLine(rabbitMqHost);
Console.WriteLine("=================================================");

builder.Services.AddMassTransit(config => {
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        //cfg.UseHealthCheck(ctx);
    });
});



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
