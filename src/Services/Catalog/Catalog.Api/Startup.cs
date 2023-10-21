namespace ShopBasedOnMs.Services.Catalog.CatalogApi;

public class Startup
{
    public IConfiguration Configuration { get; set; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCustomMVC(Configuration);
        services.AddSwagger(Configuration);
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        var basePath = Configuration["BasePath"];

        if (basePath is null)
        {
            throw new ArgumentNullException(nameof(basePath));
        }

        loggerFactory.CreateLogger<Startup>().LogDebug("Using BasePath '{basePath}'", basePath);
        app.UsePathBase(basePath);

        app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{basePath}/swagger/v1/swagger.json", "Catalog.Api v1");
            });

        app.UseRouting();
        app.UseCors("CorsPolicy");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
        });
    }
}

public static class CustomExtensions
{
    public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
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

    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
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