using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory1.DAL
{
    public class DbCrudOperationController
    {
        string _conStr;
        MySqlDataAdapter da = null;
        MySqlConnection con = null;

        public DbCrudOperationController(string conStr)
        {
            _conStr = conStr;
            da = new MySqlDataAdapter();
            con = new MySqlConnection(_conStr);
        }

        public DataSet GetDataSetFromQuery(string query)
        {
            try
            {
                DataSet ds = new DataSet();

                using (MySqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
                return ds;
            }
            catch
            {
                throw;
            }
        }

        public string ExecuteQuery(string query)
        {
            string msg = "Fail";
            try
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {

                    cmd.CommandText = query;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    msg = "Success";

                }

            }
            catch (Exception ex)
            {
                msg = "Fail";
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return msg;
        }

    }
}
