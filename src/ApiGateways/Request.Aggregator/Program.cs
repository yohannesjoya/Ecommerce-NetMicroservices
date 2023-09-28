using Request.Aggregator.Services;
using Request.Aggregator.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string catalogBaseUrl = builder.Configuration["ApiSettings:CatalogBaseUrl"];
string basketBaseUrl = builder.Configuration["ApiSettings:BasketBaseUrl"];
string orderBaseUrl = builder.Configuration["ApiSettings:OrderBaseUrl"];


builder.Services.AddHttpClient<ICatalogService, CatalogService>(client =>  client.BaseAddress = new Uri(catalogBaseUrl));
builder.Services.AddHttpClient<IBasketService,BasketService>(client => client.BaseAddress = new Uri(basketBaseUrl));
builder.Services.AddHttpClient<IOrderService, OrderService>(client => client.BaseAddress = new Uri(orderBaseUrl));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
