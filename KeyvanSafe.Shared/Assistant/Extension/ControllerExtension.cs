using KeyvanSafe.Shared.Certain.Enums;
using KeyvanSafe.Shared.Infrastructure.Errors;
using KeyvanSafe.Shared.Infrastructure.Operations;
using Microsoft.AspNetCore.Mvc;

namespace KeyvanSafe.Shared.Assistant.Extension;

public static class ControllerExtension
{
    public static IActionResult ReturnResponse(this ControllerBase controller, OperationResult operation)
    {
        object response = operation.Value;
        if (response is ResponseModel errorModel)
            response = new ErrorResponse(errorModel);

        return operation.Status switch
        {
            OperationResultStatusEnum.Ok => controller.Ok(response),
            OperationResultStatusEnum.Invalidated => controller.BadRequest(response),
            OperationResultStatusEnum.Unauthorized => controller.UnprocessableEntity(response),
            OperationResultStatusEnum.UnProcessable => controller.UnprocessableEntity(response),
            _ => controller.UnprocessableEntity(response)
        };
    }
}