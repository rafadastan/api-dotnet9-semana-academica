using Microsoft.AspNetCore.Http;
using SemanaAcademica.Domain.Contracts.CrossCutting.Security.UserContext;

namespace SemanaAcademica.CrossCutting.Security.UserContexts
{
    public class UserHttpContext : IUserContext
    {
        private readonly IHttpContextAccessor _accessor;

        public UserHttpContext(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor?.HttpContext?.User?.Identity?.Name ?? "underfined";
        public string IpUser => _accessor?.HttpContext?.Request?.Headers["X-Forwarded-For"].ToString() ?? string.Empty;

        public bool IsAuthenticated()
        {
            return _accessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }
    }
}
