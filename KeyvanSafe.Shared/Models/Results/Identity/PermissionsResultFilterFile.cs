using KeyvanSafe.Shared.Infrastructure.Pagination;
using KeyvanSafe.Shared.Models.Dtos.Identity.PermissionDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KeyvanSafe.Shared.Models.Results.Identity;

public class GetPermissionsResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is PaginatedList<PermissionDto> value)
            result.Value = new
            {
                value.Page,
                value.PageSize,
                value.TotalCount,
                Data = value.Data.Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Name,
                    x.Value,
                    x.CreatedAt,
                    x.UpdatedAt
                })
            };

        await next();
    }
}