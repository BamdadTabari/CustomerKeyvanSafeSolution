using KeyvanSafe.Domain.EntityFramework.Interfaces.IdentityRepositories;

namespace KeyvanSafe.Domain.EntityFramework.Interfaces.UnitOfWorks;

public interface IUnitOfWorkIdentity : IDisposable
{
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    IClaimRepository Claims { get; }
    IPermissionRepository Permissions { get; }

    Task<bool> CommitAsync();
}