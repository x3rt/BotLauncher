using Newtonsoft.Json;

namespace Bot_Launcher.Services.Common
{
    public sealed class Config
    {
        [JsonProperty("path-to-executable")] public string PathToExecutable { get; set; } = @"D:\coding\Kurva\KurvaBot\KurvaBot\bin\Debug\net6.0";
        [JsonProperty("executable")] public string Executable { get; set; } = "Bot.exe";
        [JsonProperty("exit-codes")] public ExitCodesConfig ExitCodes { get; set; } = new ExitCodesConfig();
        [JsonProperty("restart-delay")] public int RestartDelay { get; set; } = 1;
        [JsonProperty("archive-to-extract")] public string ArchiveToExtract { get; set; } = "Latest.zip";
        [JsonProperty("debug")] public bool Debug { get; set; } = false;

    }
}