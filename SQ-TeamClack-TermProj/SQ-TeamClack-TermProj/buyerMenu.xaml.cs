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
    /// Interaction logic for buyerMenu.xaml
    /// </summary>
    public partial class buyerMenu : Page
    {
        user localUser = new user();
        public buyerMenu()
        {
            InitializeComponent();
            UsernameLabel.Content = localUser.getUsername();

        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Return to login page
            loginPage login = new loginPage();
            this.NavigationService.Navigate(login);

        }

        private void InitiateOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            // Go to initiate order page
            buyer_InitiateOrder initiateOrder = new buyer_InitiateOrder();
            this.NavigationService.Navigate(initiateOrder);
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
