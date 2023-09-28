using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

string environment = builder.Environment.EnvironmentName;

builder.Configuration.AddJsonFile($"ocelot.{environment}.json", true, true);

//logging configuration

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();


builder.Services.AddOcelot().AddCacheManager(x => { x.WithDictionaryHandle(); });

var app = builder.Build();



app.MapGet("/", () => "Hello World!");

app.UseOcelot().Wait();

app.Run();
