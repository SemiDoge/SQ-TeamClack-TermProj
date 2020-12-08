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
    /// Interaction logic for buyerMenu.xaml
    /// </summary>
    public partial class buyerMenu : Page
    {
        private User localUser;

        /*!
         * \brief CONSTRUCTOR - This constructor constructs the Buyer Menu page.
         * \details This constructor constructs the buyer menu's page and sets the page's localUser to the localUser passed in as a parameter.
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
        */
        public buyerMenu(User localUser)
        {
            InitializeComponent();

            this.localUser = localUser;
            UsernameLabel.Content = localUser.USERNAME;
        }

        /*!
         * \brief This handler handles when the user clicks the "Initiate Order" button.
         * \details This handler is superfluous as it would take the user back to the page they are currently on. 
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
         * \brief This handler handles when the user clicks the "Review Customers" button.
         * \details This handler makes to change the page to the review customers page. This handler needs to also make sure that the user means to leave the order creation process, in progress.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void ReviewCustomersBTN_Click(object sender, RoutedEventArgs e)
        {
            // Go to Review Customers page
            buyer_ReviewCustomers reviewCustomers = new buyer_ReviewCustomers(localUser);
            this.NavigationService.Navigate(reviewCustomers);
        }

        /*!
         * \brief This handler handles when the user clicks the "Review Orders" button.
         * \details This handler calls the queryOpenOrders method.
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
            localUser.logout();

            // Return to login page
            loginPage login = new loginPage(localUser);
            this.NavigationService.Navigate(login);
        }
    }
}
