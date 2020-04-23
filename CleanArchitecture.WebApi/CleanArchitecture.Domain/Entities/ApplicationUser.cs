
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CleanArchitecture.Domain.Entities
{
   
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50), Required]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }
        public int UserType { get; set; }
        //public string RegistrationNumber { get; set; }
    }
}
