using System.Security.Claims;

namespace API.Extensions
{
    public static class GeneralExtentions
    {
        public static string GetUserName(this ClaimsPrincipal user){
           return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}