using KeyvanSafe.Shared.Certain.Enums;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Permissions;
using KeyvanSafe.Shared.Infrastructure.Pagination;

namespace KeyvanSafe.Shared.Persistence.Extensions.Identity;

public static class PermissionQueryableExtension
{
    public static IQueryable<Permission> ApplyFilter(this IQueryable<Permission> query, DefaultPaginationFilter filter)
    {
        // Filter by Value
        if (!string.IsNullOrEmpty(filter.StringValue))
            query = query.Where(x => x.Value.ToLower().Contains(filter.StringValue.ToLower().Trim()));

        // Filter by User Role ids
        if (filter.IntValueList != null)
            query = query.Where(x => x.Roles.Any(x => filter.IntValueList.Contains(x.PermissionId)));

        // Filter by Name
        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower().Trim()));

        // Filter by Title
        if (!string.IsNullOrEmpty(filter.Title))
            query = query.Where(x => x.Title.ToLower().Contains(filter.Title.ToLower().Trim()));

        return query;
    }

    public static IQueryable<Permission> ApplySort(this IQueryable<Permission> query, SortByEnum? sortBy)
    {
        return sortBy switch
        {
            SortByEnum.CreationDate => query.OrderBy(x => x.CreatedAt),
            SortByEnum.CreationDateDescending => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderByDescending(x => x.Id)
        };
    }
}