using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace ConfigXF
{
    public static class ConfigManager<T>
    {
        private static JsonSerializerSettings settings;

        public static T CurrentConfig;

        public static void Init(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(Assembly), "Assembly Cannot be null");
            }

            InitializeConfigManager(new ConfigManagerSettings(assembly));
        }

        public static void Init(ConfigManagerSettings configManagerSettings)
        {
            if (configManagerSettings?.Assembly == null)
            {
                throw new ArgumentNullException(nameof(Assembly), "Assembly Cannot be null");
            }

            InitializeConfigManager(configManagerSettings);
        }

        private static void InitializeConfigManager(ConfigManagerSettings configManagerSettings)
        {
            InitJsonSettings(configManagerSettings.Required);

            SetBuildInConfig(configManagerSettings);

            OverrideConfigIfConfigFileFound(configManagerSettings);
        }


        public static string GetCurrentConfigInJson()
        {
            return JsonConvert.SerializeObject(CurrentConfig, settings);
        }

        private static void OverrideConfigIfConfigFileFound(ConfigManagerSettings configManagerSettings)
        {
            var configJson = EmbeddedResourceHelper.Load(configManagerSettings.MasterFile, configManagerSettings.Assembly);
            if (!string.IsNullOrWhiteSpace(configJson))
            {
                SetConfig(configJson);
            }
        }

        private static void SetBuildInConfig(ConfigManagerSettings configManagerSettings)
        {
            var configFile = configManagerSettings.DebugFile;
#if !DEBUG
            configFile = configManagerSettings.ReleaseFile;
#endif
            var configJson = EmbeddedResourceHelper.Load(configFile, configManagerSettings.Assembly);
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
