using System.IO;
using System.Linq;
using System.Reflection;

namespace ConfigXF
{
    internal static class EmbeddedResourceHelper
    {
        public static string Load(string resourceName, Assembly assembly)
        {
            if (TryGetResource(resourceName, out var fullResourceName, assembly))
            {
                using (var stream = assembly.GetManifestResourceStream(fullResourceName))
                using (var reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();
                    return result;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        private static bool TryGetResource(string resourceName, out string fullResourceName, Assembly assembly)
        {
            try
            {
                fullResourceName = assembly
                        .GetManifestResourceNames()
                        .First(resource => resource.EndsWith(resourceName));
                return true;
            }
            catch (System.Exception)
            {
                fullResourceName = string.Empty;
                return false;
            }
        }
    }
}
