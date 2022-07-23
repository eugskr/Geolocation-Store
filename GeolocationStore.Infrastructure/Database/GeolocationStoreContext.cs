using System.Collections.Generic;
using GeolocationStore.Domain.RepositoryModels;
using Microsoft.EntityFrameworkCore;

namespace GeolocationStore.Infrastructure.Database
{
    public class GeolocationStoreContext : DbContext
    {
        public GeolocationStoreContext(DbContextOptions<GeolocationStoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var ipAddressDetails = modelBuilder
                .Entity<IpAddressDetails>();

            ipAddressDetails
                .Property(x => x.Location)
                .HasColumnType("jsonb");

            ipAddressDetails.HasData(new List<IpAddressDetails>()
            {
                new()
                {
                    Ip = "140.82.112.4",
                    Type = "ipv4",
                    ContinentCode = "NA",
                    ContinentName = "North America",
                    CountryCode = "USA",
                    CountryName = "United States",
                    RegionCode = "CA",
                    RegionName = "California",
                    City = "San Francisco",
                    Zip = "94107",
                    Latitude = "37.76784896850586",
                    Longitude = "-122.39286041259766",
                    Location = new Location()
                    {
                        GeonameId = 5391959,
                        Capital = "Washington D.C.",
                        Languages = new List<Languages>()
                        {
                            new()
                            {
                                Code = "en",
                                Name = "English",
                                Native = "English"
                            }
                        }
                    }
                },
                new()
                {
                    Ip = "194.181.92.97",
                    Type = "ipv4",
                    ContinentCode = "EU",
                    ContinentName = "Europe",
                    CountryCode = "PL",
                    CountryName = "Poland",
                    RegionCode = "MZ",
                    RegionName = "Mazovia",
                    City = "Śródmieście",
                    Zip = "00-025",
                    Latitude = "52.2317008972168",
                    Longitude = "21.018339157104492",
                    Location = new Location()
                    {
                        GeonameId = 758470,
                        Capital = "Warsaw",
                        Languages = new List<Languages>()
                        {
                            new()
                            {
                                Code = "pl",
                                Name = "Polish",
                                Native = "Polski"
                            }
                        }
                    }
                },
                new()
                {
                    Ip = "89.184.73.7",
                    Type = "ipv4",
                    ContinentCode = "EU",
                    ContinentName = "Europe",
                    CountryCode = "UA",
                    CountryName = "Ukraine",
                    RegionCode = "30",
                    RegionName = "Kyiv City",
                    City = "Kyiv",
                    Zip = "01033",
                    Latitude = "50.4547004699707",
                    Longitude = "30.523799896240234",
                    Location = new Location()
                    {
                        GeonameId = 703448,
                        Capital = "Kyiv",
                        Languages = new List<Languages>()
                        {
                            new()
                            {
                                Code = "uk",
                                Name = "Ukrainian",
                                Native = "Українська"
                            }
                        }
                    }
                }
            });
        }

        public DbSet<IpAddressDetails> IpAddressDetails { get; set; }
    }
}