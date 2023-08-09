using KeyvanSafe.Shared.Assistant.Models;
using Microsoft.AspNetCore.Http;

namespace KeyvanSafe.Shared.Assistant.Extension;

public static class HttpRequestExtension
{
    private static int GetUserId(this HttpRequest request)
    {
        return request.GetUserId();
    }

    private static string GetIpAddress(this HttpRequest request)
    {
        return request?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "";
    }

    public static RequestInfo GetRequestInfo(this HttpRequest request)
    {
        return new RequestInfo
        {
            UserId = request.GetUserId(),
            IpAddress = request.GetIpAddress()
        };
    }
}