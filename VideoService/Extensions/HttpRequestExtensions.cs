using Microsoft.AspNetCore.Http;

namespace VideoService.Extensions
{
    public static class HttpRequestExtensions
    {
        public static bool IsTrusted(this HttpRequest request)
        {
            var user = request.HttpContext.User;

            return user.Identity.IsAuthenticated;
        }
    }
}