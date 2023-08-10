using KeyvanSafe.Shared.Models.Dtos.Identity.PermissionDtos;
using KeyvanSafe.Shared.Models.Dtos.Identity.UserDtos;

namespace KeyvanSafe.Shared.Models.Dtos.Identity.RoleDtos;
public class RoleDto : BaseDto
{
    public string Title { get; set; }

    #region Navigations

    public ICollection<UserRoleDto> UserRoles { get; set; }
    public ICollection<PermissionDto> RolePermission { get; set; }

    #endregion
}
