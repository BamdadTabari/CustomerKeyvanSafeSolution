using KeyvanSafe.Domain.EntityFramework.Interfaces.IIdentityRepositories;

namespace KeyvanSafe.Domain.EntityFramework.Interfaces.IUnitOfWorks;

public interface IUnitOfWorkIdentity : IDisposable
{
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    IClaimRepository Claims { get; }
    IPermissionRepository Permissions { get; }

    Task<bool> CommitAsync();
}