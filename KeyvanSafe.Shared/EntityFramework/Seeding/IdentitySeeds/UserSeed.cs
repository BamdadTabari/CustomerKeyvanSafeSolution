using KeyvanSafe.Shared.Assistant.Helpers;
using KeyvanSafe.Shared.Certain.Constants;
using KeyvanSafe.Shared.Certain.Enums;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Users;

namespace KeyvanSafe.Shared.EntityFramework.Seeding.IdentitySeeds;

public static class UserSeed
{
    public static List<User> All => new List<User>()
    {
        new User()
        {
            Id = 1,
            IsMobileConfirmed = false,
            Email = "bamdadtabari@outlook.com",
            Mobile = "09301724389",
            State = UserStateEnum.Active,
            UserName = "Illegible_Owner",
            FirstName = "Bamdad",
            LastName = "Tabari",
            FullName = "Bamdad Tabari",
            PasswordHash = PasswordHasher.Hash("owner"),
            ConcurrencyStamp = StampGenerator.CreateSecurityStamp(Defaults.ConcurrencyStampLength),
            SecurityStamp = StampGenerator.CreateSecurityStamp(Defaults.SecurityStampLength),
            LastPasswordChangeTime = DateTime.Now,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        }
    };
}