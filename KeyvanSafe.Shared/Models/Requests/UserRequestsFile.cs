using KeyvanSafe.Shared.Certain.Enums;
using KeyvanSafe.Shared.Infrastructure.Pagination;

namespace KeyvanSafe.Shared.Models.Requests;
public record CreateUserRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Mobile { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}

public record GetUserByFilterRequest : DefaultPaginationFilter
{
    protected GetUserByFilterRequest(int page, int pageSize) : base(page, pageSize)
    {
    }
    public GetUserByFilterRequest()
    {
    }

    public string? Email { get; set; }
    public List<UserStateEnum>? States { get; set; }
}

public record UpdateUserPasswordRequest
{
    public string NewPassword { get; set; }
    public string LastPassword { get; set; }
}

public record UpdateUserRequest
{
    public string Username { get; set; }
    public string Mobile { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}

public record UpdateUserRolesRequest
{
    public IEnumerable<int> RoleIds { get; set; }
}