using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ShopBasedOnMs.BuildingBlocks.TypeProvider;
using ShopBasedOnMs.Services.Catalog.CatalogApi.Infrastructure;

namespace ShopBasedOnMs.Services.Catalog.CatalogApi.Extensions;

/// <summary>
/// Provides functionalities on IServiceCollection as extension methods
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register db context via given connection string 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, ConnectionString connectionString)
    {
        return services.AddEntityFrameworkSqlServer()
            .AddDbContext<CatalogContext>(options =>
            {
                options.UseSqlServer(connectionString,
                                    sqlServerOptionsAction: sqlOptions =>
                                    {
                                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                    });
            });

    }

    /// <summary>
    /// Register MVC with customized settings
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomMvc(this IServiceCollection services)
    {
        services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder => builder
                                                    .SetIsOriginAllowed((host) => true)
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader()
                                                    .AllowCredentials());
        });

        return services;
    }

    /// <summary>
    /// Register Swagger with customized settings
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "ShopBasedOnMs - Catalog HTTP API",
                Version = "v1",
                Description = "The Catalog microservice HTTP-API"
            });
        });


        return services;
    }
}