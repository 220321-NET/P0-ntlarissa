using DL;
using BL;
using System;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

//logger configuration
builder.Host.UseSerilog(
    (ctx, lc) => lc
    // add console as logging target
    .WriteTo.Console()
    // add a logging target for warnings and higher severity  logs
    // structured in JSON format
    .WriteTo.File(new JsonFormatter(),
                    "../Logs/important.json",
                    restrictedToMinimumLevel: LogEventLevel.Warning)
    // add a rolling file for all logs
    .WriteTo.File("../Logs/all-.logs",
                    rollingInterval: RollingInterval.Day)
);

// Add services to the container.


//Ensures that the JSON serialization uses PascalCasing in its keys
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding caching ability for endpoints
builder.Services.AddMemoryCache();

//Registering our dependencies in Services container to be dep injected
builder.Services.AddScoped<IStoreDL>(ctx => new StoreDL(builder.Configuration.GetConnectionString("StoreApp")));
builder.Services.AddScoped<IStoreBL, StoreBL>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
