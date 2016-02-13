using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

public class Service1 : IService {
    public CustomerData Get() {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr)) {
            using (SqlCommand cmd = new SqlCommand("SELECT Customer_name, Cryptobank_iban FROM Customer")) {
                using (SqlDataAdapter sda = new SqlDataAdapter()) {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable()) {
                        CustomerData customers = new CustomerData();
                        sda.Fill(customers.CustomersTable);
                        return customers;
                    }
                }
            }
        }
    }

    public void Insert(string name, string iban) {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr)) {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Customers (Customer_name, Cryptobank_iban) VALUES (@Name, @Iban)")) {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Iban", iban);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }

    public void Update(int customerId, string name, string iban) {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr)) {
            using (SqlCommand cmd = new SqlCommand("UPDATE Customers SET Customer_name = @Name, Customer_iban = @Iban WHERE CustomerId = @CustomerId")) {
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Iban", iban);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }

    public void Delete(int customerId) {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr)) {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE CustomerId = @CustomerId")) {
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}