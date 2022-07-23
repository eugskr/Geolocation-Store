using System;
using GeolocationStore.Application;
using GeolocationStore.Application.Providers.Geolocations;
using GeolocationStore.Domain.RepositoryModels;
using GeolocationStore.Infrastructure.Database;
using GeolocationStore.Infrastructure.IpStack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeolocationStore.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddScoped<IGeolocationDataService, GeolocationDataService>()
                .AddScoped<IDbRepository<IpAddressDetails>, DbRepository<IpAddressDetails>>();

            services.AddDbContext<GeolocationStoreContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("GeolocationStoreDbConnection"));
            });

            services.AddHttpClient(GeolocationDataService.HttpClientName,
                c => { c.BaseAddress = new Uri(configuration.GetValue<string>("IpStackUrl")); });

            return services;
        }
    }
}