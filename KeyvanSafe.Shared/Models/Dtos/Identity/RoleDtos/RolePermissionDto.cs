using KeyvanSafe.Shared.Models.Dtos.Identity.PermissionDtos;

namespace KeyvanSafe.Shared.Models.Dtos.Identity.RoleDtos;
public class RolePermissionDto : BaseDto
{
    #region Navigations

    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    public PermissionDto Permission { get; set; }
    public RoleDto Role { get; set; }

    #endregion
}
