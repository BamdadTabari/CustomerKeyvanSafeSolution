using KeyvanSafe.Shared.Certain.Enums;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Roles;
using KeyvanSafe.Shared.Infrastructure.Pagination;

namespace KeyvanSafe.Shared.Persistence.Extensions.Identity;

public static class RoleQueryableExtension
{
    public static IQueryable<Role> ApplyFilter(this IQueryable<Role> query, DefaultPaginationFilter filter)
    {
        // Filter by permission ids
        if (filter.IntValueList != null)
            query = query.Where(x => x.UserRoles.Any(x => filter.IntValueList.Contains(x.RoleId)));

        // Filter by User Role ids
        if (filter.IntValueList != null)
            query = query.Where(x => x.RolePermission.Any(x => filter.IntValueList.Contains(x.PermissionId)));

        // Filter by title
        if (!string.IsNullOrEmpty(filter.Title))
            query = query.Where(x => x.Title.ToLower().Contains(filter.Title.ToLower().Trim()));

        return query;
    }

    public static IQueryable<Role> ApplySort(this IQueryable<Role> query, SortByEnum? sortBy)
    {
        return sortBy switch
        {
            SortByEnum.CreationDate => query.OrderBy(x => x.CreatedAt),
            SortByEnum.CreationDateDescending => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderByDescending(x => x.Id)
        };
    }
}