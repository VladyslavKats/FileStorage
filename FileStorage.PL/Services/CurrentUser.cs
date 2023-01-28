using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace FileStorage.PL.Services
{
    public class CurrentUser
    {
        private readonly HttpContext _httpContext;

        public CurrentUser(IHttpContextAccessor _httpContextAccessor)
        {
            _httpContext = _httpContextAccessor.HttpContext;
        }

        public string GetId()
        {
            return _httpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                    .Value;
        }
    }
}
