using KeyvanSafe.Shared.Infrastructure.Pagination;

namespace KeyvanSafe.Shared.Models.Requests;
public record CreateRoleRequest
{
    public string Title { get; set; }
    public IList<int> PermissionIds { get; set; }
}

public record GetRolesByFilterRequest : DefaultPaginationFilter
{
    protected GetRolesByFilterRequest(int page, int pageSize) : base(page, pageSize)
    {
    }
    public GetRolesByFilterRequest()
    {
    }
    public List<int>? PermissionIds { get; set; }
}

public record UpdateRoleRequest
{
    public string Title { get; set; }
    public IList<int> PermissionIds { get; set; }
}