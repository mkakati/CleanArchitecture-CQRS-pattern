using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CleanArchitecture.Domain.Entities
{
    public class BaseEntity
    {
        [DefaultValue("true")]
        public bool IsActive { get; set; }
        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
        //[ForeignKey("Users")]
        public string DeletedById { get; set; }
        public ApplicationUser DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        //[ForeignKey("Users")]
        //[Required]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        //[Required]
        public DateTime? CreatedDate { get; set; }
        //[ForeignKey("Users")]
        public string UpdateById { get; set; }
        public ApplicationUser UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        //public virtual User Users { get; set; }
        //public virtual User Users1 { get; set; }
        //public virtual User Users2 { get; set; }
    }
}
