using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Services.JwtHandler
{
    public interface IJwtTokenMethod
    {
        Task<string> GenerateJwtToken(Employee identityUser);
    }
}
