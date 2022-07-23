using System.Collections.Generic;
using System.Threading.Tasks;
using GeolocationStore.Api.Attributes;
using GeolocationStore.Application.Models.Requests;
using GeolocationStore.Application.Models.Responses;
using GeolocationStore.Application.Providers.Geolocations;
using Microsoft.AspNetCore.Mvc;

namespace GeolocationStore.Api.Controllers
{
    [ApiController]
    [Route("api/geolocations")]
    public class GeolocationsController : ControllerBase
    {
        private readonly IGeolocationProvider _geolocationProvider;

        public GeolocationsController(IGeolocationProvider geolocationProvider)
        {
            _geolocationProvider = geolocationProvider;
        }

        /// <summary>
        /// Retrieve all geolocation data
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<GeolocationDataResponse>), 200)]
        public async Task<IActionResult> Get()
        {
            var response = await _geolocationProvider.Get();
            return Ok(response);
        }

        /// <summary>
        /// Create geolocation data
        /// </summary>
        /// <param name="ipAddressRequest">IPV4</param>
        /// <response code="400">Bad request, possible failure reasons:
        /// [LocationDataAlreadyExists]
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(GeolocationDataResponse), 201)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public async Task<IActionResult> Create([FromBody] IpAddressRequest ipAddressRequest)
        {
            var response = await _geolocationProvider.Create(ipAddressRequest);
            if (response.IsSuccess)
                return Created(Request?.Path.Value, response.Data);

            return Problem(
                detail: response.FailureDetails.FailureReason,
                statusCode: response.FailureDetails.HttpResponseCode,
                title: response.FailureDetails.FailureTitle);
        }

        /// <summary>
        /// Get geolocation data by ip
        /// </summary>
        /// <param name="ipAddress">IPV4</param>
        /// <response code="404">Not found, possible failure reasons:
        /// [LocationNotFound]
        /// </response>
        [HttpGet("{ipAddress}")]
        [ProducesResponseType(typeof(GeolocationDataResponse), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> GetByIp([IpAddressRegex] string ipAddress)
        {
            var response = await _geolocationProvider.GetByIp(ipAddress);
            if (response.IsSuccess)
                return Ok(response.Data);

            return Problem(
                detail: response.FailureDetails.FailureReason,
                statusCode: response.FailureDetails.HttpResponseCode,
                title: response.FailureDetails.FailureTitle);
        }

        /// <summary>
        /// Delete geolocation data
        /// </summary>
        /// <param name="ipAddress">IPV4</param>
        [HttpDelete("{ipAddress}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([IpAddressRegex] string ipAddress)
        {
            await _geolocationProvider.Delete(ipAddress);
            return Ok();
        }
    }
}