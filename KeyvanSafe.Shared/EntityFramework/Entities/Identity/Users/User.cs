using KeyvanSafe.Shared.Assistant.Extension;
using KeyvanSafe.Shared.Certain.Constants;
using KeyvanSafe.Shared.Certain.Enums;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KeyvanSafe.Shared.EntityFramework.Entities.Identity.Users;

public class User : BaseEntity
{
    #region Identity

    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Mobile { get; set; }
    public bool IsMobileConfirmed { get; set; }

    public string Email { get; set; }
    //public bool IsEmailConfirmed { get; set; }

    #endregion

    #region Login

    public string PasswordHash { get; set; }
    public DateTime? LastPasswordChangeTime { get; set; }

    public int FailedLoginCount { get; set; }
    public DateTime? LockoutEndTime { get; set; }

    public DateTime? LastLoginDate { get; set; }

    #endregion

    #region Profile
    public UserStateEnum State { get; set; }

    #endregion

    #region Management

    public string SecurityStamp { get; set; }
    public string ConcurrencyStamp { get; set; }
    public bool IsLockedOut { get; set; }
    #endregion

    #region Navigations
    public ICollection<Claim> Claims { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    #endregion
}

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(b => b.UserName).IsUnique();

        #region Mappings

        builder.Property(b => b.UserName)
            .HasMaxLength(Defaults.UsernameMaxLength)
            .IsRequired();

        builder.Property(b => b.Mobile)
            .IsRequired();

        builder.Property(b => b.Email)
            .IsRequired();

        builder.Property(b => b.PasswordHash)
            .HasMaxLength(Defaults.PasswordHashLength)
            .IsRequired();

        builder.Property(b => b.SecurityStamp)
            .HasMaxLength(Defaults.SecurityStampLength)
            .IsFixedLength();

        builder.Property(b => b.ConcurrencyStamp)
            .IsConcurrencyToken()
            .HasMaxLength(Defaults.ConcurrencyStampLength);

        #endregion

        #region Conversions

        builder.Property(x => x.State)
            .HasConversion(new EnumToStringConverter<UserStateEnum>())
            .HasMaxLength(UserStateEnum.Active.GetMaxLength());

        #endregion

        #region Navigations


        builder
            .HasMany(x => x.Claims)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            // just for now, at this time I choose the "DeleteBehavior.Restrict" ,
            // because there is not any logic selected yet, so I just select the easy way
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.UserRoles)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            // just for now, at this time I choose the "DeleteBehavior.Restrict" ,
            // because there is not any logic selected yet, so I just select the easy way
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
    }
}