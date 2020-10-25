using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Infrastructure
{
    public class Response<T>
    {
        public string Message { get; set; }
        public T Payload { get; set; }
        public bool Success { get; set; }
    }

    public static class Response
    {
        public static Response<T> OkResponse<T>(T obj)
        {
            return new Response<T>
            {
                Message = null,
                Payload = obj,
                Success = true
            };
        }

        public static Response<T> OkResponse<T>(T obj, string message)
        {
            return new Response<T>
            {
                Message = message,
                Payload = obj,
                Success = true
            };
        }

    }
}
