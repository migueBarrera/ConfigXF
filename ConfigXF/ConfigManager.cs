using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace ConfigXF
{
    public static class ConfigManager<T>
    {
        private const string ConfigFilename = "Config.json";

        private const string DebugConfig = "Config_Debug.json";
        private const string ReleaseConfig = "Config_Release.json";

        private static JsonSerializerSettings settings;

        public static T CurrentConfig;

        public static void Init(Assembly assembly, Required required = Required.Default)
        {
            InitJsonSettings(required);

            SetBuildInConfig(assembly);

            OverrideConfigIfConfigFileFound(assembly);
        }

        public static string GetCurrentConfigInJson()
        {
            return JsonConvert.SerializeObject(CurrentConfig, settings);
        }

        private static void OverrideConfigIfConfigFileFound(Assembly assembly)
        {
            var configJson = EmbeddedResourceHelper.Load(ConfigFilename, assembly);
            if (!string.IsNullOrWhiteSpace(configJson))
            {
                SetConfig(configJson);
            }
        }

        private static void SetBuildInConfig(Assembly assembly)
        {
            var configFile = DebugConfig;
#if !DEBUG
            configFile = ReleaseConfig;
#endif
            var configJson = EmbeddedResourceHelper.Load(configFile, assembly);
            if (!string.IsNullOrWhiteSpace(configJson))
            {
                SetConfig(configJson);
            }
        }

        private static void SetConfig(string configJson)
        {
            CurrentConfig = DeserializeConfig(configJson);
        }

        private static T DeserializeConfig(string configJson)
        {
            return JsonConvert.DeserializeObject<T>(configJson, settings);
        }

        private static void InitJsonSettings(Required required)
        {
            settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error,
                ContractResolver = new EnsureAllExistsContractResolver(required),
                Formatting = Formatting.Indented,
            };
        }

        private class EnsureAllExistsContractResolver : DefaultContractResolver
        {
            private Required required;

            public EnsureAllExistsContractResolver(Required required)
            {
                this.required = required;
            }

            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                JsonProperty property = base.CreateProperty(member, memberSerialization);

                property.Required = this.required;

                return property;
            }
        }
    }
}
