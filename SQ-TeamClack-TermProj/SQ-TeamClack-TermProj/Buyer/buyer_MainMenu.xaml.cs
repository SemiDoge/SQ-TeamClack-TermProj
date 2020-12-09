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

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for buyer_MainMenu.xaml
    /// </summary>
    public partial class buyer_MainMenu : Page
    {
        private User localUser;
        public buyer_MainMenu(User localUser)
        {
            InitializeComponent();

            // Load username
            this.localUser = localUser;
            UsernameLabel.Content = localUser.USERNAME;
        }

        /*!
         * \brief This handler handles when the user clicks the "Create Order" button.
         * \details This handler connects to the "Create Order" orders page.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void createOrderBTN_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Go to initiate order page
            buyer_InitiateOrder initiateOrder = new buyer_InitiateOrder(localUser);
            theFrame.NavigationService.Navigate(initiateOrder);

            createOrderBTN.IsEnabled = false;
            reviewCustomersBTN.IsEnabled = true;
            reviewOrdersBTN.IsEnabled = true;

        }

        /*!
         * \brief This handler handles when the user clicks the "Review Customers" button.
         * \details This handler makes to change the page to the review customers page. This handler needs to also make sure that the user means to leave the order creation process, in progress.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void reviewCustomersBTN_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            createOrderBTN.IsEnabled = true;
            reviewCustomersBTN.IsEnabled = false;
            reviewOrdersBTN.IsEnabled = true;

            // Go to Review Customers page
            if (localUser.getOrderProgress() == true)
            {
                MessageBoxResult result = MessageBox.Show("You have a order in progress. Are you sure you want to exit?", "T.M.S. Application", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    buyer_ReviewCustomers reviewCustomers = new buyer_ReviewCustomers(localUser);
                    theFrame.NavigationService.Navigate(reviewCustomers);
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
                theFrame.NavigationService.Navigate(reviewCustomers);
            }
        }

        /*!
         * \brief This handler handles when the user clicks the "Review Orders" button.
         * \details This handler connects to the review orders page. This handler needs to also make sure that the user means to leave the order creation process, in progress.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void reviewOrdersBTN_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            createOrderBTN.IsEnabled = true;
            reviewCustomersBTN.IsEnabled = true;
            reviewOrdersBTN.IsEnabled = false;

            // Go to Review Orders page
            if (localUser.getOrderProgress() == true)
            {
                MessageBoxResult result = MessageBox.Show("You have a order in progress. Are you sure you want to exit?", "T.M.S. Application", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    buyer_ReviewOrders reviewOrders = new buyer_ReviewOrders(localUser);
                    theFrame.NavigationService.Navigate(reviewOrders);
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
                theFrame.NavigationService.Navigate(reviewOrders);
            }
        }

        /*!
         * \brief This handler is an event handler for the logout button.
         * \details This handler is to allow the user to navigate back to the login page and log out. This handler needs to also make sure that the user means to leave the order creation process, in progress.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void logoutBTN_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Return to login page
            if (localUser.getOrderProgress() == true)
            {
                MessageBoxResult result = MessageBox.Show("You have a order in progress. Are you sure you want to exit?", "T.M.S. Application", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    localUser.logout();
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
                localUser.logout();
                loginPage login = new loginPage(localUser);
                this.NavigationService.Navigate(login);
            }
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
