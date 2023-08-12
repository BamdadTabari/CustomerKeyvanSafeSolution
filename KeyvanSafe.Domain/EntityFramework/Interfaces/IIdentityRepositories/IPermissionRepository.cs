using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Permissions;
using KeyvanSafe.Shared.Infrastructure.Pagination;

namespace KeyvanSafe.Domain.EntityFramework.Interfaces.IIdentityRepositories;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<Permission> GetPermissionByIdAsync(int id);
    Task<List<Permission>> GetPermissionsByIdsAsync(IEnumerable<int> ids);
    Task<List<Permission>> GetPermissionsByFilterAsync(DefaultPaginationFilter filter);
    Task<int> CountPermissionsByFilterAsync(DefaultPaginationFilter filter);
}