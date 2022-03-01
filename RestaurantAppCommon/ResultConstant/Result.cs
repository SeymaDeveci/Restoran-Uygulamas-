using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantAppCommon.ResultConstant
{
    public class Result<T> : IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public T Data { get; set; } //classın geriye dönüş değer tipi
        public int TotalCount { get; set; } //Şu kadar kayıt bulundu şeklinde

        public Result(bool isSuccess, string message)
            : this(isSuccess, message, default(T))
        {

        }

        public Result(bool isSuccess, string message, T data)
            : this(isSuccess, message, data, 0)
        {

        }

        public Result(bool isSuccess, string message, T data, int totalcount)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
            TotalCount = totalcount;
        }

    }
}
