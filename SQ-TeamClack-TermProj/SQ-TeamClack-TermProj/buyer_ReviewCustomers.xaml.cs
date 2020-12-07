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

namespace SQ_TeamClack_TermProject
{
    /// <summary>
    /// Interaction logic for buyer_ReviewCustomers.xaml
    /// </summary>
    public partial class buyer_ReviewCustomers : Page
    {
        public User localUser;
        public buyer_ReviewCustomers(User localUser)
        {
            InitializeComponent();

            this.localUser = localUser;
            UsernameLabel.Content = localUser.USERNAME;
        }

        private void InitiateOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            // Go to initiate order page
            buyer_InitiateOrder initiateOrder = new buyer_InitiateOrder(localUser);
            this.NavigationService.Navigate(initiateOrder);
        }

        private void ReviewCustomersBTN_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
