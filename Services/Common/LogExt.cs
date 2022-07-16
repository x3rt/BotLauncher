using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Pastel;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Bot_Launcher.Services.Common
{
    internal static class LogExt
    {
        private static string LogPath = "Resources/Logs/log-.log";

        public static Logger CreateLogger(Config cfg)
        {
            const string template = "[{Timestamp:yyyy-MM-dd HH:mm:ss zzz}] [Launcher] [{Level:u3}] {Message:l}{NewLine}{Exception}";

            LoggerConfiguration logConfig = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .MinimumLevel.Is(cfg.Debug ? LogEventLevel.Debug : LogEventLevel.Information)
                    .WriteTo.Console(outputTemplate: template)
                ;

            if (true)
            {
                logConfig = logConfig.WriteTo.File(
                    LogPath,
                    LogEventLevel.Debug,
                    outputTemplate: template,
                    rollingInterval: RollingInterval.Month,
                    buffered: false,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: 30
                );
            }

            return logConfig.CreateLogger();
        }
    }
}