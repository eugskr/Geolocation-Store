using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeolocationStore.Domain.RepositoryModels
{
    public class IpAddressDetails: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Ip { get; set; }
        public string Type { get; set; }
        public string ContinentCode { get; set; }
        public string ContinentName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Location Location { get; set; }
    }

    public class Location
    {
        public long GeonameId { get; set; }
        public string Capital { get; set; }
        public List<Languages> Languages { get; set; }
    }

    public class Languages
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Native { get; set; }
    }
}