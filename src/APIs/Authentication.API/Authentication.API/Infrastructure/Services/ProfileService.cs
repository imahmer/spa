using Authentication.API.DomainModels.Entities.Identity;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.API.Infrastructure.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;
        protected UserManager<User> _userManager;

        public ProfileService(UserManager<User> userManager, IUserClaimsPrincipalFactory<User> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var userRoles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>();

            foreach (string roleName in userRoles)
            {
                claims.AddRange(GetUserClaims(user, roleName));
            }
            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = (user != null) && user.IsActive;
        }

        public static Claim[] GetUserClaims(User user, string roleName)
        {
            return new Claim[]
            {
                new Claim("EmployeeId", user.EmployeeId.ToString()),//("user_id", user.UserId.ToString() ?? ""),
		        new Claim(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),//(!string.IsNullOrEmpty(user.Firstname) && !string.IsNullOrEmpty(user.Lastname)) ? (user.Firstname + " " + user.Lastname) : ""),
		        new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roleName)
            };
        }
    }
}
