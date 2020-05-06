using System.Reflection;
using SampleConfigXF.Android;
using SampleConfigXF.Services;

[assembly: Xamarin.Forms.Dependency(
   typeof(AssemblyService))]
namespace SampleConfigXF.Android
{
    public class AssemblyService : IAssemblyService
    {
        public Assembly GetPlatformAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}