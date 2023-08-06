using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Users;

namespace KeyvanSafe.Shared.EntityFramework.Seeding.IdentitySeeds;

public static class UserRoleSeed
{
    public static List<UserRole> All => new List<UserRole>
    {
        new UserRole()
        {
            RoleId = 1,
            UserId = 1,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        }
    };
}