using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory1.DAL.Exceptions
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
