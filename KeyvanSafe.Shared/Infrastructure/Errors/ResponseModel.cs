using KeyvanSafe.Shared.Certain.Enums;

namespace KeyvanSafe.Shared.Infrastructure.Errors;
public struct ResponseModel
{
    public ResponseModel(OperationResultStatusEnum httpStatusCodeCode, string message)
    {
        HttpStatusCodeCode = httpStatusCodeCode;
        Message = message;
    }

    public  string Message { get; set; }
    public  OperationResultStatusEnum HttpStatusCodeCode { get; set; }
}