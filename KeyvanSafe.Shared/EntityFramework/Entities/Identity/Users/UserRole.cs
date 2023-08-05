using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyvanSafe.Shared.EntityFramework.Entities.Identity.Users;

public class UserRole : BaseEntity
{
    #region Navigations

    public int RoleId { get; set; }
    public int UserId { get; set; }

    public User User { get; set; }
    public Role Role { get; set; }

    #endregion
}
public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {

        builder.HasKey(x => new { x.UserId, x.RoleId });

        #region Navigations

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.UserId);

        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.RoleId);

        #endregion
    }
}