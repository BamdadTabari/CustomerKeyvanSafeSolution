using KeyvanSafe.Shared.Certain.Enums;

namespace KeyvanSafe.Shared.Infrastructure.Errors;
public struct ResponseModel
{
    public ResponseModel(OperationResultStatusEnum statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public  string Message { get; set; }
    public  OperationResultStatusEnum StatusCode { get; set; }
}