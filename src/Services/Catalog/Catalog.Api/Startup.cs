using Microsoft.EntityFrameworkCore;
using ShopBasedOnMs.Services.Catalog.CatalogApi.Extensions;

namespace ShopBasedOnMs.Services.Catalog.CatalogApi;

/// <summary>
/// Startup
/// </summary>
public class Startup
{
    /// <summary>
    /// Configuration
    /// </summary>
    /// <value></value>
    public IConfiguration Configuration { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }


    /// <summary>
    /// Configure and Register services (DI)
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCustomMvc();
        services.AddSwagger();
        services.AddCustomDbContext(new(Configuration.GetConnectionString("SqlServer")!));
    }

    /// <summary>
    /// Configuring middlewares and etc.
    /// </summary>
    /// <param name="app"></param>
    public void Configure(IApplicationBuilder app)
    {
        var basePath = Configuration["BASE_PATH"];

        if (basePath is null)
        {
            throw new ArgumentNullException("The argument can't be null at this point", nameof(basePath));
        }

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