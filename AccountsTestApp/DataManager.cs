using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace AccountsTestApp
{
    public static class DataManager
    {

        public static List<string> GetAccounts()
        {
            List<string> accountList = new List<string>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AccountsTestApp.Properties.Settings.AccountsConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT [Name] from Accounts;";
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        accountList.Add(reader.GetString(0));
                    }
                }
            }
            

            return accountList;
        }
    }
}
