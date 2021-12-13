using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.Bal.Constants;
using ViceArmory.Bal.Database;
using ViceArmory.Dal.Exceptions;

namespace ViceArmory.Bal.Logger
{
    public class ExceptionLogger: ILogger
    {
        private readonly IDatabaseService databaseService;

        public ExceptionLogger(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public ExceptionModel LogException(Exception exception, string parentExceptionId = null)
        {
            var exceptionId = this.databaseService.RecordAddUpdateDelete(ExceptionConstants.Add,
                new
                {
                    _message = exception.Message,
                    _source = exception.Source,
                    _stacktrace = exception.StackTrace,
                    _parentId = parentExceptionId
                });

            var returnValue = new ExceptionModel();
            returnValue.Message = exception.Message;
            returnValue.Source = exception.Source;
            returnValue.StackTrace = exception.StackTrace;
            returnValue.ExceptionId = exceptionId;

            while (exception.InnerException != null)
            {
                returnValue.InnerException = this.LogException(exception.InnerException, exceptionId);

            }

            return returnValue;
        }
    }
}
