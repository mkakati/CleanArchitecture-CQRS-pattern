using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CleanArchitecture.Domain.Entities
{
    [Table("UserRegister", Schema = "MK")]


    public class UserRegister
    {   
        public UserRegister()
        {
            RegisterId = GUIDGenerator.Generate();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid RegisterId { get; set; }
        [StringLength(100), Required]
        public string UserName { get; set; }
        [StringLength(50), Required]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        //[StringLength(100)]
        //public string SSN { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        [StringLength(50), Required]
        public string PhoneNo { get; set; }
        //public int? OTP { get; set; }
        //public DateTime? OTPStartDateTime { get; set; }
        //public DateTime? OTPEndDateTime { get; set; }
        //public string RegistrationNumber { get; set; }
    }
}
