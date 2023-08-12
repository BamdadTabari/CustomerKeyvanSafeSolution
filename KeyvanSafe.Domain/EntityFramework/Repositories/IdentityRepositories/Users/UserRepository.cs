using KeyvanSafe.Domain.EntityFramework.Interfaces.IdentityRepositories;
using KeyvanSafe.Shared.Assistant.Extension;
using KeyvanSafe.Shared.Certain.Enums;
using KeyvanSafe.Shared.EntityFramework.Configs;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Users;
using KeyvanSafe.Shared.Infrastructure.Pagination;
using KeyvanSafe.Shared.Persistence.Extensions.Identity;
using Microsoft.EntityFrameworkCore;

namespace KeyvanSafe.Domain.EntityFramework.Repositories.IdentityRepositories.Users;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly IQueryable<User> _queryable;


    public UserRepository(AppDbContext context) : base(context)
    {
        _queryable = DbContext.Set<User>();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _queryable
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (user == null)
            throw new NullReferenceException("user not found with this id");

        return user;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        var user = await _queryable
            .Include(x => x.UserRoles)
            .SingleOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());

        if (user == null)
            throw new NullReferenceException("user not found with this name ");

        return user;
    }

    public async Task<int> CountUsersByFilterAsync(DefaultPaginationFilter filter)
    {
        var query = _queryable;

        query = query.ApplyFilter(filter);

        return await query.CountAsync();
    }

    public async Task<List<User>> GetUsersByIdsAsync(IEnumerable<int> ids)
    {
        var query = _queryable;
        query = query.AsNoTracking()
            .Include(x => x.UserRoles);

        // Filter by ids
        if (ids?.Any() == true)
            query = query.Where(x => ids.Contains(x.Id));

        query = query.ApplySort(UserSortByEnum.CreationDate);

        return await query.ToListAsync();
    }

    public async Task<List<User>> GetUsersByFilterAsync(DefaultPaginationFilter filter)
    {
        var query = _queryable;

        query = query.AsNoTracking();

        query = query.ApplyFilter(filter);
        query = query.ApplySort(filter.UserSortByEnum);

        return await query.Paginate(filter.Page, filter.PageSize).ToListAsync();
    }
}