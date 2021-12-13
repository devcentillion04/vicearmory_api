using System;
using System.Collections.Generic;
using System.Text;

namespace ViceArmory.Dal.Exceptions
{
    public class ExceptionModel
    {
        public string ExceptionId { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public string Source { get; set; }

        public ExceptionModel InnerException { get; set; }
    }
}
