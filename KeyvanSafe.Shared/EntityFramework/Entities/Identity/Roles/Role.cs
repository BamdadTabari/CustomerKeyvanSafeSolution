using Dayana.Shared.Basic.MethodsAndObjects.Models;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyvanSafe.Shared.EntityFramework.Entities.Identity.Roles;

public class Role : BaseEntity
{
    public string Title { get; set; }

    #region Navigations

    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<RolePermission> RolePermission { get; set; }

    #endregion
}
public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        #region Navigations

        builder
            .HasMany(x => x.UserRoles)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.RolePermission)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

    }
}
