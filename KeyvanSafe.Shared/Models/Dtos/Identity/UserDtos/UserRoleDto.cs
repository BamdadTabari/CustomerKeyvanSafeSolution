using KeyvanSafe.Shared.Models.Dtos.Identity.RoleDtos;

namespace KeyvanSafe.Shared.Models.Dtos.Identity.UserDtos;
public class UserRoleDto : BaseDto
{
    #region Navigations

    public int RoleId { get; set; }
    public int UserId { get; set; }

    public UserDto User { get; set; }
    public RoleDto Role { get; set; }

    #endregion
}
