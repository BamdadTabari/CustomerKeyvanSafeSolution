using KeyvanSafe.Domain.EntityFramework.Interfaces.IdentityRepositories;
using KeyvanSafe.Domain.EntityFramework.Interfaces.UnitOfWorks;
using KeyvanSafe.Domain.EntityFramework.Repositories.IdentityRepositories.Claims;
using KeyvanSafe.Domain.EntityFramework.Repositories.IdentityRepositories.Permissions;
using KeyvanSafe.Domain.EntityFramework.Repositories.IdentityRepositories.Roles;
using KeyvanSafe.Domain.EntityFramework.Repositories.IdentityRepositories.Users;
using KeyvanSafe.Shared.EntityFramework.Configs;

namespace KeyvanSafe.Domain.EntityFramework.Repositories.UnitOfWorks;

public class UnitOfWorkIdentity : IUnitOfWorkIdentity
{
    private readonly AppDbContext _context;

    public IUserRepository Users { get; }
    public IRoleRepository Roles { get; }
    public IClaimRepository Claims { get; }
    public IPermissionRepository Permissions { get; }

    public UnitOfWorkIdentity(AppDbContext context)
    {
        _context = context;

        Users = new UserRepository(_context);
        Roles = new RoleRepository(_context);
        Claims = new ClaimRepository(_context);
        Permissions = new PermissionRepository(_context);
    }

    public async Task<bool> CommitAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}