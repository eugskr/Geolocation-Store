namespace GeolocationStore.Application.Helpers
{
    public static class IpAddressRegexHelper
    {
        public const string IpAddressRegex = @"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)(\.(?!$)|$)){4}$";
        public const string IpAddressError = "Please, provide valid ipv4 address";
    }
}