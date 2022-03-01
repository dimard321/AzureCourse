using System.Configuration;
using System.Reflection;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Pr.API.Common.Middleware;
using Pr.API.Search.Interfaces;
using Pr.API.Search.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddScoped<ISearchService, SearchService>()
    .AddScoped<IProductsService, ProductsService>()
    .AddHttpClient("ProductsService", config =>
    {
        config.BaseAddress = new Uri(configuration["Services:Products"]);
    });
builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "API Search",
            Version = "v1",
            Description = "The documentation Web API Search"
        });
    })
    .AddSwaggerGenNewtonsoftSupport();

var mappingConfig = new MapperConfiguration(cfg =>
{
    cfg.AddMaps("Pr.API.Common");

});
mappingConfig.AssertConfigurationIsValid();
var autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);

var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Proj API");
    c.EnableTryItOutByDefault();
});

app.Run();

