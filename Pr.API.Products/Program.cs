using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pr.API.Common.Middleware;
using Pr.API.Products.Db;
using Pr.API.Products.Interfaces;
using Pr.API.Products.Providers;

var builder = WebApplication.CreateBuilder(args);



builder.Services
    .AddScoped<IProductsProvider, ProductsProvider>()
    .AddDbContext<ProductsDbContext>(options => options.UseInMemoryDatabase("Products"))
    .AddControllers();

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "API Products",
            Version = "v1",
            Description = "The documentation Web API Products"
        });
        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
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
