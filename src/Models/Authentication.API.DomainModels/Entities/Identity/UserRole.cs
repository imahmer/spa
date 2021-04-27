using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.API.DomainModels.Entities.Identity
{
    public class UserRole : IdentityUserRole<int>
    {

        [Key, Column(Order = 2)]
        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
    }
}
