using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.AspNetCore;

namespace ShopBasedOnMs.BuildingBlocks.Logging.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseCustomSerilog(this IHostBuilder hostBuilder)
    {
        return hostBuilder.UseSerilog(SerilogLogger.Configure());
    }
}