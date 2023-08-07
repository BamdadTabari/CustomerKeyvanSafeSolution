using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Roles;

namespace KeyvanSafe.Shared.EntityFramework.Seeding.IdentitySeeds;

public static class RolePermissionSeed
{
    public static List<RolePermission> All => new List<RolePermission>()
    {
        new RolePermission()
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            PermissionId = 1,
            RoleId = 1,
        },
        new RolePermission()
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            PermissionId = 2,
            RoleId = 1,
        },
        new RolePermission()
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            PermissionId = 3,
            RoleId = 1,
        },
        new RolePermission()
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            PermissionId = 4,
            RoleId = 1,
        },
        new RolePermission()
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            PermissionId = 5,
            RoleId = 1,
        },
        new RolePermission()
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            PermissionId = 6,
            RoleId = 1,
        },
    };
}