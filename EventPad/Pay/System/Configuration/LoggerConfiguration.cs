namespace EventPad.Pay.Configuration;

using Serilog;
using Serilog.Events;
using Settings;

/// <summary>
/// EventPad.Logger Configuration
/// </summary>
public static class LoggerConfiguration
{
    /// <summary>
    /// Add EventPad.Logger
    /// </summary>
    public static void AddAppLogger(this WebApplicationBuilder builder, MainSettings mainSettings,
        LogSettings logSettings)
    {
        var LoggerConfiguration = new Serilog.LoggerConfiguration();

        // Base configuration
        LoggerConfiguration
            .Enrich.WithCorrelationIdHeader()
            .Enrich.FromLogContext();

        // Log level
        if (!Enum.TryParse(logSettings.Level, out LogLevel level)) level = LogLevel.Information;

        ;

        var serilogLevel = level switch
        {
            LogLevel.Verbose => LogEventLevel.Verbose,
            LogLevel.Debug => LogEventLevel.Debug,
            LogLevel.Information => LogEventLevel.Information,
            LogLevel.Warning => LogEventLevel.Warning,
            LogLevel.Error => LogEventLevel.Error,
            LogLevel.Fatal => LogEventLevel.Fatal,
            _ => LogEventLevel.Information
        };

        LoggerConfiguration
            .MinimumLevel.Is(serilogLevel)
            .MinimumLevel.Override("Microsoft", serilogLevel)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", serilogLevel)
            .MinimumLevel.Override("System", serilogLevel)
            ;

        // Writers
        var logItemTemplate =
            "[{Timestamp:HH:mm:ss:fff} {Level:u3} ({CorrelationId})] {Message:lj}{NewLine}{Exception}";

        // Writing to Console configuration 
        if (logSettings.WriteToConsole)
            LoggerConfiguration.WriteTo.Console(
                serilogLevel,
                logItemTemplate
            );

        // Writing to File configuration 
        if (logSettings.WriteToFile)
        {
            if (!Enum.TryParse(logSettings.FileRollingInterval, out LogRollingInterval interval))
                interval = LogRollingInterval.Day;

            ;

            var serilogInterval = interval switch
            {
                LogRollingInterval.Infinite => RollingInterval.Infinite,
                LogRollingInterval.Year => RollingInterval.Year,
                LogRollingInterval.Month => RollingInterval.Month,
                LogRollingInterval.Day => RollingInterval.Day,
                LogRollingInterval.Hour => RollingInterval.Hour,
                LogRollingInterval.Minute => RollingInterval.Minute,
                _ => RollingInterval.Day
            };


            if (!int.TryParse(logSettings.FileRollingSize, out var size)) size = 5242880;

            ;

            var fileName =
                $"_.log";

            LoggerConfiguration.WriteTo.File($"logs/{fileName}",
                serilogLevel,
                logItemTemplate,
                rollingInterval: serilogInterval,
                rollOnFileSizeLimit: true,
                fileSizeLimitBytes: size
            );
        }

        // Make EventPad.Logger
        var Logger = LoggerConfiguration.CreateLogger();


        // Apply EventPad.Logger to application
        builder.Host.UseSerilog(Logger, true);
    }
}
