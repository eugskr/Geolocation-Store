using System;
using FluentValidation.AspNetCore;
using GeolocationStore.Application.Providers.Geolocations;
using Microsoft.Extensions.DependencyInjection;

namespace GeolocationStore.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddScoped<IGeolocationProvider, GeolocationProvider>();

            services
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            return services;
        }
    }
}