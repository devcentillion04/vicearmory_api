using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViceArmory.Dal.Exceptions;
using ViceArmory.Utility;
using ViceArmory.Dal.Exceptions;
using ViceArmory.Dal;
using ViceArmory.Bal.Logger;

namespace ViceArmory.Controllers
{
    public class ViceArmoryBase: ControllerBase
    {
        public static string conStr;
        public static string dbType;
        public DbCrudOperationController objDbCrudOperationController = null;

        protected readonly ILogger logger;

        public ViceArmoryBase()
        {
            conStr = Startup.ConnectionString;
            objDbCrudOperationController = new DbCrudOperationController(conStr);
        }

        public ViceArmoryBase(ILogger logger)
        {
            this.logger = logger;
        }

        protected ApiResult<T> LogException<T>(Exception exception)
        {
            return ApiResult<T>.Exception(this.logger.LogException(exception));
        }
    }
}
