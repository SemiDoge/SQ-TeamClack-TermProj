using System.Windows;
using System.Windows.Controls;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for adminMenu.xaml
    /// </summary>
    public partial class adminMenu : Page
    {
        private User localUser;

        /*!
         * \brief CONSTRUCTOR - This constructor constructs the IP Config page.
         * \details This constructor initializes all the properties that are needed in order to use the IP Config page.
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
        */

        public adminMenu(User localUser)
        {
            InitializeComponent();

            this.localUser = localUser;
        }

        /*!
         * \brief This handler changes the to the
         * \details
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void BackupButton_Click(object sender, RoutedEventArgs e)
        {
            admin_Backup p1 = new admin_Backup(localUser);
            AdminFrame.NavigationService.Navigate(p1);
        }

        /*!
         * \brief
         * \details
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void CarrierButton_Click(object sender, RoutedEventArgs e)
        {
            admin_CarrierConfig p1 = new admin_CarrierConfig(localUser);
            AdminFrame.NavigationService.Navigate(p1);
        }

        /*!
         * \brief
         * \details
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            admin_TableConfig p1 = new admin_TableConfig(localUser);
            AdminFrame.NavigationService.Navigate(p1);
        }
    }
}