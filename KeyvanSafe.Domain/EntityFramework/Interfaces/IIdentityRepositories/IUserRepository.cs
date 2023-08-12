using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Users;
using KeyvanSafe.Shared.Infrastructure.Pagination;

namespace KeyvanSafe.Domain.EntityFramework.Interfaces.IdentityRepositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByUsernameAsync(string username);
    Task<int> CountUsersByFilterAsync(DefaultPaginationFilter filter);
    Task<List<User>> GetUsersByIdsAsync(IEnumerable<int> ids);
    Task<List<User>> GetUsersByFilterAsync(DefaultPaginationFilter filter);
}