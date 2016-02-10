using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebService
{
    public class DataBase
    {
        SqlConnection con;
        SqlCommand com;
        SqlDataAdapter da;
        DataTable dt;
        const string connectionString = "Data Source=databaseforassignment.database.windows.net;Initial Catalog=databaseforassignment;Persist Security Info=True;User ID=cryptouser;Password=584tg7gu:";

        void Connect()
        {
            try
            {
                con = new SqlConnection("Data Source=databaseforassignment.database.windows.net;Initial Catalog=databaseforassignment;Persist Security Info=True;User ID=cryptouser;Password=584tg7gu:");
                con.Open();
            }
            catch (Exception)
            {
                Console.WriteLine("Could not connect to the database");
            }
            
        }

    }
}