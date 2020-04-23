using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Common.ApiResponse
{
    public class ResponseMessage
    {
        public const string Error = "Some internal error occured";
        public const string UserExist = "Username already exist";
        public const string Success = "Record saved successfully";
        public const string PhoneExist = "Mobile number already exist";
        public const string InvalidXMLRSAkey = "Invalid XML RSA key";
        public const string InvalidKeySize = "Key size is not valid";
        public const string KeyIsNullOrEmpty = "Key is null or empty";
        public const string RSAKeyValue = "RSAKeyValue";
        public const string Modulus = "Modulus";
        public const string Exponent = "Exponent";
        public const string P = "P";
        public const string Q = "Q";
        public const string DP = "DP";
        public const string DQ = "DQ";
        public const string InverseQ = "InverseQ";
        public const string D = "D";
        public const string Expire = "Token has expired";
        public const string UserNotExist = "User does not exists";
        public const string InvalidPassword = "The password provided is incorret";
        
    }
}
