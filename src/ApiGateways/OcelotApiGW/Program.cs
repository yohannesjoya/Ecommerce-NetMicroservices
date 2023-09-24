using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//logging configuration

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// services configuration

builder.Services.AddOcelot();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.UseOcelot().Wait();

app.Run();
