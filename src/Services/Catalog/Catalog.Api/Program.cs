using ShopBasedOnMs.Services.Catalog.CatalogApi;
using ShopBasedOnMs.BuildingBlocks.Logging.Extensions;

var configuration = GetConfiguration();

var host = CreateHostBuilder(configuration, Environment.GetCommandLineArgs());

host.Run();

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