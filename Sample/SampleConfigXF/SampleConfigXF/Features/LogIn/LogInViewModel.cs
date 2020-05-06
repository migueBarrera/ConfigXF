using SampleConfigXF.Framework;
using SampleConfigXF.Services.Commons;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using ConfigXF;
using SampleConfigXF.Config;

namespace SampleConfigXF.Features.LogIn
{
    public class LogInViewModel : BaseViewModel
    {
        private string testText;

        public LogInViewModel()
        {
            TestText = ConfigManager<AppConfig>.CurrentConfig?.Title;
        }

        public ICommand ClickButtonCommand => new AsyncCommand(ClickButtonCommandExecute);

        public string TestText { get => testText; set => SetAndRaisePropertyChanged(ref testText,value); }

        private async Task ClickButtonCommandExecute()
        {
            var result = await new TaskHelperFactory(DialogsService, ConnectivityService)
                            .CreateInternetAccessViewModelInstance(LoggingService)
                            .TryExecuteAsync(FakeService);

            if (result.IsSuccess)
            {
                // use result as result.Value
            }
        }

        private async Task<bool> FakeService()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));

            return true;
        }
    }
}
