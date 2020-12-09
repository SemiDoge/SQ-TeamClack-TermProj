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
    /// Interaction logic for admin_MainMenu.xaml
    /// </summary>
    public partial class admin_MainMenu : Page
    {
        private User localUser;
        public admin_MainMenu(User localUser)
        {
            InitializeComponent();
            this.localUser = localUser;
            UsernameLabel.Content = localUser.USERNAME;
        }

        private void Admin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Transfer to Admin Menu
            adminMenu p1 = new adminMenu(localUser);
            theFrame.NavigationService.Navigate(p1);
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

        /*!
         * \brief This handler is an event handler for the logout button.
         * \details This handler is to allow the user to navigate back to the login page and log out. This handler needs to also make sure that the user means to leave the order creation process, in progress.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void logoutBTN_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            localUser.logout();
            loginPage login = new loginPage(localUser);
            this.NavigationService.Navigate(login);
        }
    }
}
