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
         * \brief CONSTRUCTOR - This constructor constructs the Admin Menu page.
         * \details This constructor initializes all the properties that are needed in order to use the Admin Menu page.
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
        */

        public adminMenu(User localUser)
        {
            InitializeComponent();

            this.localUser = localUser;
        }

        /*!
         * \brief This handler handles when the user clicks the "Backup" button.
         * \details This handler brings up the admin backup button page and sets the page's localUser to the localUser passed in as a parameter.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void BackupButton_Click(object sender, RoutedEventArgs e)
        {
            admin_Backup adminBackup = new admin_Backup(localUser);
            AdminFrame.NavigationService.Navigate(adminBackup);
        }

        /*!
         * \brief This handler handles when the user clicks the "Carrier" button.
         * \details This handler brings up the carrier page and sets the page's localUser to the localUser passed in as a parameter. 
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void CarrierButton_Click(object sender, RoutedEventArgs e)
        {
            admin_CarrierConfig carrierConfig = new admin_CarrierConfig(localUser);
            AdminFrame.NavigationService.Navigate(carrierConfig);
        }

        /*!
         * \brief This handler handles when the user clicks the "Table Config" button.
         * \details This handler brings up the route table page and sets the page's localUser to the localUser passed in as a parameter.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            admin_TableConfig adminTableConfig = new admin_TableConfig(localUser);
            AdminFrame.NavigationService.Navigate(adminTableConfig);
        }
    }
}