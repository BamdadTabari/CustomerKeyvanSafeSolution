using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyvanSafe.Shared.EntityFramework.Entities.Identity.Claims;

public class Claim : BaseEntity
{
    public string Value { get; set; }

    #region Navigations
    public int UserId { get; set; }

    public User User { get; set; }

    #endregion
}

public class ClaimEntityConfiguration : IEntityTypeConfiguration<Claim>
{
    public void Configure(EntityTypeBuilder<Claim> builder)
    {
        builder.HasKey(x => x.Id);

        #region Mappings

        builder.Property(b => b.Value)
        .HasMaxLength(Defaults.LongLength1)
            .IsRequired();

        #endregion

        #region Navigations

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Claims)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}