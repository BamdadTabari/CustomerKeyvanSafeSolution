namespace KeyvanSafe.Shared.Models.Dtos.Identity.Auth;
public class LoginResultDto
{
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
