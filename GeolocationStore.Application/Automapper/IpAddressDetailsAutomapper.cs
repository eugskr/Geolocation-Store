using AutoMapper;
using GeolocationStore.Application.Models.Responses;
using GeolocationStore.Domain.RepositoryModels;

namespace GeolocationStore.Application.Automapper
{
    public class IpAddressDetailsAutomapper : Profile
    {
        public IpAddressDetailsAutomapper()
        {
            CreateMap<IpAddressDetailsResponse, IpAddressDetails>();
            CreateMap<LocationResponse, Location>();
            CreateMap<LanguagesResponse, Languages>();
            CreateMap<IpAddressDetails, GeolocationDataResponse>();
        }
    }
}