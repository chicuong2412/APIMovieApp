using Microsoft.AspNetCore.Authorization;

namespace API.Authorization.Requirements
{
    public class PermissionRequirements : IAuthorizationRequirement
    {
        public PermissionRequirements(string permission)
        {
            Permission = permission;
        }
        public string Permission { get; }
    }
}
