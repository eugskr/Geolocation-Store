using System.Collections.Generic;
using Newtonsoft.Json;

namespace GeolocationStore.Application.Models.Responses
{
    public class IpAddressDetailsResponse
    {
        [JsonProperty("ip")] public string Ip { get; set; }
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("continent_code")] public string ContinentCode { get; set; }
        [JsonProperty("continent_name")] public string ContinentName { get; set; }
        [JsonProperty("country_code")] public string CountryCode { get; set; }
        [JsonProperty("country_name")] public string CountryName { get; set; }
        [JsonProperty("region_code")] public string RegionCode { get; set; }
        [JsonProperty("region_name")] public string RegionName { get; set; }
        [JsonProperty("city")] public string City { get; set; }
        [JsonProperty("zip")] public string Zip { get; set; }
        [JsonProperty("latitude")] public decimal Latitude { get; set; }
        [JsonProperty("longitude")] public decimal Longitude { get; set; }
        [JsonProperty("location")] public LocationResponse Location { get; set; }
        [JsonProperty("country_flag")] public string CountryFlag { get; set; }
        [JsonProperty("country_flag_emoji")] public string CountryFlagEmoji { get; set; }
        [JsonProperty("country_flag_emoji_unicode")] public string CountryFlagEmojiUnicode { get; set; }
        [JsonProperty("calling_code")] public string CallingCode { get; set; }
        [JsonProperty("is_eu")] public bool IsEu { get; set; }
    }

    public class LocationResponse
    {
        [JsonProperty("geoname_id")] public long GeonameId { get; set; }
        [JsonProperty("capital")] public string Capital { get; set; }
        [JsonProperty("languages")] public List<LanguagesResponse> Languages { get; set; }
    }

    public class LanguagesResponse
    {
        [JsonProperty("code")] public string Code { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("native")] public string Native { get; set; }
    }
}