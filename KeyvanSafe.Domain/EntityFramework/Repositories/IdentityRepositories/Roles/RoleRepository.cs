using KeyvanSafe.Domain.EntityFramework.Interfaces.IIdentityRepositories;
using KeyvanSafe.Shared.Assistant.Extension;
using KeyvanSafe.Shared.Certain.Enums;
using KeyvanSafe.Shared.EntityFramework.Configs;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Roles;
using KeyvanSafe.Shared.Infrastructure.Pagination;
using KeyvanSafe.Shared.Persistence.Extensions.Identity;
using Microsoft.EntityFrameworkCore;

namespace KeyvanSafe.Domain.EntityFramework.Repositories.IdentityRepositories.Roles;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    private readonly IQueryable<Role> _queryable;

    public RoleRepository(AppDbContext context) : base(context)
    {
        _queryable = DbContext.Set<Role>();
    }

    public async Task<Role> GetRoleByIdAsync(int id)
    {
        return await _queryable
            .Include(x => x.RolePermission)
            .ThenInclude(x => x.Permission)
            .SingleOrDefaultAsync(x => x.Id == id) ?? new Role();
    }

    public async Task<List<Role>> GetRolesByIdsAsync(IEnumerable<int> ids)
    {
        var query = _queryable;

        query = query.AsNoTracking()
            .Include(x => x.RolePermission)
            .ThenInclude(x => x.Permission);

        // Filter by ids
        if (ids?.Any() == true)
            query = query.Where(x => ids.Contains(x.Id));

        query = query.ApplySort(SortByEnum.CreationDate);

        return await query.ToListAsync();
    }

    public async Task<List<Role>> GetRolesByFilterAsync(DefaultPaginationFilter filter)
    {
        var query = _queryable;

        query = query.AsNoTracking();

        query = query.ApplyFilter(filter);
        query = query.ApplySort(filter.SortByEnum);

        return await query.Paginate(filter.Page, filter.PageSize).ToListAsync();
    }

    public async Task<int> CountRolesByFilterAsync(DefaultPaginationFilter filter)
    {
        var query = _queryable;

        query = query.ApplyFilter(filter);

        return await query.CountAsync();
    }
}