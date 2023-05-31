using System.Security.Claims;

namespace Services.HttpContex
{
    public interface IUserContext
    {
        ClaimsPrincipal User { get; }
    }
}
