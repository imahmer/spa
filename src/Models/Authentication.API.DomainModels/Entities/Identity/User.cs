using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.API.DomainModels.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public long? EmployeeId { get; set; }

        public string UserPassword { get; set; } = "Default@1";

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FatherName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string PhoneNo { get; set; }

        public string MobileNo { get; set; }

        [Required]
        public char UserType { get; set; } = 'D';

        [Required]
        [DefaultValue(1)]
        public long CreatedById { get; set; } = 1;

        [Required]
        [Column(TypeName = "datetime2(7)")]
        [DefaultValue("GetUTCDate()")]
        public DateTime CreatedDate { get; set; }

        public long ModifiedById { get; set; }

        [Column(TypeName = "datetime2(7)")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [DefaultValue("false")]
        public Boolean IsActive { get; set; } = false;

        [Required]
        [DefaultValue("false")]
        public Boolean IsDeleted { get; set; } = false;

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
