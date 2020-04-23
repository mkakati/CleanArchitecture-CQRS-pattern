using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Common.ApiResponse
{
    public class ResponseCode
    {
        public static int ServerError = 500;
        public static int BadGateway = 502;
        public static int Ok = 200;
        public static int BadRequest = 400;
        public static int Unauthorized = 401;
        public static int NotFound = 404;
    }
}
