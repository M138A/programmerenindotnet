using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebService
{
    public class DatabaseConnector
    {
        public SqlConnection ConnectToDb()
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Server=tcp:databaseforassignment.database.windows.net,1433;Database=databaseforassignment;User ID=cryptouser@databaseforassignment;Password=584tg7gu:;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            return cnn;
        }
        public void CloseConnection(SqlConnection conn)
        {
            conn.Close();
        }
    }
}