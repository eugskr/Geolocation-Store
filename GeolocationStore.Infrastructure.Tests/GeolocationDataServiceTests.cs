using System.Net.Http;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using GeolocationStore.Infrastructure.IpStack;
using Moq;
using Xunit;

namespace GeolocationStore.Infrastructure.Tests
{
    public class GeolocationDataServiceTests
    {
        [Theory]
        [GeolocationDataServiceTestsData]
        public async void GetIpAddressGeolocation_Success(
            string ipAddress,
            HttpResponseMessage httpResponseMessage,
            HttpClient client,
            [Frozen] Mock<IHttpClientFactory> httpClientFactoryMock,
            GeolocationDataService sut)
        {
            httpClientFactoryMock.Setup(x => x.CreateClient("IpStack")).Returns(client);
            var response = await sut.GetIpAddressGeolocation(ipAddress);
            
            Assert.Equal(ipAddress, response.Data.Ip);
        }
    }
    
    internal class GeolocationDataServiceTestsDataAttribute : AutoDataAttribute
    {
        public GeolocationDataServiceTestsDataAttribute()
            : base(
                () =>
                    new Fixture()
                        .Customize(new AutoMoqCustomization() { ConfigureMembers = true })
            )
        {
        }
    }
}