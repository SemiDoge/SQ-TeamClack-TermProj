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

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for buyer_OrderDisplay.xaml
    /// </summary>
    public partial class buyer_OrderDisplay : Page
    {
        private User localUser;
        private Order order;
        

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

        }

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

        private void cityInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            city.Content = cityInput.SelectedItem;
            city.Visibility = Visibility.Visible;
        }
    }
}
