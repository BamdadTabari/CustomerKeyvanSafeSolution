using KeyvanSafe.Shared.Certain.Enums;

namespace KeyvanSafe.Shared.Infrastructure.Errors;

public class ErrorResponse
{
    public ErrorResponse(ResponseModel response)
    {
        Code = (int)response.HttpStatusCodeCode;
        Message = response.Message;
    }

    public int Code { get; }
    public string Message { get; }
}