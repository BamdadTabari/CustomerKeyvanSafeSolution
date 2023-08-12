using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Users;
using KeyvanSafe.Shared.Persistence.Specifications;
using System.Linq.Expressions;

namespace KeyvanSafe.Domain.Specifications.Identity;

public class DuplicateUserSpecificationFile : Specification<User>
{
    private readonly string _username;

    public DuplicateUserSpecificationFile(string username)
    {
        _username = username;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Username.ToLower() == _username.ToLower();
    }
}