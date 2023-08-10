using KeyvanSafe.Shared.Infrastructure.Pagination;

namespace KeyvanSafe.Shared.Models.Requests;
public record GetPermissionsByFilterRequest : DefaultPaginationFilter
{
    protected GetPermissionsByFilterRequest(int page, int pageSize) : base(page, pageSize)
    {
    }
    public GetPermissionsByFilterRequest()
    {
    }

    public int? RoleId { get; init; }
}