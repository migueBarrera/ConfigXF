using System.IO;
using System.Linq;
using System.Reflection;

namespace ConfigXF
{
    internal static class EmbeddedResourceHelper
    {
        public static string Load(string resourceName, Assembly assembly = null)
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

        private static bool TryGetResource(string resourceName, out string fullResourceName, Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetExecutingAssembly();
            }

            try
            {
                fullResourceName = assembly
                        .GetManifestResourceNames()
                        .First(resource => resource.EndsWith(resourceName));
            }
            catch (System.Exception)
            {
                fullResourceName = string.Empty;
                return false;
            }

            return true;
        }
    }
}
