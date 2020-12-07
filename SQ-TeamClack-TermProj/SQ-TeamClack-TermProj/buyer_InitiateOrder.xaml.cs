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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace SQ_TeamClack_TermProject
{
    /// <summary>
    /// Interaction logic for buyer_InitiateOrder.xaml
    /// </summary>
    public partial class buyer_InitiateOrder : Page
    {
        private bool databaseConnected = false;
        private bool contractSelected = false;
        private User localUser;
        private Contract selectedContract = new Contract();
        private List<Contract> listItems = new List<Contract>();
        private contractParams p;

        public buyer_InitiateOrder(User localUser)
        {
            InitializeComponent();

            this.localUser = localUser;
            // DataContext = new buyer_InitiateOrder();
        }

        private void ReviewOrdersBTN_Click(object sender, RoutedEventArgs e)
        {
            // Go to Review Orders page
            buyer_ReviewOrders reviewOrders = new buyer_ReviewOrders(localUser);
            this.NavigationService.Navigate(reviewOrders);
        }

        private void connectDataBTN_Click(object sender, RoutedEventArgs e)
        {
            // Connect to database
            string conStr = ConfigurationManager.ConnectionStrings["CntrtMrktplc"].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT Client_Name, Job_Type, Quantity, Origin, Destination, Van_Type FROM Contract;");
            MySqlDataReader reader = null;


            using (MySqlConnection connection = new MySqlConnection(conStr))
            {

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(cmdSB.ToString(), connection);
                try
                {
                    connection.Open();
                    databaseConnected = true; // bool that handles 
                    reader = cmd.ExecuteReader();
                    // Once connected, fill textbox with information
                    while (reader.Read())
                    {
                        databaseView.Items.Add(new contractParams { clientName = reader["Client_Name"].ToString(), jobType = int.Parse(reader["Job_Type"].ToString()), quantity = int.Parse(reader["Quantity"].ToString()), origin = reader["Origin"].ToString(), destination = reader["Destination"].ToString(), vanType = int.Parse(reader["Van_Type"].ToString()) });
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }


            //====================================================
            // FOR TESTING ONLY, REMOVE IN FINAL VERSION
            contractSelected = true;
            //====================================================

            // Once bool is changed, display database in listview box
            if (databaseConnected == true)
            {
                connectDataBTN.IsEnabled = false;
                orderStatusLabel.Content = "Select a Contract";
                // display database
                // Add items from database to Contract list
            }
        }

        private void InitiateOrderBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReviewCustomersBTN_Click(object sender, RoutedEventArgs e)
        {
            // Go to Review Customers page
            buyer_ReviewCustomers reviewCustomers = new buyer_ReviewCustomers(localUser);
            this.NavigationService.Navigate(reviewCustomers);
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            //log the user out 
            localUser.logout();

            // Return to login page
            loginPage login = new loginPage(localUser);
            this.NavigationService.Navigate(login);
        }

        private void createOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            // Validate an order has been selected
            // Then transfer to new page
            if (databaseConnected == false)
            {
                errorLabel.Content = "ERROR: Must connect to database before creating an order.\n";
            }

            if (contractSelected == false)
            {
                errorLabel.Content += "ERROR: Must select a contract before creating an order.\n";
            }

            if (contractSelected == true && databaseConnected == true)
            {
                // Assign items from contracts into variables
                contractParams p = (contractParams)databaseView.SelectedItem;

                // Go to next page
                buyer_OrderDisplay order = new buyer_OrderDisplay(localUser, p);
                this.NavigationService.Navigate(order);
            }
            else
            {
                return;
            }
        }

        private void databaseView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            contractParams s = (contractParams)databaseView.SelectedItem;

            displaySelectedContract.Content = s.clientName + "'s Contract";

            contractSelected = true;
        }


    }
}
