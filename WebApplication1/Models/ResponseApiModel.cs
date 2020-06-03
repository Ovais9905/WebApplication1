using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{    
    public class ResponseApiModel
    {
        public APIStatus Status { get; set; }

        public string Message { get; set; }

        public dynamic Data { get; set; }
    }

    public enum APIStatus
    {
        Failed,
        Success

    }
}
