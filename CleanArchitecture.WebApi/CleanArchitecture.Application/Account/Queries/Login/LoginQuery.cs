using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Account.Queries.Login
{
    public class LoginQuery :  IRequest<object>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        //public string Key { get; set; }
        public string DiviceTokenId { get; set; }
    }
}
