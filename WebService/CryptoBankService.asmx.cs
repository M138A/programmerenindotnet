using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using CryptoBank.Models;

namespace WebService
{
    /// <summary>
    /// Summary description for CryptoBankService
    /// </summary>
    [WebService(Namespace = "http://localhost:46131/CryptoBankWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CryptoBankService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool ValidateLogin(string email, string password)
        {
            databaseforassignmentEntities1 context = new databaseforassignmentEntities1();

            var query = from user in context.Users
                         where user.email == email && user.password.ToString() == password && user.status == "active"
                         select user;

            List<User> users = new List<User>();
            foreach (var u in query)
            {
                users.Add(u);
            }

            return (users.Count > 0) ? true : false;
        }

        [WebMethod]
        public UserModel GetUserByLogin(string email, string password)
        {
            databaseforassignmentEntities1 context = new databaseforassignmentEntities1();
            var query = from user in context.Users
                        where user.email == email && user.password.ToString() == password && user.status == "active"
                        select user;

            UserModel loginUser = new UserModel();
            foreach (var u in query)
            {
                loginUser.accountnumber = u.accountnumber;
                loginUser.balance = u.balance;
                loginUser.city = u.city;
                loginUser.email = u.email;
                loginUser.name = u.name;
                loginUser.password = u.password;
            }

            return loginUser;
        }

        [WebMethod]
        public void RegisterUser(UserModel user)
        {
            databaseforassignmentEntities1 context = new databaseforassignmentEntities1();
            User DBuser = new User();

            DBuser.accountnumber = user.accountnumber;
            DBuser.balance = 0;
            DBuser.city = user.city;
            DBuser.email = user.email;
            DBuser.name = user.name;
            DBuser.password = user.password;
            DBuser.status = "active";

            context.Users.Add(DBuser);

            context.SaveChanges();
        }
        

        [WebMethod]
        public bool TransferMoney(int amountOfBitCoins, string receiverAccountNumber, string senderAccountNumber)
        {
            databaseforassignmentEntities1 context = new databaseforassignmentEntities1();

            var resultSender = context.Users.SingleOrDefault(a => a.accountnumber == senderAccountNumber);
            var resultReceiver = context.Users.SingleOrDefault(a => a.accountnumber == receiverAccountNumber);

            if (resultSender == null || resultReceiver == null) return false;

            if ((resultSender.balance - amountOfBitCoins) <= 0) return false;

            resultSender.balance = resultSender.balance - amountOfBitCoins;
            resultReceiver.balance = resultReceiver.balance + amountOfBitCoins;

            context.transactions.Add(new transaction
            {
                transactiontype = "transfer",
                date = DateTime.Now,
                sender = senderAccountNumber,
                receiver = receiverAccountNumber,
                amount = amountOfBitCoins
            });

            context.SaveChanges();
            return true;
        }

        [WebMethod]
        public List<string> AllUsers()
        {
            databaseforassignmentEntities1 context = new databaseforassignmentEntities1();

            var query = from user in context.Users
                        where user.status == "active"
                        select user;

            List<string> accountNumbers = new List<string>();
            foreach (var u in query)
            {
                accountNumbers.Add(u.accountnumber);
            }

            return accountNumbers;
        }

        [WebMethod]
        public List<TransactionModel> AllTransactions(string accountNumber)
        {
            databaseforassignmentEntities1 context = new databaseforassignmentEntities1();

            var query = from transaction in context.transactions
                        where transaction.sender == accountNumber || transaction.receiver == accountNumber
                        select transaction;
            
            List<TransactionModel> transactions = new List<TransactionModel>();

            foreach (var t in query)
            {
                transactions.Add(new TransactionModel
                {
                    transactiontype = t.transactiontype,
                    date = t.date.ToString(),
                    sender = t.sender,
                    receiver = t.receiver,
                    amount = Convert.ToDouble(t.amount)
                });
            }

            return transactions;
        }
               
        [WebMethod]
        public List<TransactionModel> TransactionsPerType(string type, string accountNumber)
        {
            databaseforassignmentEntities1 context = new databaseforassignmentEntities1();

            var query = from transaction in context.transactions
                        where (transaction.sender == accountNumber || transaction.receiver == accountNumber) && transaction.transactiontype == type
                        select transaction;

            List<TransactionModel> transactions = new List<TransactionModel>();

            foreach (var t in query)
            {
                transactions.Add(new TransactionModel
                {
                    transactiontype = t.transactiontype,
                    date = t.date.ToString(),
                    sender = t.sender,
                    receiver = t.receiver,
                    amount = Convert.ToDouble(t.amount)
                });
            }

            return transactions;
        }

        [WebMethod]
        public List<TransactionModel> TransactionsPerSender(string sender)
        {
            databaseforassignmentEntities1 context = new databaseforassignmentEntities1();

            var query = from transaction in context.transactions
                        where transaction.sender == sender && transaction.transactiontype == "transfer"
                        select transaction;

            List<TransactionModel> transactions = new List<TransactionModel>();

            foreach (var t in query)
            {
                transactions.Add(new TransactionModel
                {
                    transactiontype = t.transactiontype,
                    date = t.date.ToString(),
                    sender = t.sender,
                    receiver = t.receiver,
                    amount = Convert.ToDouble(t.amount)
                });
            }

            return transactions;
        }

        [WebMethod]
        public List<TransactionModel> TransactionsPerReceiver(string receiver)
        {
            databaseforassignmentEntities1 context = new databaseforassignmentEntities1();

            var query = from transaction in context.transactions
                        where transaction.receiver == receiver && transaction.transactiontype == "transfer"
                        select transaction;

            List<TransactionModel> transactions = new List<TransactionModel>();

            foreach (var t in query)
            {
                transactions.Add(new TransactionModel
                {
                    transactiontype = t.transactiontype,
                    date = t.date.ToString(),
                    sender = t.sender,
                    receiver = t.receiver,
                    amount = Convert.ToDouble(t.amount)
                });
            }

            return transactions;
        }

        [WebMethod]
        public List<UserModel> Finduser(string accountNumber)
        {
            databaseforassignmentEntities1 context = new databaseforassignmentEntities1();

            var query = from user in context.Users
                        where user.accountnumber == accountNumber
                        select user;

            List<UserModel> users = new List<UserModel>();

            foreach (var u in query)
            {
                users.Add(new UserModel
                {
                    status = u.status,
                    password = u.password,
                    name = u.name,
                    email = u.email,
                    city = u.city,
                    accountnumber = u.accountnumber,
                    balance = u.balance
                });
            }

            return users;
        }

        [WebMethod]
        public void DeleteUser(string userAccountnumber)
        {
            databaseforassignmentEntities1 context = new databaseforassignmentEntities1();

            var user = context.Users.SingleOrDefault(a => a.accountnumber == userAccountnumber);

            user.status = "inactive";

            context.SaveChanges();
        }

    }
}
