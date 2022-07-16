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
            string template =
                "[{Timestamp:yyyy-MM-dd HH:mm:ss zzz}] [Launcher] [{Level:u3}] {Message:l}{NewLine}{Exception}";

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


        //     public static void Debug(string message, bool toConsole = true, bool toFile = true)
        //     {
        //         WriteLog(message, LogLevel.Debug, toFile, toConsole);
        //     }
        //
        //     public static void Info(string message, bool toConsole = true, bool toFile = true)
        //     {
        //         WriteLog(message, LogLevel.Info, toFile, toConsole);
        //     }
        //
        //     public static void Warn(string message, bool toConsole = true, bool toFile = true)
        //     {
        //         WriteLog(message, LogLevel.Warn, toFile, toConsole);
        //     }
        //
        //     public static void Error(string message, bool toConsole = true, bool toFile = true)
        //     {
        //         WriteLog(message, LogLevel.Error, toFile, toConsole);
        //     }
        //
        //
        //     private static void WriteLog(string message, LogLevel logLevel, bool toFile = true, bool toConsole = true)
        //     {
        //         Console.WriteLine("Writing log to file" + message);
        //         Console.WriteLine(!System.IO.File.Exists(getFullPath()) ? "File does not exist" : "File exists");
        //         string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //         string logLevelString = logLevel.ToString();
        //         logLevelString = logLevel switch
        //         {
        //             LogLevel.Warn => logLevelString.Pastel("#ffcc00"),
        //             LogLevel.Debug => logLevelString.Pastel("#00ccff"),
        //             LogLevel.Error => logLevelString.Pastel("#ff0000"),
        //             LogLevel.Info => logLevelString.Pastel("#00ff00"),
        //             _ => logLevelString
        //         };
        //         string logMessage = $"[{time}] [{logLevelString}] [Launcher] {message}";
        //
        //         if (toFile)
        //         {
        //             if (!System.IO.File.Exists(getFullPath()))
        //             {
        //                 Console.WriteLine("Creating Log file");
        //                 CreateLogFile();
        //             }
        //
        //             File.AppendAllText(getFullPath(), logMessage + Environment.NewLine);
        //         }
        //
        //         if (toConsole)
        //         {
        //             Console.WriteLine(logMessage);
        //         }
        //     }
        //
        //
        //     private static void CreateLogFile()
        //     {
        //         if (!Directory.Exists(LogPath))
        //         {
        //             Console.WriteLine("Creating Log directory");
        //             Directory.CreateDirectory(LogPath);
        //         }
        //         Console.WriteLine("Creating Log file");
        //
        //         System.IO.File.Create(getFullPath());
        //         Console.WriteLine("Created Log file");
        //         WriteLog("Beginning of log" + Environment.NewLine, LogLevel.Info, toConsole: false);
        //     }
        //
        //     private static string FileName()
        //     {
        //         return DateTime.Now.ToString("yyyy-MM-") + "logs.log";
        //     }
        //
        //     private static string getFullPath()
        //     {
        //         return LogPath + FileName();
        //     }
        // }
    }
}