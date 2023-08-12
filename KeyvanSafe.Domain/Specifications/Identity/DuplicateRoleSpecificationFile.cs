using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Roles;
using KeyvanSafe.Shared.Persistence.Specifications;
using System.Linq.Expressions;

namespace KeyvanSafe.Domain.Specifications.Identity;

public class DuplicateRoleSpecificationFile : Specification<Role>
{
    private readonly string _title;

    public DuplicateRoleSpecificationFile(string title)
    {
        _title = title;
    }

    public override Expression<Func<Role, bool>> ToExpression()
    {
        return role => role.Title.ToLower() == _title.ToLower();
    }
}