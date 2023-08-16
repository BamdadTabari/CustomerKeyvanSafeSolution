using KeyvanSafe.Shared.Certain.Enums;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Users;
using KeyvanSafe.Shared.Infrastructure.Pagination;

namespace KeyvanSafe.Shared.Persistence.Extensions.Identity;

public static class UserQueryableExtension
{
    public static IQueryable<User> ApplyFilter(this IQueryable<User> query, DefaultPaginationFilter filter)
    {
        // Filter by uSERNAME
        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(x =>
                x.UserName.ToLower().Contains(filter.Name.ToLower().Trim()));

        // Filter by email
        if (!string.IsNullOrEmpty(filter.StringValue))
            query = query.Where(x => x.Email.ToLower().Contains(filter.StringValue.ToLower().Trim()));

        // Filter by Mobile
        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x => x.Mobile.ToLower().Contains(filter.Keyword.ToLower().Trim()));

        return query;
    }

    public static IQueryable<User> ApplySort(this IQueryable<User> query, UserSortByEnum? sortBy)
    {
        return sortBy switch
        {
            UserSortByEnum.CreationDate => query.OrderBy(x => x.CreatedAt),
            UserSortByEnum.CreationDateDescending => query.OrderByDescending(x => x.CreatedAt),
            UserSortByEnum.LastLoginDate => query.OrderBy(x => x.LastLoginDate),
            UserSortByEnum.LastLoginDateDescending => query.OrderByDescending(x => x.LastLoginDate),
            _ => query.OrderByDescending(x => x.Id)
        };
    }
}