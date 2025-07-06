using E_Commerce.Model;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace E_Commerce.Extensions
{
    public static class RoleClaimsExtension
    {
        [Authorize("Administrador")]
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            var result = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new("firstName", user.Name.Split(' ')[0])
            };
            result.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug)));
            return result;
        }
    }
}
