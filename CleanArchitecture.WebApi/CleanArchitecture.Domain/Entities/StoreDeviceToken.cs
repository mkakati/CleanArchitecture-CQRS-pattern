using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CleanArchitecture.Domain.Entities
{
    public class StoreDeviceToken : BaseEntity
    {
        [Column("DiviceTokenId")]
        public Guid Id { get; set; }
        public string UserById { get; set; }
        public ApplicationUser UserBy { get; set; }
        public string TokenId { get; set; }
    }
}
