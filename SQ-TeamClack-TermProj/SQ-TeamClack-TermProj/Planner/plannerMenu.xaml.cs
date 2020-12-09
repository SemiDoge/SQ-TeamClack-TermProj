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
    /// Interaction logic for plannerMenu.xaml
    /// </summary>
    public partial class plannerMenu : Page
    {
        private User localUser;

        /*!
         * \brief CONSTRUCTOR - This constructor constructs the Planner Menu page.
         * \details This constructor initializes all the properties that are needed in order to use the Planner Menu page.
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
        */
        public plannerMenu(User localUser)
        {
            InitializeComponent();

            this.localUser = localUser;
        }

        /*!
         * \brief This handler handles when the user clicks the "Complete Order" button.
         * \details This handler brings up the admin backup button page.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void completeOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            // Transfer to completeOrder Page
            planner_CompleteOrder orderPage = new planner_CompleteOrder(localUser);
            PlannerFrame.NavigationService.Navigate(orderPage);
        }

        /*!
         * \brief This handler handles when the user clicks the "Order Status" button.
         * \details This handler brings up the order status button page.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void orderStatusBTN_Click(object sender, RoutedEventArgs e)
        {
            // Transfer to orderStatus Page
            planner_OrderStatus statusPage = new planner_OrderStatus(localUser);
            PlannerFrame.NavigationService.Navigate(statusPage);
        }
    }
}
