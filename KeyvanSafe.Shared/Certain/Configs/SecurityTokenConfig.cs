namespace KeyvanSafe.Shared.Certain.Configs;

public class SecurityTokenConfig
{
    public const string Key = "SecurityToken";

    public string Issuer { get; set; } = "https://localhost";
    public string Audience { get; set; } = "https://localhost";
    public string AccessTokenSecretKey { get; set; } = "WQ7+dPhLEHdhdaKNzu!ck-fg86TPhUfd#E&&Qq+=vUtfxJ!@sDfe#u^prXW2&Qhmy33u!@e?5-xb*";
    public TimeSpan AccessTokenLifetime { get; set; } = TimeSpan.FromDays(7);
    public string RefreshTokenSecretKey { get; set; } = "WQ7+dPsdjifdsklhFBNzudsifpifsiPhUAPOSIDPIOUFDIOshF#u^prXW2&Qhmy33u!@e5455sdfd";
    public TimeSpan RefreshTokenLifetime { get; set; } = TimeSpan.FromDays(7);
}