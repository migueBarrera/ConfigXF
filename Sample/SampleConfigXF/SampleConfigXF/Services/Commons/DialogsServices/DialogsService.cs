using System.Threading.Tasks;
using SampleConfigXF.Resources.Localization;
using Xamarin.Forms;

namespace SampleConfigXF.Services.Commons
{
    public class DialogsService : IDialogsService
    {
        public Task ShowAlertAsync(string title, string body)
        {
            return Application.Current.MainPage.DisplayAlert(
                title,
                body,
                Strings.Alert_OK);
        }

        public Task<bool> ShowAlertAsync(string title, string body, string yesText, string noText)
        {
            return Application.Current.MainPage.DisplayAlert(
                                                             title,
                                                             body,
                                                             yesText,
                                                             noText);
        }
    }
}