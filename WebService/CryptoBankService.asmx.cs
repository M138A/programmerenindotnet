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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This method is used to register a user.
        /// </summary>
        /// <param name="user">This is the user where a account needs to be registered for.</param>
        [WebMethod]
        public void RegisterUser(UserModel user)
        {
            databaseforassignmentEntities1 context = new databaseforassignmentEntities1();
            User DBuser = new User();

            if (context.Users.SingleOrDefault(u => u.email == user.email) == null)
            {
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
        }
        
        /// <summary>
        /// This method is used to transfer money from one account to the other.
        /// </summary>
        /// <param name="amountOfBitCoins">The ammount of bitcoins that the sender wants to send to the receiver.</param>
        /// <param name="receiverAccountNumber">The account number of the person that is receiving the bitcoins.</param>
        /// <param name="senderAccountNumber">The account number of the person that is sending the bitcoins.</param>
        /// <returns>A boolean value based on the given inputs and the result of the checks.</returns>
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

        /// <summary>
        /// This method is used to find all the users in the database that have a status of active.
        /// </summary>
        /// <returns>All the users in the database that have a status of active.</returns>
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

        /// <summary>
        /// This method is used to find all the transactions of a certain user.
        /// </summary>
        /// <param name="accountNumber">The account number of the person that is searched on.</param>
        /// <returns>All the transactions of a certain user.</returns>
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
               
        /// <summary>
        /// This method is used to find all the transactions of a certain type.
        /// </summary>
        /// <param name="type">This is the type that is searched on.</param>
        /// <param name="accountNumber">This is the account number that is searched on.</param>
        /// <returns>All the transactions of a certain type.</returns>
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

        /// <summary>
        /// This method is used to find all the transactions where the selected user is the sender.
        /// </summary>
        /// <param name="sender">The account number of the selected user.</param>
        /// <returns>All the transactions where the selected user is the sender.</returns>
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

        /// <summary>
        /// This method is used to find all the transactions where the selected user is the receiver.
        /// </summary>
        /// <param name="receiver">The account number of the selected user.</param>
        /// <returns>All the transactions where the selected user is the receiver.</returns>
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

        /// <summary>
        /// This method is used to find all the data in the database from a certain user.
        /// </summary>
        /// <param name="accountNumber">The account number of the user that is searched on.</param>
        /// <returns>A list of users.</returns>
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

        /// <summary>
        /// This method is used to set a user to inactive in the database.
        /// </summary>
        /// <param name="userAccountnumber">The account number of the user that needs to be set to inactive.</param>
        [WebMethod]
        public void DeleteUser(string userAccountnumber)
        {
            databaseforassignmentEntities1 context = new databaseforassignmentEntities1();

            var user = context.Users.SingleOrDefault(a => a.accountnumber == userAccountnumber);

            user.status = "inactive";

            context.SaveChanges();
        }

        /// <summary>
        /// Updates the balance of the user according to his deposit/withdrawal
        /// </summary>
        /// <param name="accountNumber">the logged in user account number</param>
        /// <param name="amount">the deposited amount</param>
        [WebMethod]
        public void MakeDepositWIthdrawal(string accountNumber, double amount, bool deposit) {
            databaseforassignmentEntities1 context = new databaseforassignmentEntities1();

            var user = context.Users.SingleOrDefault(u => u.accountnumber == accountNumber);

            if (deposit)
            {
                user.balance += amount;

                context.transactions.Add(new transaction
                {
                    transactiontype = "deposit",
                    date = DateTime.Now,
                    sender = accountNumber,
                    receiver = accountNumber,
                    amount = amount
                });

                context.SaveChanges();
            }
            else
            {
                if (user.balance - amount >= 0)
                    user.balance -= amount;

                context.transactions.Add(new transaction
                {
                    transactiontype = "withdrawing",
                    date = DateTime.Now,
                    sender = accountNumber,
                    receiver = accountNumber,
                    amount = amount
                });

                context.SaveChanges();
            }
        }

    }
}