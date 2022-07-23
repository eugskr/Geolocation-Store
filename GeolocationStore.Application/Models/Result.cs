using GeolocationStore.Application.Models.Responses;

namespace GeolocationStore.Application.Models
{
    public class Result<T>
    {
        public T Data { get; protected set; }
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        public FailureDetails FailureDetails { get; protected set; }
        public string FailureReason => FailureDetails?.FailureReason;
        public int? HttpResponseCode => FailureDetails?.HttpResponseCode;
        public string FailureTitle => FailureDetails?.FailureTitle;
        public static Result<T> Success(T data) => new() { Data = data, IsSuccess = true };

        public static Result<T> Failure(string failureTitle, string failureReason, int? httpResponseCode) =>
            new()
            {
                IsSuccess = false,
                FailureDetails = new()
                {
                    FailureTitle = failureTitle,
                    FailureReason = failureReason,
                    HttpResponseCode = httpResponseCode
                }
            };

        public static Result<T> Failure(IpStackErrorDetails errorDetails) =>
            new()
            {
                IsSuccess = false,
                FailureDetails = new()
                {
                    FailureTitle = errorDetails.Type,
                    FailureReason = errorDetails.Info,
                    HttpResponseCode = 400
                }
            };
    }

    public class FailureDetails
    {
        public string FailureTitle { get; set; }
        public string FailureReason { get; set; }
        public int? HttpResponseCode { get; set; }
    }
}