using KeyvanSafe.Shared.Infrastructure.Pagination;
using KeyvanSafe.Shared.Models.Dtos.Identity.ClaimDtos;
using KeyvanSafe.Shared.Models.Dtos.Identity.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KeyvanSafe.Shared.Models.Results.Identity;

public class CreateUserPermissionResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is ClaimDto value)
            result.Value = new
            {
                value.Id,
                value.Value
            };

        await next();
    }
}

public class CreateUserResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is UserDto value)
            result.Value = new
            {
                value.Id,
                value.Username
            };

        await next();
    }
}

public class DeleteUserPermissionResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is ClaimDto value)
            result.Value = new
            {
                value.Id,
                value.Value
            };

        await next();
    }
}


public class GetUserByFilterResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is PaginatedList<UserDto> value)
            result.Value = new
            {
                value.Page,
                value.PageSize,
                value.TotalCount,
                Data = value.Data.Select(x => new
                {
                    x.Id,
                    x.Username,
                    //x.IsEmailConfirmed,
                    x.IsMobileConfirmed,
                    x.IsLockedOut,
                    x.Mobile,


                    RoleTitles = x.UserRoles.Select(x => x.Role.Title),

                    x.Email,
                    State = nameof(x.State),
                    x.CreatedAt,
                    x.UpdatedAt
                })
            };

        await next();
    }
}


public class GetUserByIdResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is UserDto value)
            result.Value = new
            {
                value.Id,
                value.Username,
                //value.IsEmailConfirmed,
                value.IsMobileConfirmed,
                value.IsLockedOut,
                value.Mobile,
                RoleTitles = value.UserRoles.Select(x => x.Role.Title),
                value.Email,
                State = nameof(value.State),
                value.CreatedAt,
                value.UpdatedAt
            };

        await next();
    }
}


public class UpdateUserResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is UserDto value)
            result.Value = new
            {
                value.Id,
                value.Username,
                value.UpdatedAt
            };

        await next();
    }
}


public class UpdateUserRolesResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is UserRoleDto value)
            result.Value = new
            {
                value.RoleId,
                value.UserId
            };

        await next();
    }
}