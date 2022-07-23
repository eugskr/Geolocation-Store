namespace GeolocationStore.Application.Models.Responses
{
    public class IpStackErrorResponse
    {
        public IpStackErrorDetails Error { get; set; }
    }

    public class IpStackErrorDetails
    {
        public int Code { get; set; }
        public string Type { get; set; }
        public string Info { get; set; }
    }
}