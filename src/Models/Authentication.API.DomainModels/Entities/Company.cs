using Authentication.API.DomainModels.Entities.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Authentication.API.DomainModels.Entities
{
    public class Company
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }

        public string Logo { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
