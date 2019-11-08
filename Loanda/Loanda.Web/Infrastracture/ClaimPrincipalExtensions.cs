using System.Security.Claims;

namespace Loanda.Web.Infrastracture
{
    public static class ClaimPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        => claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
