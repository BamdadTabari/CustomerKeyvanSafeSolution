using KeyvanSafe.Shared.Models.Dtos.Identity.Auth;
using KeyvanSafe.Shared.Models.Dtos.Identity.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KeyvanSafe.Shared.Models.Results.Identity;

public class GetProfileResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is UserDto value)
            result.Value = new
            {
                value.Id,
                value.UserName,
                Roles = value.UserRoles.Select(x => new
                {
                    Id = x.RoleId
                }),
                value.Mobile,
                value.Email,
                value.CreatedAt,
                value.UpdatedAt
            };

        await next();
    }
}

public class LoginResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is LoginResultDto value)
            result.Value = new
            {
                Username = value.UserName,
                value.FullName,
                value.AccessToken,
                value.RefreshToken,
            };

        await next();
    }
}

public class TokenResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is TokenResultDto value)
            result.Value = new
            {
                value.AccessToken
            };

        await next();
    }
}