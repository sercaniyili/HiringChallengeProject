using HiringChallange.Domain.Entities.Identity;

namespace HiringChallange.Application.Interfaces.Token
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(AppUser user);
    }
}
