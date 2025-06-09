using System.Security.Claims;

namespace BreweryAPI.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        Guid GetUserId {  get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor contextAccessor;
        public ClaimsPrincipal User => contextAccessor.HttpContext?.User;
        public Guid GetUserId => Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            contextAccessor = httpContextAccessor;
        }
    }
}
