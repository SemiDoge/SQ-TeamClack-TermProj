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
        private bool cityValid = false;
        private bool orderValid = false;
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

            customerName.Content = order.customerName;
            origin.Content = order.origin;
            quantity.Content = order.quantity;
            destination.Content = order.destination;
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
            buyer_ReviewCustomers reviewCustomers = new buyer_ReviewCustomers(localUser);
            this.NavigationService.Navigate(reviewCustomers);
        }

        private void ReviewOrdersBTN_Click(object sender, RoutedEventArgs e)
        {
            // Go to Review Orders page
            buyer_ReviewOrders reviewOrders = new buyer_ReviewOrders(localUser);
            this.NavigationService.Navigate(reviewOrders);
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            //log the user out 
            localUser.logout();

            // Return to login page
            loginPage login = new loginPage(localUser);
            this.NavigationService.Navigate(login);
        }

        private void citySubmitBTN_Click(object sender, RoutedEventArgs e)
        {
            // Connect to database
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT COUNT(*) FROM CarrierCities WHERE CityName=" + cityInput.SelectedItem + ";");
            int mysqlint = 0;


            using (MySqlConnection connection = new MySqlConnection(conStr))
            {

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(cmdSB.ToString(), connection);
                try
                {
                    connection.Open();
                    mysqlint = int.Parse(cmd.ExecuteScalar().ToString());
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

            if (mysqlint == 1 && (string)city.Content == order.origin)
            {
                city.Content = cityInput.Text;
                city.Visibility = Visibility.Visible;
            }
            else if ((string)city.Content == "")
            {
                errorMsg.Content = "ERROR: City field cannot be blank.\n";
            }
            else
            {
                errorMsg.Content = "ERROR: City does not exist.\n";
            }


            cityValid = true;
            orderValid = true;
            cityLabel.Content = cityInput.Text;
            citySubmitBTN.IsEnabled = false;
        }

        private void orderSubmitBTN_Click(object sender, RoutedEventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("INSERT INTO Orders(CustomerName, JobType, Quantity, Origin, Destination, Van_Type, CarrierName) VALUES ('" + order.customerName + "', " + order.jobType + ", " + order.quantity + ", '" + order.origin + "', '" + order.destination + "', " + order.vanType + ",'');");

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

            if (orderValid == true)
            {
                // Generate order number
                // Send order to database
                // Then return to buyer menu
                buyer_InitiateOrder buyerOrderMenu = new buyer_InitiateOrder(localUser);
                this.NavigationService.Navigate(buyerOrderMenu);
            }
            else
            {
                errorLabel.Content += "ERROR: One or more fields for this order are invalid";
            }
        }

        private void cityInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            city.Content = cityInput.SelectedItem;
            city.Visibility = Visibility.Visible;
        }
    }
}
