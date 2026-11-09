using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerceOrder.Core
{
    public class Result<T>
    {
        public T? Data { get; set;  } 
        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }

        [JsonIgnore]//bu alanın apide json olarak gözükmesini istemiyoruz
        public bool IsSuccess { get; set;  }

        public static Result<T> Success(T data, int statusCode)
        {
            return new Result<T> { Data = data, StatusCode = statusCode, IsSuccess = true };
        }

        public static Result<T> Fail(List<string> errors, int statusCode)
        {
            return new Result<T> { Errors = errors, StatusCode = statusCode, IsSuccess = false };
        }
        public static Result<T> Fail(string error, int statusCode)
        {
            return new Result<T> { Errors = new List<string> { error }, StatusCode = statusCode, IsSuccess = false };
        }

    }
}
