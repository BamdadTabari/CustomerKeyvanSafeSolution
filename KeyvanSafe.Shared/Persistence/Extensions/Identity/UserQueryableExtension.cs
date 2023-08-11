namespace KeyvanSafe.Shared.Persistence.Extensions.Identity;

public static class UserQueryableExtension
{
    public static IQueryable<User> ApplyFilter(this IQueryable<User> query, CustomaizedPaginationFilterTwo<List<UserState>?, UserSortBy?> filter)
    {
        // Filter by keyword
        if (!string.IsNullOrEmpty(filter.keyword))
            query = query.Where(x =>
                x.Username.ToLower().Contains(filter.keyword.ToLower().Trim()));

        // Filter by email
        if (!string.IsNullOrEmpty(filter.StringValue))
            query = query.Where(x => x.Email.ToLower().Contains(filter.StringValue.ToLower().Trim()));

        // Filter by statuses
        if (filter.Value1?.Any() == true)
            query = query.Where(x => filter.Value1.Contains(x.State));

        return query;
    }

    public static IQueryable<User> ApplySort(this IQueryable<User> query, UserSortBy? sortBy)
    {
        return sortBy switch
        {
            UserSortBy.CreationDate => query.OrderBy(x => x.CreatedAt),
            UserSortBy.CreationDateDescending => query.OrderByDescending(x => x.CreatedAt),
            UserSortBy.LastLoginDate => query.OrderBy(x => x.LastLoginDate),
            UserSortBy.LastLoginDateDescending => query.OrderByDescending(x => x.LastLoginDate),
            _ => query.OrderByDescending(x => x.Id)
        };
    }
}