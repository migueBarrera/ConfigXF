using SampleConfigXF.Framework;
using SampleConfigXF.Services.Commons;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;

namespace SampleConfigXF.Features.LogIn
{
    public class LogInViewModel : BaseViewModel
    {
        public LogInViewModel(IConfiguration configuration)
        {

        }

        public ICommand ClickButtonCommand => new AsyncCommand(ClickButtonCommandExecute);

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
