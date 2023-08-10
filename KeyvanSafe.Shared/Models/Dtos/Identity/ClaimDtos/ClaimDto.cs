using KeyvanSafe.Shared.Models.Dtos.Identity.UserDtos;

namespace KeyvanSafe.Shared.Models.Dtos.Identity.ClaimDtos;
public class ClaimDto : BaseDto
{
    public string Value { get; set; }

    #region Navigations
    public int UserId { get; set; }

    public UserDto User { get; set; }

    #endregion
}
