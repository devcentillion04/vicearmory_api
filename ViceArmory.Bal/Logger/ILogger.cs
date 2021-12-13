using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.Dal.Exceptions;

namespace ViceArmory.Bal.Logger
{
    public interface ILogger
    {
        ExceptionModel LogException(Exception exception, string parentExceptionId = null);
    }
}

