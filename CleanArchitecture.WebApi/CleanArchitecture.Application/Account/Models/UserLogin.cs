using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Account.Models
{
    public class UserLogin 
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public int UserRole { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
}
