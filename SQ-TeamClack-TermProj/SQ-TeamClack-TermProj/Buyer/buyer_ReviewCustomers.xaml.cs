using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for buyer_ReviewCustomers.xaml
    /// </summary>
    public partial class buyer_ReviewCustomers : Page
    {
        public User localUser;

        /*!
         * \brief CONSTRUCTOR - This constructor constructs the Review Customers page.
         * \details This constructor initializes all the properties that are needed in order to use the Review Customers page.
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
        */

        public buyer_ReviewCustomers(User localUser)
        {
            InitializeComponent();
            File.AppendAllText(@"Log\Log.log", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Buyer loaded review customers.\n");

            this.localUser = localUser;
            Loaded += MyWindow_Loaded;
        }

        /*!
         * \brief This handler handles on load functionality.
         * \details This handler calls the queryCustomers method on load.
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
        */

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            queryCustomers();
        }

        /*!
         * \brief This handler handles when the user clicks the "Initiate Order" button.
         * \details This handler connects to the "Initiate Order" orders page.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void InitiateOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            // Go to initiate order page
            buyer_InitiateOrder initiateOrder = new buyer_InitiateOrder(localUser);
            this.NavigationService.Navigate(initiateOrder);
        }

        /*!
         * \brief DISABLED - This handler handles when the user clicks the "Review Customer" button.
         * \details This handler is superfluous as it would take the user back to the page they are currently on.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void ReviewCustomersBTN_Click(object sender, RoutedEventArgs e)
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
            buyer_ReviewOrders reviewOrders = new buyer_ReviewOrders(localUser);
            this.NavigationService.Navigate(reviewOrders);
        }

        /*!
         * \brief This handler is an event handler for the logout button.
         * \details This handler is to allow the user to navigate back to the login page and log out. This handler needs to also make sure that the user means to leave the order creation process, in progress.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            //log the user out
            localUser.logout();

            // Return to login page
            loginPage login = new loginPage(localUser);
            this.NavigationService.Navigate(login);
        }

        /*!
         * \brief This handler is an event handler for the "Add Customer" button.
         * \details This handler queries the
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void addCustomerBTN_Click(object sender, RoutedEventArgs e)
        {
            // Open add customer sub menu
            var dialog = new addCustomerWindow();
            string cusName = "";
            if (dialog.ShowDialog() == true)
            {
                // Move input into a temp string
                cusName = dialog.ResponseText;
            }

            // Add customer to list view
            customerList.Items.Add(cusName);

            // Add customer to database
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("INSERT INTO Customers(CustomerName) VALUES ('" + cusName + "');");

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(cmdSB.ToString(), connection);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
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

            //when a new customer is added, delete the on load customer query returns and replace it with a fresh query
            customerList.DataContext = null;
            customerList.Items.Clear();
            queryCustomers();
        }

        /*!
         * \brief This method populates the customerList List View.
         * \details This method queries the unique Customers table of the Omnicorp database, and adds them to the customerList List View.
         * \param <b>void</b>
        */

        private void queryCustomers()
        {
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT DISTINCT CustomerName FROM Customers;");
            MySqlDataReader reader = null;

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(cmdSB.ToString(), connection);
                try
                {
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        customerList.Items.Add(new contractParams { clientName = reader["CustomerName"].ToString() });
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
        }
    }
}