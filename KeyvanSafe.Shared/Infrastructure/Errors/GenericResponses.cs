using KeyvanSafe.Shared.Certain.Enums;

namespace KeyvanSafe.Shared.Infrastructure.Errors;
public static class GenericResponses
{
    public static ResponseModel SendResponse(string message, OperationResultStatusEnum statusCode)
        => new ResponseModel(message: message, statusCode: statusCode);
}