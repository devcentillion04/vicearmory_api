using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViceArmory.Dal.Exceptions;

namespace ViceArmory.Utility
{

    public class ApiResult<T>
    {
        public T Data { get; set; }

        public bool IsSuccess { get; set; }


        public ExceptionModel Error { get; set; }

        public static ApiResult<T> Success(T data)
        {
            var returnValue = new ApiResult<T>();
            returnValue.Data = data;
            returnValue.IsSuccess = true;
            return returnValue;
        }

        public static ApiResult<T> Exception(ExceptionModel model)
        {
            var returnValue = new ApiResult<T>();
            returnValue.Error = model;
            returnValue.IsSuccess = false;
            return returnValue;
        }
    }
}
