using System.Threading.Tasks;
using GeolocationStore.Application.Models;
using GeolocationStore.Application.Models.Responses;

namespace GeolocationStore.Application
{
    public interface IGeolocationDataService
    {
        Task<Result<IpAddressDetailsResponse>> GetIpAddressGeolocation(string ipAddress);
    }
}