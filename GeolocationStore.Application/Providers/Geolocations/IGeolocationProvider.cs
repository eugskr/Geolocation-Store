using System.Collections.Generic;
using System.Threading.Tasks;
using GeolocationStore.Application.Models;
using GeolocationStore.Application.Models.Requests;
using GeolocationStore.Application.Models.Responses;

namespace GeolocationStore.Application.Providers.Geolocations
{
    public interface IGeolocationProvider
    {
        Task<Result<GeolocationDataResponse>> Create(IpAddressRequest ipAddressRequest);
        Task<Result<GeolocationDataResponse>> GetByIp(string ipAddress);
        Task<List<GeolocationDataResponse>> Get();
        Task Delete(string ipAddress);
    }
}