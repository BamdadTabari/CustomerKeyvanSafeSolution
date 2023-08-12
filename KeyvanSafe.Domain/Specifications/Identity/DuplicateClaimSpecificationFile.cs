using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Claims;
using KeyvanSafe.Shared.Persistence.Specifications;
using System.Linq.Expressions;

namespace KeyvanSafe.Domain.Specifications.Identity;

public class DuplicateClaimSpecificationFile : Specification<Claim>
{
    private readonly int _userId;
    private readonly int _permissionId;

    public DuplicateClaimSpecificationFile(int userId, int permissionId)
    {
        _userId = userId;
        _permissionId = permissionId;
    }

    public override Expression<Func<Claim, bool>> ToExpression()
    {
        return claim => claim.Value == _permissionId.ToString() && claim.UserId == _userId;
    }
}