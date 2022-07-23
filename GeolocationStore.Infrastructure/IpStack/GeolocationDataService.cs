using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GeolocationStore.Application;
using GeolocationStore.Application.Models;
using GeolocationStore.Application.Models.Responses;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace GeolocationStore.Infrastructure.IpStack
{
    public class GeolocationDataService : IGeolocationDataService
    {
        public static readonly string HttpClientName = "IpStack";
        private readonly IHttpClientFactory _clientFactory;
        private readonly IpStackKeyOptions _options;

        public GeolocationDataService(IHttpClientFactory clientFactory, IOptions<IpStackKeyOptions> options)
        {
            _clientFactory = clientFactory;
            _options = options.Value;
        }

        public async Task<Result<IpAddressDetailsResponse>> GetIpAddressGeolocation(string ipAddress)
        {
            var client = _clientFactory.CreateClient(HttpClientName);

            Dictionary<string, string> queryParameters = new() { ["access_key"] = _options.ApiKey };
            string requestUri = QueryHelpers.AddQueryString(ipAddress, queryParameters);

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var httpResponse = await client.SendAsync(httpRequestMessage);
            
            var jo = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());

            var errorResponse = jo.ToObject<IpStackErrorResponse>();
            if (errorResponse.Error is not null)
                return Result<IpAddressDetailsResponse>.Failure(errorResponse.Error);

            var ipAddressDetails = jo.ToObject<IpAddressDetailsResponse>();
            return Result<IpAddressDetailsResponse>.Success(ipAddressDetails);
        }
    }

    public class IpStackKeyOptions
    {
        public string ApiKey { get; set; }
    }
}