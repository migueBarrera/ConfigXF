using Newtonsoft.Json;
using System.Reflection;

namespace ConfigXF
{
    public class ConfigManagerSettings
    {
        public Assembly Assembly { get; set; }

        public Required Required { get; set; } = Required.Default;

        public string DebugFile { get; set; } = "Config_Debug.json";

        public string ReleaseFile { get; set; } = "Config_Release.json";

        public string MasterFile { get; set; } = "Config.json";

        public ConfigManagerSettings(Assembly assembly)
        {
            this.Assembly = assembly;
        }

        public ConfigManagerSettings()
        {
        }
    }
}