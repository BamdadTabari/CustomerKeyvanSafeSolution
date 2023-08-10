using KeyvanSafe.Shared.Infrastructure.Pagination;
using KeyvanSafe.Shared.Models.Dtos.Identity.RoleDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KeyvanSafe.Shared.Models.Results.Identity;

public class CreateRoleResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is RoleDto value)
            result.Value = new
            {
                value.Id,
                value.Title,
            };

        await next();
    }
}

public class DeleteRoleResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is RoleDto value)
            result.Value = new
            {
                value.Id
            };

        await next();
    }
}


public class GetRoleByIdResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is RoleDto value)
            result.Value = new
            {
                value.Id,
                value.Title,
                value.CreatedAt,
                value.UpdatedAt,
                Permissions = value.RolePermission.Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Name,
                    x.Value
                })
            };

        await next();
    }
}

public class GetRolesByFilterResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is PaginatedList<RoleDto> value)
            result.Value = new
            {
                value.Page,
                value.PageSize,
                value.TotalCount,
                Data = value.Data.Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.CreatedAt,
                    x.UpdatedAt
                })
            };

        await next();
    }
}

public class UpdateRoleResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is RoleDto value)
            result.Value = new
            {
                value.Id,
                value.UpdatedAt
            };

        await next();
    }
}
