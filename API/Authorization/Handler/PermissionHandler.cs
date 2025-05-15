using API.Authorization.Requirements;
using API.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace API.Authorization.Handler
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirements>
    {
        private readonly ExpiredJWTRepository _expiredJWTRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionHandler(ExpiredJWTRepository expiredJWTRepository, IHttpContextAccessor httpContextAccessor)
        {
            _expiredJWTRepository = expiredJWTRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirements requirement)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Split(" ").Last();

            var isExpired = await _expiredJWTRepository.IsTokenExpired(token);
            if (isExpired)
            {
                context.Fail();
                return;
            }

            if (context.User.HasClaim(c => c.Type == "Permission" && c.Value == requirement.Permission))
            {
                context.Succeed(requirement);
            }
            return;
        }
    }
}
