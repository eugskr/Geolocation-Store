using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using GeolocationStore.Application.Automapper;
using GeolocationStore.Application.Models;
using GeolocationStore.Application.Models.Requests;
using GeolocationStore.Application.Models.Responses;
using GeolocationStore.Application.Providers.Geolocations;
using GeolocationStore.Domain.RepositoryModels;
using Moq;
using Xunit;

namespace GeolocationStore.Application.Tests
{
    public class GeolocationProviderTests
    {
        [Theory]
        [GeolocationProviderTestsData]
        public async void Create_Success(
            IpAddressRequest ipAddressRequest,
            IpAddressDetails ipAddressDetails,
            Result<IpAddressDetailsResponse> ipAddressDetailsResponse,
            [Frozen] Mock<IDbRepository<IpAddressDetails>> dbRepoMock,
            [Frozen] Mock<IGeolocationDataService> geolocationServiceMock,
            GeolocationProvider sut
        )
        {
            ipAddressDetails.Ip = ipAddressRequest.IpAddress;
            ipAddressDetailsResponse.IsSuccess = true;
            dbRepoMock.Setup(x => x.GetBy(v => v.Ip == ipAddressRequest.IpAddress))
                .ReturnsAsync((IpAddressDetails)null);
            geolocationServiceMock.Setup(x => x.GetIpAddressGeolocation(ipAddressRequest.IpAddress))
                .ReturnsAsync(ipAddressDetailsResponse);

            dbRepoMock.Setup(x => x.Insert(It.IsAny<IpAddressDetails>())).ReturnsAsync(ipAddressDetails);

            var response = await sut.Create(ipAddressRequest);

            Assert.Equal(response.Data.Ip, ipAddressRequest.IpAddress);
        }

        [Theory]
        [GeolocationProviderTestsData]
        public async void Create_LocationExist_Failure(
            IpAddressRequest ipAddressRequest,
            IpAddressDetails ipAddressDetails,
            [Frozen] Mock<IDbRepository<IpAddressDetails>> dbRepoMock,
            GeolocationProvider sut
        )
        {
            dbRepoMock.Setup(x => x.GetBy(v => v.Ip == ipAddressRequest.IpAddress))
                .ReturnsAsync(ipAddressDetails);

            var response = await sut.Create(ipAddressRequest);

            Assert.Equal("LocationDataAlreadyExists", response.FailureTitle);
            Assert.Null(response.Data);
        }

        [Theory]
        [GeolocationProviderTestsData]
        public async void Create_GeolocationService_Failure(
            IpAddressRequest ipAddressRequest,
            IpAddressDetails ipAddressDetails,
            Result<IpAddressDetailsResponse> ipAddressDetailsResponse,
            [Frozen] Mock<IDbRepository<IpAddressDetails>> dbRepoMock,
            [Frozen] Mock<IGeolocationDataService> geolocationServiceMock,
            GeolocationProvider sut
        )
        {
            ipAddressDetailsResponse.IsSuccess = false;
            dbRepoMock.Setup(x => x.GetBy(v => v.Ip == ipAddressRequest.IpAddress))
                .ReturnsAsync(ipAddressDetails);

            geolocationServiceMock.Setup(x => x.GetIpAddressGeolocation(ipAddressRequest.IpAddress))
                .ReturnsAsync(ipAddressDetailsResponse);

            var response = await sut.Create(ipAddressRequest);

            Assert.True(response.IsFailure);
            Assert.Null(response.Data);
        }

        [Theory, GeolocationProviderTestsData]
        public async void GetById_ValidIp_Success(
            [Frozen] IpAddressDetails ipAddressDetails,
            [Frozen] Mock<IDbRepository<IpAddressDetails>> dbRepoMock,
            GeolocationProvider sut
        )
        {
            string ip = ipAddressDetails.Ip;
            dbRepoMock.Setup(x => x.GetBy(v => v.Ip == ipAddressDetails.Ip)).ReturnsAsync(ipAddressDetails);

            var response = await sut.GetByIp(ipAddressDetails.Ip);

            Assert.Equal(ip, response.Data.Ip);
        }

        [Theory]
        [GeolocationProviderTestsData]
        public async void GetById_ReturnNull(
            [Frozen] Mock<IDbRepository<IpAddressDetails>> dbRepoMock,
            string ip,
            GeolocationProvider sut
        )
        {
            dbRepoMock.Setup(x => x.GetBy(v => v.Ip == ip)).ReturnsAsync((IpAddressDetails)null);

            var result = await sut.GetByIp(ip);

            Assert.Null(result.Data);
        }

        [Theory]
        [GeolocationProviderTestsData]
        public async void Get_Execute_Once(
            List<IpAddressDetails> ipAddressDetailsList,
            [Frozen] Mock<IDbRepository<IpAddressDetails>> dbRepoMock,
            GeolocationProvider sut
        )
        {
            dbRepoMock.Setup(x => x.GetAllRecords()).ReturnsAsync(ipAddressDetailsList);

            var response = await sut.Get();

            dbRepoMock.Verify(x => x.GetAllRecords(), Times.Once);
        }


        [Theory]
        [GeolocationProviderTestsData]
        public async void Delete_Execute_Once(
            string ipAddress,
            IpAddressDetails ipAddressDetails,
            [Frozen] Mock<IDbRepository<IpAddressDetails>> dbRepoMock,
            GeolocationProvider sut
        )
        {
            dbRepoMock.Setup(x => x.GetBy(v => v.Ip == ipAddress)).ReturnsAsync(ipAddressDetails);
            dbRepoMock.Setup(x => x.Delete(ipAddressDetails));

            await sut.Delete(ipAddress);

            dbRepoMock.Verify(x => x.Delete(ipAddressDetails), Times.Once);
        }
    }

    internal class GeolocationProviderTestsDataAttribute : AutoDataAttribute
    {
        public GeolocationProviderTestsDataAttribute()
            : base(
                () =>
                    new Fixture()
                        .Customize(new AutoMoqCustomization() { ConfigureMembers = true })
                        .Customize(new GeolocationProviderTestsCustomization())
            )
        {
        }
    }

    internal class GeolocationProviderTestsCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(() =>
                new MapperConfiguration(cfg => { cfg.AddProfile(new IpAddressDetailsAutomapper()); }).CreateMapper());
        }
    }
}