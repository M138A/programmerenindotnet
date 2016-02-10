using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdminApplication
{
    /// <summary>
    /// Interaction logic for Transactions.xaml
    /// </summary>
    public partial class Transactions : Window
    {
        private string currentUser;

        public Transactions(string user)
        {
            InitializeComponent();

            currentUser = user;
            fillWithTransactionsAccount(currentUser);
        }

        /// <summary>
        /// Method that calls fillWithTransactions to fill the data grid with all the transactions
        /// </summary>
        private void SearchAllTransactions(object sender, RoutedEventArgs e)
        {
            fillWithTransactionsAccount(currentUser);
        }

        /// <summary>
        /// Method that calls fillWithTransactions to fill the data grid with all the deposit transactions
        /// </summary>
        private void SearchDepositTransactions(object sender, RoutedEventArgs e)
        {
            fillWithTransactionsType("deposit", currentUser);
        }

        /// <summary>
        /// Method that calls fillWithTransactions to fill the data grid with all the withdrawing transactions
        /// </summary>
        private void SearchWithdrawingTransactions(object sender, RoutedEventArgs e)
        {
            fillWithTransactionsType("withdrawing", currentUser);
        }

        /// <summary>
        /// Method that calls fillWithTransactions to fill the data grid with all the transfer transactions
        /// </summary>
        private void SearchTransferTransactions(object sender, RoutedEventArgs e)
        {
            fillWithTransactionsType("transfer", currentUser);
        }

        /// <summary>
        /// This method fills the data grid with transactions. 
        /// The moment this method is run all the transactions will be searched through to find the transactions that have the right type and user.
        /// </summary>
        /// <param name="type"> The type is used to identify which transactions the admin wants to find </param>
        /// /// <param name="accountNumber"> The accountNumber is used to identify which person the admin wants to find </param>
        public void fillWithTransactionsType(string type, string accountNumber)
        {
            TransactionGrid.Items.Clear();
            Cryptobankservice.CryptoBankServiceSoapClient cssc = new Cryptobankservice.CryptoBankServiceSoapClient();

            Cryptobankservice.TransactionModel[] transactions = cssc.TransactionsPerType(type, accountNumber);

            for (var i = 0; i < transactions.Length; i++)
            {
                var data = new TransactionData
                {
                    date = transactions[i].date,
                    type = transactions[i].transactiontype,
                    sender = transactions[i].sender,
                    receiver = transactions[i].receiver,
                    amount = transactions[i].amount
                };

                TransactionGrid.Items.Add(data);
                data = null;
            }
        }

        /// <summary>
        /// The same as fillWithTransactionsType, the only difference here is that the search is only on the user.
        /// </summary>
        /// <param name="accountNumber"> The accountNumber is used to identify which person the admin wants to find </param>
        public void fillWithTransactionsAccount(string accountNumber)
        {
            TransactionGrid.Items.Clear();
            Cryptobankservice.CryptoBankServiceSoapClient cssc = new Cryptobankservice.CryptoBankServiceSoapClient();

            Cryptobankservice.TransactionModel[] transactions = cssc.AllTransactions(accountNumber);

            for (var i = 0; i < transactions.Length; i++)
            {
                var data = new TransactionData
                {
                    date = transactions[i].date,
                    type = transactions[i].transactiontype,
                    sender = transactions[i].sender,
                    receiver = transactions[i].receiver,
                    amount = transactions[i].amount
                };

                TransactionGrid.Items.Add(data);
                data = null;
            }
        }

        /// <summary>
        /// A way to switch from the Transaction window to the User Information window.
        /// </summary>
        private void SwitchToUserInformation(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }
    }

    /// <summary>
    /// Blue print for the data that needs to be put in the data grid.
    /// </summary>
    public class TransactionData
    {
        public string date { get; set; }
        public string type { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        public double amount { get; set; }
    }
}
