using ConfigXF;
using System.Reflection;
using SampleConfigXF.Config;
using SampleConfigXF.Framework;
using SampleConfigXF.Services.Commons;
using Xamarin.Forms;

namespace SampleConfigXF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ////You can use general config
            ConfigManager<AppConfig>.Init(Assembly.GetExecutingAssembly());

            ////OR custom config

            ////ConfigManager<AppConfig>.Init(
            //    new ConfigManagerSettings()
            //    {
            //        Assembly = Assembly.GetExecutingAssembly(),
            //        Required = Newtonsoft.Json.Required.Always,
            //        DebugFile = "Config_Debug.json",
            //        ReleaseFile = "MyReleaseFile.json",
            //        MasterFile = "MyMasterFile.json",
            //    });

            Locator.Init();
            Locator.Resolve<INavigationService>().NavigateToLogIn();
        }

        protected override void OnSleep()
        {
            NavigationService.AppSleep();
        }

        protected override void OnResume()
        {
            NavigationService.AppResume();
        }
    }
}
