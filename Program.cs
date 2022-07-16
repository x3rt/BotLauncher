﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Bot_Launcher.Services;
using Bot_Launcher.Services.Common;
using Ionic.Zip;
using Serilog;

namespace Bot_Launcher
{
    class Program
    {
        public static ConfigService cfg = null;
        static async Task Main(string[] args)
        {
            try
            {
                cfg = await LoadBotConfigAsync();
                Log.Logger = LogExt.CreateLogger(cfg.CurrentConfiguration);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Launcher] Error loading config: " + ex.Message);
                Environment.Exit(99);
            }

            Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = Path.Combine(cfg.CurrentConfiguration.PathToExecutable, cfg.CurrentConfiguration.Executable);
            process.StartInfo.WorkingDirectory = cfg.CurrentConfiguration.PathToExecutable;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                for (;;)
                {
                    int code = StartProcess(process);
                    Log.Information("Process exited with code: {Code} ", code);
                    if (code == cfg.CurrentConfiguration.ExitCodes.Shutdown)
                        break;
                    if (code == cfg.CurrentConfiguration.ExitCodes.UnzipAndRestart)
                    {
                        Log.Information("Unzipping and restarting...");
                        //unzip folder
                        using ZipFile zip = new ZipFile(Path.Combine(cfg.CurrentConfiguration.PathToExecutable, cfg.CurrentConfiguration.ArchiveToExtract));
                        zip.ExtractAll(cfg.CurrentConfiguration.PathToExecutable, ExtractExistingFileAction.OverwriteSilently);

                        //restart
                        continue;
                    }
                    if (code == cfg.CurrentConfiguration.ExitCodes.Restart ||
                        cfg.CurrentConfiguration.ExitCodes.RestartUnknown)
                        continue;

                    Log.Error("Unknown exit code, exiting");
                    break;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error: {Error}", ex.Message);
            }
        }

        private static int StartProcess(Process process)
        {
            process.Start();
            Log.Information("Process started...");
            process.WaitForExit();
            return process.ExitCode;
        }


        private static async Task<ConfigService> LoadBotConfigAsync()
        {
            Log.Information("Loading configuration... ");

            var cfg = new ConfigService();
            await cfg.LoadConfigAsync();

            Console.Write("\r");
            return cfg;
        }
    }
}