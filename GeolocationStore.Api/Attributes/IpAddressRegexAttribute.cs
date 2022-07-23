using System.ComponentModel.DataAnnotations;
using GeolocationStore.Application.Helpers;

namespace GeolocationStore.Api.Attributes
{
    public class IpAddressRegexAttribute : RegularExpressionAttribute
    {
        public IpAddressRegexAttribute() : base(IpAddressRegexHelper.IpAddressRegex)
        {
            ErrorMessage = IpAddressRegexHelper.IpAddressError;
        }
    }
}