

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CleanArchitecture.Domain.Entities
{
    [Table("User", Schema = "MK")]
    public class User
    {
        public User()
        {
            UserId = GUIDGenerator.Generate();
        }
        public Guid UserId { get; set; }
        [Required]
        public string UserById { get; set; }
        public ApplicationUser UserBy { get; set; }
        [StringLength(100), Required]
        public string UserName { get; set; }
        [StringLength(50), Required]
        public string PhoneNo { get; set; }
        public bool isLoginFirstTime { get; set; } = true;
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Token { get; set; }
    }
}
