using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Common.ApiResponse
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            Status = 0;
            Message = ResponseMessage.Error;
            StatusCode = ResponseCode.BadRequest;
        }
        public string Message { get; set; }
        public object ResponseData { get; set; }
        public int Status { get; set; }
        public int StatusCode { get; set; }
        public int Total { get; set; }
    }
}
