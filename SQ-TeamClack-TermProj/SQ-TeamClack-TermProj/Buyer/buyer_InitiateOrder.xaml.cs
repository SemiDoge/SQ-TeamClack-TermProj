using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for buyer_InitiateOrder.xaml
    /// </summary>
    public partial class buyer_InitiateOrder : Page
    {
        private bool databaseConnected = false;
        private bool contractSelected = false;
        private User localUser;
        private contractParams p;
        //private Contract selectedContract = new Contract();
        //private List<Contract> listItems = new List<Contract>();

        /*!
         * \brief CONSTRUCTOR - This constructor constructs the Initiate Order page.
         * \details This constructor initializes all the properties that are needed in order to use the Initiate order page.
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
        */

        public buyer_InitiateOrder(User localUser)
        {
            File.AppendAllText(@"Log\Log.txt", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Buyer loaded initiate order.\n");

            InitializeComponent();

            this.localUser = localUser;
            // DataContext = new buyer_InitiateOrder();
            InitiateOrderBTN.IsEnabled = false;
        }

        /*!
         * \brief This handler handles when the user clicks the "Connect Data" button.
         * \details This handler connects to the contract marketplace via the "CntrtMrktplc" connection string, and queries all of the available contracts.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

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
                    File.AppendAllText(@"Log\Log.txt", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Buyer connected to the contract marketplace database.\n");
                    databaseConnected = true; // bool that handles
                    localUser.setOrderProgress(true);
                    reader = cmd.ExecuteReader();
                    // Once connected, fill textbox with information
                    while (reader.Read())
                    {
                        databaseView.Items.Add(new contractParams
                        {
                            clientName = reader["Client_Name"].ToString(),
                            jobType = int.Parse(reader["Job_Type"].ToString()),
                            quantity = int.Parse(reader["Quantity"].ToString()),
                            origin = reader["Origin"].ToString(),
                            destination = reader["Destination"].ToString(),
                            vanType = int.Parse(reader["Van_Type"].ToString())
                        });
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

            // Once bool is changed, display database in listview box
            //tommy
            if (databaseConnected == true)
            {
                connectDataBTN.IsEnabled = false;
                orderStatusLabel.Content = "Select a Contract";
                // display database
                // Add items from database to Contract list
            }
        }

        /*!
         * \brief DISABLED - This handler handles when the user clicks the "Review Customer" button.
         * \details This handler is superfluous as it would take the user back to the page they are currently on.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void InitiateOrderBTN_Click(object sender, RoutedEventArgs e)
        {
        }

        /*!
         * \brief This handler handles when the user clicks the "Review Orders" button.
         * \details This handler connects to the review orders page. This handler needs to also make sure that the user means to leave the order creation process, in progress.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void ReviewOrdersBTN_Click(object sender, RoutedEventArgs e)
        {
            // Go to Review Orders page
            if (localUser.getOrderProgress() == true)
            {
                MessageBoxResult result = MessageBox.Show("You have a order in progress. Are you sure you want to exit?", "T.M.S. Application", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    buyer_ReviewOrders reviewOrders = new buyer_ReviewOrders(localUser);
                    this.NavigationService.Navigate(reviewOrders);
                    localUser.setOrderProgress(false);
                }
                else
                {
                    return;
                }
            }
            else
            {
                buyer_ReviewOrders reviewOrders = new buyer_ReviewOrders(localUser);
                this.NavigationService.Navigate(reviewOrders);
            }
        }

        /*!
         * \brief This handler handles when the user clicks the "Review Customers" button.
         * \details This handler makes to change the page to the review customers page. This handler needs to also make sure that the user means to leave the order creation process, in progress.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void ReviewCustomersBTN_Click(object sender, RoutedEventArgs e)
        {
            // Go to Review Customers page
            if (localUser.getOrderProgress() == true)
            {
                MessageBoxResult result = MessageBox.Show("You have a order in progress. Are you sure you want to exit?", "T.M.S. Application", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    buyer_ReviewCustomers reviewCustomers = new buyer_ReviewCustomers(localUser);
                    this.NavigationService.Navigate(reviewCustomers);
                    localUser.setOrderProgress(false);
                }
                else
                {
                    return;
                }
            }
            else
            {
                buyer_ReviewCustomers reviewCustomers = new buyer_ReviewCustomers(localUser);
                this.NavigationService.Navigate(reviewCustomers);
            }
        }

        /*!
         * \brief This handler is an event handler for the logout button.
         * \details This handler is to allow the user to navigate back to the login page and log out. This handler needs to also make sure that the user means to leave the order creation process, in progress.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Return to login page
            if (localUser.getOrderProgress() == true)
            {
                MessageBoxResult result = MessageBox.Show("You have a order in progress. Are you sure you want to exit?", "T.M.S. Application", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    loginPage login = new loginPage(localUser);
                    this.NavigationService.Navigate(login);
                    localUser.setOrderProgress(false);
                }
                else
                {
                    return;
                }
            }
            else
            {
                File.AppendAllText(@"Log\Log.txt", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Buyer logged out.\n");
                localUser.logout();
                loginPage login = new loginPage(localUser);
                this.NavigationService.Navigate(login);
            }
        }

        /*!
         * \brief This handler handles when the user clicks the "Create Order" button.
         * \details This handler verifies that the database was connected to successfully, that a contract was selected, and if both are true it assigns the details of the contract to a contract struct and moves on the "Order Display" page.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

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

        /*!
         * \brief This handler handles the logic behind a selection in the List View.
         * \details This handler handles what happens when the user selects an contract entry in the List View.
         * \param sender <b>object</b>
         * \param e <b>SelectionChangedEventArgs</b>
        */

        private void databaseView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            contractParams s = (contractParams)databaseView.SelectedItem;

            displaySelectedContract.Content = s.clientName + "'s Contract";

            contractSelected = true;
        }
    }
}