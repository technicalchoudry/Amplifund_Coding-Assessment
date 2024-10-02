namespace VideoStreaming.Models
{
    public enum Status
    {
        Success = 200,
        Created = 201,
        Accepted = 202,
        NonAuthoritiveInformation = 203,
        NoDataAvailable = 204,
        BadRequest = 400,
        Unauthorized = 401,
        PaymentRequired = 402,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504
    }

}
