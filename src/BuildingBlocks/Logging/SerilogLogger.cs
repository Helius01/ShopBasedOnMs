using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;

namespace ShopBasedOnMs.BuildingBlocks.Logging;

/// <summary>
/// It provides functionalities to configure serilog in applications 
/// </summary>
public static class SerilogLogger
{
    /// <summary>
    /// Configure the Serilog via specific action
    /// </summary>
    /// <returns></returns>
    public static Action<HostBuilderContext, LoggerConfiguration> Configure() =>
                    (context, logConfiguration) =>
                    {
                        var sequenceServerAddress = context.Configuration["SequenceServerAddress"];
                        var logstashServerAddress = context.Configuration["LogstashServerAddress"];

                        logConfiguration.MinimumLevel
                                        .Verbose()
                                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                                        .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                                        .Enrich.FromLogContext()
                                        .WriteTo.Console()
                                        .WriteTo.File(new RenderedCompactJsonFormatter(), "log.ndjson", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose)
                                        .WriteTo.Seq(string.IsNullOrWhiteSpace(sequenceServerAddress) ? "http://seq" : sequenceServerAddress)
                                        .WriteTo.Http(string.IsNullOrWhiteSpace(logstashServerAddress) ? "http://logstash" : logstashServerAddress, null);
                    };
}
