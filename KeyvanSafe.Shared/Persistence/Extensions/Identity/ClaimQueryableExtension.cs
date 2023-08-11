using KeyvanSafe.Shared.Certain.Enums;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Claims;
using KeyvanSafe.Shared.Infrastructure.Pagination;

namespace KeyvanSafe.Shared.Persistence.Extensions.Identity;

public static class ClaimQueryableExtension
{
    public static IQueryable<Claim> ApplyFilter(this IQueryable<Claim> query, DefaultPaginationFilter filter)
    {
        // Filter By User Id
        if (filter.IntValue.HasValue || filter.IntValue != 0)
            query = query.Where(x => x.Value.ToLower().Contains(filter.StringValue.ToLower().Trim()));

        // Filter By Value
        if (!string.IsNullOrEmpty(filter.StringValue))
            query = query.Where(x => x.Value.ToLower().Contains(filter.StringValue.ToLower().Trim()));

        return query;
    }

    public static IQueryable<Claim> ApplySort(this IQueryable<Claim> query, SortByEnum? sortBy)
    {
        return sortBy switch
        {
            SortByEnum.CreationDate => query.OrderBy(x => x.CreatedAt),
            SortByEnum.CreationDateDescending => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderByDescending(x => x.Id)
        };
    }
}