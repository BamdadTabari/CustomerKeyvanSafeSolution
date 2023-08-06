using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Permissions;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Roles;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Users;
using KeyvanSafe.Shared.EntityFramework.Seeding.IdentitySeeds;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KeyvanSafe.Shared.EntityFramework.Configs;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.Entity<UserRole>().HasData(UserRoleSeed.All);
        modelBuilder.Entity<Role>().HasData(RoleSeed.All);
        modelBuilder.Entity<User>().HasData(UserSeed.All);
        modelBuilder.Entity<Permission>().HasData(PermissionSeed.All);
        modelBuilder.Entity<RolePermission>().HasData(RolePermissionSeed.All);
        // Creating Model
        base.OnModelCreating(modelBuilder);
    }
}
