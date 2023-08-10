using KeyvanSafe.Shared.Certain.Enums;

namespace KeyvanSafe.Shared.Infrastructure.Operations;


public record OperationResult
{
    public readonly OperationResultStatusEnum Status;
    public readonly bool IsPersistAble;
    public readonly object Value;
    public readonly Dictionary<string, string> OperationValues;

    public OperationResult(OperationResultStatusEnum status, bool isPersistAble = false, object value = null,
        Dictionary<string, string> operationValues = null)
    {
        Status = status;
        Value = value;
        IsPersistAble = isPersistAble;
        OperationValues = operationValues;
    }

    public OperationResult(OperationResult operation, bool succeeded)
    {
        Status = succeeded ? OperationResultStatusEnum.Ok : OperationResultStatusEnum.UnProcessable;
        IsPersistAble = operation.IsPersistAble;
        Value = operation.Value;
        OperationValues = operation.OperationValues;
    }

    public bool Succeeded => IsSucceeded(Status);

    private bool IsSucceeded(OperationResultStatusEnum status) => status switch
    {
        _ when
            status == OperationResultStatusEnum.Ok => true,
        _ when
            status == OperationResultStatusEnum.Invalidated ||
            status == OperationResultStatusEnum.NotFound ||
            status == OperationResultStatusEnum.Unauthorized ||
            status == OperationResultStatusEnum.UnProcessable => false,
        _ => false
    };
}