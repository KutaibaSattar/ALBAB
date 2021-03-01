using System.Linq;
using System.Security.Claims;

namespace ALBaB.Extensions
{
    public static class ClaimsPrinciplalExtensions
    {
        public static string RetrieveUserIdFromPrincipal( this ClaimsPrincipal user)
        {

            return user?.Claims?.FirstOrDefault( x => x.Type == ClaimTypes.Email)?.Value;
            
        }
    }
}