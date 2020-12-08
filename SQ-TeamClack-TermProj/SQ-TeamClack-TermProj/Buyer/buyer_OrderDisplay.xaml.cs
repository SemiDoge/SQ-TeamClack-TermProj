using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for buyer_OrderDisplay.xaml
    /// </summary>
    public partial class buyer_OrderDisplay : Page
    {
        private User localUser;
        private Order order;

        /*!
         * \brief CONSTRUCTOR - This constructor constructs the Order Display page.
         * \details This constructor initializes all the properties that are needed in order to use the Order Display page.
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
         * \param param - <b>contractParams</b> - This parameter structure contains all of the contract information taken from the contracts page.
        */

        public buyer_OrderDisplay(User localUser, contractParams param)
        {
            InitializeComponent();

            this.localUser = localUser;

            fillCityComboBox();

            UsernameLabel.Content = localUser.USERNAME;

            // Disable create order button
            InitiateOrderBTN.IsEnabled = false;

            order = new Order(param);

            customerName.Content = order.CUSTOMERNAME;
            origin.Content = order.ORIGIN;
            quantity.Content = order.QUANTITY;
            destination.Content = order.DESTINATION;
        }

        /*!
         * \brief This method populates the cityInfo Combo Box.
         * \details This methos populates the cityInfo CB by querying the CarrierCities table in the database for all of the unique city names.
         * \param <b>void</b>
        */

        private void fillCityComboBox()
        {
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT distinct(cityName) FROM CarrierCities;");
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
                        cityInput.Items.Add(reader["cityName"].ToString());
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

        private void InitiateOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            //tommy
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
        }

        /*!
         * \brief This handler handles when the user clicks the "Order Submit" button.
         * \details This handler handles the newly created order being entered into the database via the users connection string.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void orderSubmitBTN_Click(object sender, RoutedEventArgs e)
        {
            if ((string)cityInput.SelectedItem == order.ORIGIN)
            {
                string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
                StringBuilder cmdSB = new StringBuilder("INSERT INTO Orders(OrderID, OrderDate, CustomerName, JobType, Quantity, Origin, Destination, Van_Type, MarkedForAction) VALUES (" + order.ORDERID + ", '" + order.getTimeStamp().ToString() + "', '" + order.CUSTOMERNAME + "', " + order.JOBTYPE + ", " + order.QUANTITY + ", '" + order.ORIGIN + "', '" + order.DESTINATION + "', " + order.VANTYPE + ", " + order.MARKEDFORACTION + "); INSERT INTO Customers(CustomerName) VALUES ('" + order.CUSTOMERNAME + "');");

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

                buyer_InitiateOrder buyerOrderMenu = new buyer_InitiateOrder(localUser);
                this.NavigationService.Navigate(buyerOrderMenu);
            }
            else
            {
                errorMsg.Content = "ERROR: Selected city is not proximal to origin.\n";
            }
        }

        /*!
         * \brief This handler handles the logic behind a selection in the Combo Box.
         * \details This handler handles what happens when the user selects a city in the cityInfo Combo Box.
         * \param sender <b>object</b>
         * \param e <b>SelectionChangedEventArgs</b>
        */

        private void cityInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            city.Content = cityInput.SelectedItem;
            city.Visibility = Visibility.Visible;
        }
    }
}