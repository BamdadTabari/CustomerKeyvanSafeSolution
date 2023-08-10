using KeyvanSafe.Shared.Models.Dtos.Identity.RoleDtos;

namespace KeyvanSafe.Shared.Models.Dtos.Identity.PermissionDtos;
public class PermissionDto : BaseDto
{
    public string Name { get; set; }
    public string Title { get; set; }
    public string Value { get; set; }

    #region Navigations

    public ICollection<RolePermissionDto> Roles { get; set; }

    #endregion
}
