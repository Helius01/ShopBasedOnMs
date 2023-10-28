using System.Data.SqlClient;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Serilog;
using ShopBasedOnMs.BuildingBlocks.Logging.Extensions;
using ShopBasedOnMs.Services.Catalog.CatalogApi;
using ShopBasedOnMs.Services.Catalog.CatalogApi.Infrastructure;

var configuration = GetConfiguration();

try
{
    var host = CreateHostBuilder(configuration, Environment.GetCommandLineArgs());
    Log.Information("Configuring web host ({ApplicationContext})...", AppName);
    Log.Information("Applying migrations ({ApplicationContext})...", AppName);

    // Migration
    using (var scope = host.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<CatalogContextSeed>>();

        var delay = Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5);
        
        var retry = Policy.Handle<SqlException>().WaitAndRetry(delay);

        retry.Execute(() =>
        {
            new CatalogContextSeed().MigrateAndSeedAsync(context, logger).Wait();
        });
    }

    Log.Information("Starting web host ({ApplicationContext})...", Program.AppName);
    host.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

static IConfiguration GetConfiguration()
{

    var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

    return builder.Build();
}


IHost CreateHostBuilder(IConfiguration configuration, string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseWebRoot("Pics")
            .ConfigureAppConfiguration(x => x.AddConfiguration(configuration));
        })
        .UseCustomSerilog()
        .Build();

/// <summary>
/// A part of Program class that carry some properties
/// </summary>
public partial class Program
{
    /// <summary>
    /// The name of App.The value is used in logs
    /// </summary>
    private const string AppName = "Catalog.Api";
}
