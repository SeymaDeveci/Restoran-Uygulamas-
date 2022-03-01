using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantAppCommon.ResultConstant
{
    public interface IResult
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }
}
