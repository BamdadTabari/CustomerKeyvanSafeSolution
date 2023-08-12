namespace KeyvanSafe.Domain.EntityFramework.Interfaces.IdentityRepositories;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<Permission> GetPermissionByIdAsync(int id);
    Task<List<Permission>> GetPermissionsByIdsAsync(IEnumerable<int> ids);
    Task<List<Permission>> GetPermissionsByFilterAsync(DefaultPaginationFilter filter);
    Task<int> CountPermissionsByFilterAsync(DefaultPaginationFilter filter);
}