using Newtonsoft.Json;
namespace Bot_Launcher.Services.Common
{
    public sealed class ExitCodesConfig
    {
        [JsonProperty("restart")] public int Restart { get; set; } = 100;
        [JsonProperty("shutdown")] public int Shutdown { get; set; } = 99;
        [JsonProperty("unzip-and-restart")] public int UnzipAndRestart { get; set; } = 101;
        [JsonProperty("restart-unknown")] public bool RestartUnknown { get; set; } = true;
    }
}