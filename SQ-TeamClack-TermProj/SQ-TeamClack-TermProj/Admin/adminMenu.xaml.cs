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
         * \details This constructor initializes all the properties that are needed in order to use the IP Config page.
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
        */

        public adminMenu(User localUser)
        {
            InitializeComponent();

            this.localUser = localUser;
        }

        /*!
<<<<<<< Updated upstream
<<<<<<< HEAD
         * \brief This handler handles when the user clicks the "Backup" button.
         * \details This handler brings up the admin backup button page.
=======
         * \brief This handler changes the to the
         * \details
>>>>>>> 994432fe3048c1ee1c55a3f8b1ead4e295007b6e
=======
         * \brief This handler handles when the user clicks the "Backup" button.
         * \details This handler brings up the admin backup button page.
>>>>>>> Stashed changes
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void BackupButton_Click(object sender, RoutedEventArgs e)
        {
            admin_Backup p1 = new admin_Backup(localUser);
            AdminFrame.NavigationService.Navigate(p1);
        }

        /*!
<<<<<<< Updated upstream
<<<<<<< HEAD
         * \brief This handler handles when the user clicks the "Carrier" button.
         * \details This handler brings up the admin backup button page. 
=======
         * \brief
         * \details
>>>>>>> 994432fe3048c1ee1c55a3f8b1ead4e295007b6e
=======
         * \brief This handler handles when the user clicks the "Carrier" button.
         * \details This handler brings up the admin backup button page. 
>>>>>>> Stashed changes
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