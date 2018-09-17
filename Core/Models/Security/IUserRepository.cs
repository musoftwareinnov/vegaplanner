using System.Security.Claims;
using System.Threading.Tasks;

namespace vegaplanner.Core.Models.Security
{
    public interface IUserRepository
    {
         void Add(InternalAppUser appUser);
         Task<InternalAppUser> Get(Claim userId);
    }
}