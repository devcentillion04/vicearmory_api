using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory.Bal.Database
{
    public interface IDatabaseService
    {
        IEnumerable<T> ExecuteQuery<T>(string query);

        IEnumerable<T> RecordList<T>(string storeProcName, object parameters);

        IEnumerable<T> ExecuteStoreProcedure<T>(string storeProcName, object parameters);

        string RecordAddUpdateDelete(string storeProcName, object parameters);
    }
}
