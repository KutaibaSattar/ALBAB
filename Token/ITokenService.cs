using System.Threading.Tasks;
using ALBaB.Entities;

namespace ALBaB.Token
{
    public interface ITokenService
    {
       Task<string> CreateToken(AppUser user);
        
    }
}