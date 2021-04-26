using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Authentication.API.DomainModels.Entities.Identity
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
