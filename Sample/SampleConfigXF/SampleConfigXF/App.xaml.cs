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

            Locator.Init();
            Locator.Resolve<INavigationService>().NavigateToLogIn();

            ConfigManager<AppConfig>.Init(Assembly.GetExecutingAssembly());

            var x = ConfigManager<AppConfig>.CurrentConfig.Title;
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
