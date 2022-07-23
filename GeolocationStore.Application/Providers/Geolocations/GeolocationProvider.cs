using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GeolocationStore.Application.Models;
using GeolocationStore.Application.Models.Requests;
using GeolocationStore.Application.Models.Responses;
using GeolocationStore.Domain.RepositoryModels;

namespace GeolocationStore.Application.Providers.Geolocations
{
    public class GeolocationProvider : IGeolocationProvider
    {
        private readonly IGeolocationDataService _geolocationDataService;
        private readonly IDbRepository<IpAddressDetails> _dbRepository;
        private readonly IMapper _mapper;

        public GeolocationProvider(IGeolocationDataService geolocationDataService,
            IDbRepository<IpAddressDetails> dbRepository, IMapper mapper)
        {
            _geolocationDataService = geolocationDataService;
            _dbRepository = dbRepository;
            _mapper = mapper;
        }

        public async Task<Result<GeolocationDataResponse>> Create(IpAddressRequest ipAddressRequest)
        {
            var ipAddressDetailsInRepo = await _dbRepository.GetBy(x => x.Ip == ipAddressRequest.IpAddress);
            if (ipAddressDetailsInRepo is not null)
                return Result<GeolocationDataResponse>.Failure("LocationDataAlreadyExists",
                    "Data with given ip already exists", 400);

            var ipAddressDetailsResponse = await _geolocationDataService.GetIpAddressGeolocation(ipAddressRequest.IpAddress);
            if (ipAddressDetailsResponse.IsFailure)
                return Result<GeolocationDataResponse>.Failure(ipAddressDetailsResponse.FailureTitle,
                    ipAddressDetailsResponse.FailureReason,
                    ipAddressDetailsResponse.HttpResponseCode);

            var ipAddressDetails = _mapper.Map<IpAddressDetails>(ipAddressDetailsResponse.Data);

            var ipAddressDetailsRepo = await _dbRepository.Insert(ipAddressDetails);

            var geolocationData = _mapper.Map<GeolocationDataResponse>(ipAddressDetailsRepo);

            return Result<GeolocationDataResponse>.Success(geolocationData);
        }

        public async Task<Result<GeolocationDataResponse>> GetByIp(string ipAddress)
        {
            var ipAddressDetailsRepo = await _dbRepository.GetBy(x => x.Ip == ipAddress);
            if (ipAddressDetailsRepo is null)
                return Result<GeolocationDataResponse>.Failure("LocationNotFound", "Data with provided ip not found",
                    400);
            var geolocationData = _mapper.Map<GeolocationDataResponse>(ipAddressDetailsRepo);
            return Result<GeolocationDataResponse>.Success(geolocationData);
        }

        public async Task<List<GeolocationDataResponse>> Get()
        {
            var ipAddressDetailsRepo = await _dbRepository.GetAllRecords();
            var geolocationData = _mapper.Map<List<GeolocationDataResponse>>(ipAddressDetailsRepo);
            return geolocationData;
        }

        public async Task Delete(string ipAddress)
        {
            var ipAddressDetailsRepo = await _dbRepository.GetBy(x => x.Ip == ipAddress);
            if (ipAddressDetailsRepo is null)
                return;
            
            await _dbRepository.Delete(ipAddressDetailsRepo);
        }
    }
}