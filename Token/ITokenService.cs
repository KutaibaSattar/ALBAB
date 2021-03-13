using System.Threading.Tasks;

using ALBAB.Entities.AppAccounts;

namespace ALBAB.Token
{
    public interface ITokenService
    {
       Task<string> CreateToken(AppUser user);
        
    }
}