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
    /// Interaction logic for planner_MainMenu.xaml
    /// </summary>
    public partial class planner_MainMenu : Page
    {
        private User localUser;
        public planner_MainMenu(User localUser)
        {
            InitializeComponent();
            this.localUser = localUser;

            UsernameLabel.Content = localUser.USERNAME;
        }

        private void plannerBTN_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Transfer to plannerMenu Page
            plannerMenu plannerM = new plannerMenu(localUser);
            theFrame.NavigationService.Navigate(plannerM);
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

        private void logoutBTN_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            localUser.logout();
            loginPage login = new loginPage(localUser);
            this.NavigationService.Navigate(login);
        }
    }
}
