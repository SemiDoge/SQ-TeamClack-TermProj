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

namespace testUI
{
    /// <summary>
    /// Interaction logic for buyer_InitiateOrder.xaml
    /// </summary>
    ///
    public partial class buyer_InitiateOrder : Page
    {
        bool databaseConnected = false;
        bool contractSelected = false;
        public user localUser = new user();
        public Contract selectedContract = new Contract();
        List<Contract> listItems = new List<Contract>();

        public buyer_InitiateOrder()
        {
            InitializeComponent();
            UsernameLabel.Content = localUser.getUsername();

            // Disable create order button
            InitiateOrderBTN.IsEnabled = false;
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Return to login page
            loginPage login = new loginPage();
            this.NavigationService.Navigate(login);

        }

        private void connectDataBTN_Click(object sender, RoutedEventArgs e)
        {
            // Connect to database

            // Once connected, fill textbox with information

            // Change bool to true once connected
            databaseConnected = true;

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

        private void createOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            // Validate an order has been selected
            // Then transfer to new page
            if(databaseConnected ==  false)
            {
                errorLabel.Content = "ERROR: Must connect to database before creating an order.\n";
            }
            
            if(contractSelected == false)
            {
                errorLabel.Content += "ERROR: Must select a contract before creating an order.\n";
            }

            if(contractSelected == true && databaseConnected == true)
            {
                // Assign items from contracts into variables
                // Go to next page
                buyer_OrderDisplay order = new buyer_OrderDisplay();
                this.NavigationService.Navigate(order);
            }
            else
            {
                return;
            }
        }

        private void InitiateOrderBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReviewCustomersBTN_Click(object sender, RoutedEventArgs e)
        {
            // Go to Review Customers page
            buyer_ReviewCustomers reviewCustomers = new buyer_ReviewCustomers();
            this.NavigationService.Navigate(reviewCustomers);
        }

        private void ReviewOrdersBTN_Click(object sender, RoutedEventArgs e)
        {
            // Go to Review Orders page
            buyer_ReviewOrders reviewOrders = new buyer_ReviewOrders();
            this.NavigationService.Navigate(reviewOrders);
        }
    }


}
