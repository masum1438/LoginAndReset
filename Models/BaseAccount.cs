using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace LoginandReset.Models
{
    public class BaseAccount
    {
        public bool VerifyUser(string UserName, string Password)
        {
            bool status = false;
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnString);

            try
            {
                sqlConnection.Open();
                string Query = "select * from OSTMember where name='" + UserName + "' and Password='" + Password + "'";
                SqlCommand cmd = new SqlCommand(Query, sqlConnection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 0;

                //cmd.ExecuteNonQuery();//insert delete update
                DataTable dataTable = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);    //fetch mode table data  
                adapter.Fill(dataTable);
                //SqlDataReader reader = cmd.ExecuteReader(); //fetch mode list data

                if (dataTable.Rows.Count > 0)
                {
                    status = true;
                }

                //transaction
                cmd.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                sqlConnection.Close();
            }
            return status;
        }
    }
}