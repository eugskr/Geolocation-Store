using FluentValidation;
using GeolocationStore.Application.Helpers;

namespace GeolocationStore.Application.Models.Requests
{
    public class IpAddressRequest
    {
        public string IpAddress { get; set; }
    }

    public class IpAddressValidator : AbstractValidator<IpAddressRequest>
    {
        public IpAddressValidator()
        {
            RuleFor(x=>x.IpAddress)
                .NotEmpty().WithMessage("Please specify ipV4 address")
                .Matches(IpAddressRegexHelper.IpAddressRegex).WithMessage(IpAddressRegexHelper.IpAddressError);
        }
    }
}