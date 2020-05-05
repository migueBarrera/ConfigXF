using Xamarin.Essentials;

namespace SampleConfigXF.Services.Commons
{
    public class ConnectivityService : IConnectivityService
    {
        public bool IsThereInternet => Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
