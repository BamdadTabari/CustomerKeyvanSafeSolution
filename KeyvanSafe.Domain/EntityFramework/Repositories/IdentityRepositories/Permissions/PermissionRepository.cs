using KeyvanSafe.Domain.EntityFramework.Interfaces.IIdentityRepositories;
using KeyvanSafe.Shared.Assistant.Extension;
using KeyvanSafe.Shared.EntityFramework.Configs;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Permissions;
using KeyvanSafe.Shared.Infrastructure.Pagination;
using KeyvanSafe.Shared.Persistence.Extensions.Identity;
using Microsoft.EntityFrameworkCore;

namespace KeyvanSafe.Domain.EntityFramework.Repositories.IdentityRepositories.Permissions;

public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    private readonly IQueryable<Permission> _queryable;

    public PermissionRepository(AppDbContext context) : base(context)
    {
        _queryable = DbContext.Set<Permission>();
    }

    public async Task<Permission> GetPermissionByIdAsync(int id)
    {
        return await _queryable
            .Include(x => x.Roles)
            .SingleOrDefaultAsync(x => x.Id == id) ?? new Permission();
    }

    public async Task<List<Permission>> GetPermissionsByIdsAsync(IEnumerable<int> ids)
    {
        var query = _queryable;

        query = query.AsNoTracking()
            .Include(x => x.Roles);

        // Filter by ids
        if (ids?.Any() == true)
            query = query.Where(x => ids.Contains(x.Id));

        return await query.ToListAsync();
    }

    public async Task<List<Permission>> GetPermissionsByFilterAsync(DefaultPaginationFilter filter)
    {
        try
        {
            var query = _queryable;

            query = query.AsNoTracking()
                .Include(x => x.Roles);

            query = query.ApplyFilter(filter);
            query = query.ApplySort(filter.SortByEnum);

            return await query.Paginate(filter.Page, filter.PageSize).ToListAsync();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<int> CountPermissionsByFilterAsync(DefaultPaginationFilter filter)
    {
        var query = _queryable;

        query = query.AsNoTracking()
            .Include(x => x.Roles);

        query = query.ApplyFilter(filter);

        return await query.CountAsync();
    }
}