namespace KeyvanSafe.Domain.EntityFramework.Interfaces.IdentityRepositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role> GetRoleByIdAsync(int id);
    Task<List<Role>> GetRolesByIdsAsync(IEnumerable<int> ids);
    Task<List<Role>> GetRolesByFilterAsync(DefaultPaginationFilter filter);
    Task<int> CountRolesByFilterAsync(DefaultPaginationFilter filter);
}