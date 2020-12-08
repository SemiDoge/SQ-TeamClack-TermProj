using System.Windows;
using System.Windows.Controls;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for buyerMenu.xaml
    /// </summary>
    public partial class buyerMenu : Page
    {
        private User localUser;

        public buyerMenu(User localUser)
        {
            InitializeComponent();
            UsernameLabel.Content = localUser.USERNAME;
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Return to login page
            loginPage login = new loginPage(localUser);
            this.NavigationService.Navigate(login);
        }

        private void InitiateOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            // Go to initiate order page
            buyer_InitiateOrder initiateOrder = new buyer_InitiateOrder(localUser);
            this.NavigationService.Navigate(initiateOrder);
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
    }
}