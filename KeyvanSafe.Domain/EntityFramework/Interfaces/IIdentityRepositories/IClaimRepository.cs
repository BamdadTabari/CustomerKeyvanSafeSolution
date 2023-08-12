using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Claims;
using KeyvanSafe.Shared.Infrastructure.Pagination;

namespace KeyvanSafe.Domain.EntityFramework.Interfaces.IdentityRepositories;

public interface IClaimRepository : IRepository<Claim>
{
    Task<Claim> GetClaimByIdAsync(int id);
    Task<List<Claim>> GetClaimsByIdsAsync(IEnumerable<int> ids);
    Task<List<Claim>> GetClaimsByFilterAsync(DefaultPaginationFilter filter);
    Task<int> CountClaimsByFilterAsync(DefaultPaginationFilter filter);
}