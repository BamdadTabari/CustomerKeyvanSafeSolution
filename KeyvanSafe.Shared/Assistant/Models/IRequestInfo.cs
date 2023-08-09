namespace KeyvanSafe.Shared.Assistant.Models;

public interface IRequestInfo
{
    RequestInfo RequestInfo { get; }
}

public class RequestInfo
{
    public int? UserId { get; set; }
    public string? IpAddress { get; set; }
}