﻿using KeyvanSafe.Domain.EntityFramework.Interfaces.IIdentityRepositories;
using KeyvanSafe.Shared.Assistant.Extension;
using KeyvanSafe.Shared.EntityFramework.Configs;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Claims;
using KeyvanSafe.Shared.Infrastructure.Pagination;
using KeyvanSafe.Shared.Persistence.Extensions.Identity;
using Microsoft.EntityFrameworkCore;

namespace KeyvanSafe.Domain.EntityFramework.Repositories.IdentityRepositories.Claims;

public class ClaimRepository : Repository<Claim>, IClaimRepository
{
    private readonly IQueryable<Claim> _queryable;

    public ClaimRepository(AppDbContext context) : base(context)
    {
        _queryable = DbContext.Set<Claim>();
    }

    public async Task<Claim> GetClaimByIdAsync(int id)
    {
        return await _queryable
            .SingleOrDefaultAsync(x => x.Id == id) ?? new Claim();
    }

    public async Task<List<Claim>> GetClaimsByIdsAsync(IEnumerable<int> ids)
    {
        var query = _queryable;

        query = query.AsNoTracking();

        // Filter by ids
        if (ids?.Any() == true)
            query = query.Where(x => ids.Contains(x.Id));

        return await query.ToListAsync();
    }

    public async Task<List<Claim>> GetClaimsByFilterAsync(DefaultPaginationFilter filter)
    {
        var query = _queryable;

        query = query.AsNoTracking();

        query = query.ApplyFilter(filter);
        query = query.ApplySort(filter.SortByEnum);

        return await query.Paginate(filter.Page, filter.PageSize).ToListAsync();
    }

    public async Task<int> CountClaimsByFilterAsync(DefaultPaginationFilter filter)
    {
        var query = _queryable;

        query = query.AsNoTracking();

        query = query.ApplyFilter(filter);

        return await query.CountAsync();
    }
}