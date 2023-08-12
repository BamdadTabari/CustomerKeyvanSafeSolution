using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Roles;
using KeyvanSafe.Shared.Infrastructure.Pagination;

namespace KeyvanSafe.Domain.EntityFramework.Interfaces.IIdentityRepositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role> GetRoleByIdAsync(int id);
    Task<List<Role>> GetRolesByIdsAsync(IEnumerable<int> ids);
    Task<List<Role>> GetRolesByFilterAsync(DefaultPaginationFilter filter);
    Task<int> CountRolesByFilterAsync(DefaultPaginationFilter filter);
}