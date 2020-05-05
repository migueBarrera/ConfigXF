using System.Threading.Tasks;

namespace SampleConfigXF.Services.Commons
{
    public interface INavigationService
    {
        Task NavigateToLogIn();

        Task NavigateTo<T>();

        Task Back();
    }
}
