using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace AdminApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool alreadyShown = false;

        public MainWindow()
        {
            InitializeComponent();

            findAllUsers();
        }

        /// <summary>
        /// If the button is clicked the user that is selected in the list will be deleted.
        /// </summary>
        private void DeleteAccount(object sender, RoutedEventArgs e)
        {
            string data = UsersBox.SelectedItem.ToString();
            var usableData = data.Substring(37);
            Cryptobankservice.CryptoBankServiceSoapClient cssc = new Cryptobankservice.CryptoBankServiceSoapClient();
            cssc.DeleteUser(usableData);

            TransactionButton.Visibility = Visibility.Collapsed;
            DeleteButton.Visibility = Visibility.Collapsed;
            statusData.Content = "inactive";

            UsersBox.Items.Remove(UsersBox.SelectedItem);

            alreadyShown = false;
        }

        /// <summary>
        /// This method will be used to find all the users in the database and fill the ListBox with all the users.
        /// </summary>
        public void findAllUsers()
        {
            Cryptobankservice.CryptoBankServiceSoapClient cssc = new Cryptobankservice.CryptoBankServiceSoapClient();
            List<string> users = cssc.AllUsers();
            for (int i = 0; i < users.Count; i++)
            {
                ListBoxItem itm = new ListBoxItem();
                itm.Content = users[i];
                UsersBox.Items.Add(itm);
                itm = null;
            }
        }

        /// <summary>
        /// Every time the user selects an entry in the ListBox, the name of that item will be made into a usable string.
        /// After that it will be used to find the data of the selected user.
        /// </summary>
        private void UsersBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            
            if (alreadyShown == false)
            {
                showUserDataLabels();
                alreadyShown = true;
            }

            if (UsersBox.SelectedItem != null)
            {
                string data = UsersBox.SelectedItem.ToString();
                string usableData = data.Substring(37);

                Console.WriteLine(usableData);

                Cryptobankservice.CryptoBankServiceSoapClient cssc = new Cryptobankservice.CryptoBankServiceSoapClient();
                Cryptobankservice.UserModel[] x = cssc.Finduser(usableData);

                statusData.Content = x[0].status;
                nameData.Content = x[0].name;
                emailData.Content = x[0].email;
                cityData.Content = x[0].city;
                accountnumberData.Content = x[0].accountnumber;
                balanceData.Content = x[0].balance;
            }
        }

        /// <summary>
        /// The description will become invisible and the user data will become visible.
        /// </summary>
        public void showUserDataLabels()
        {
            description.Visibility = Visibility.Collapsed;

            status.Visibility = Visibility.Visible;
            statusData.Visibility = Visibility.Visible;
            name.Visibility = Visibility.Visible;
            nameData.Visibility = Visibility.Visible;
            email.Visibility = Visibility.Visible;
            emailData.Visibility = Visibility.Visible;
            city.Visibility = Visibility.Visible;
            cityData.Visibility = Visibility.Visible;
            accountnumber.Visibility = Visibility.Visible;
            accountnumberData.Visibility = Visibility.Visible;
            balance.Visibility = Visibility.Visible;
            balanceData.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
            TransactionButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// A way to switch from the User Information window to the Transaction window.
        /// </summary>
        private void SwitchToTransactions(object sender, RoutedEventArgs e)
        {
            string data = UsersBox.SelectedItem.ToString();
            string usableData = data.Substring(37);

            Transactions trans = new Transactions(usableData);
            App.Current.MainWindow = trans;
            this.Close();
            trans.Show();
        }
    }
}
