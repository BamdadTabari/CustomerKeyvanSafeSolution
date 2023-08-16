using KeyvanSafe.Shared.Certain.Enums;
using KeyvanSafe.Shared.Models.Dtos.Identity.ClaimDtos;

namespace KeyvanSafe.Shared.Models.Dtos.Identity.UserDtos;
public class UserDto : BaseDto
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
    public string Password { get; set; }
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
    public ICollection<ClaimDto> Claims { get; set; }
    public ICollection<UserRoleDto> UserRoles { get; set; }
    #endregion
}
