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
    /// This functions as the admin menu which can be accessed by the user (admin).
    /// </summary>
    public partial class adminMenu : Page
    {


        public adminMenu()
        {
            InitializeComponent();
        }

        /*!
         * \brief This function allows for the navigation to the backup section of the admin menu.
         * \details The function is used in the "backup" menu button to navigate the user from the default page to the backup page.
         * \param sender - <b>object</b> - 
         * \param e - <b>RoutedEventArgs</b> -
        */
        private void BackupButton_Click(object sender, RoutedEventArgs e)
        {
            SQ_TeamClack_TermProj.admin_Backup p1 = new SQ_TeamClack_TermProj.admin_Backup();
            AdminFrame.NavigationService.Navigate(p1);
        }

        /*!
         * \brief This function allows for the navigation to the carrier config section of the admin menu.
         * \details The function is used in the "carrier config" menu button to navigate the user from the default page to the carrier config page.
         * \param sender - <b>object</b> - 
         * \param e - <b>RoutedEventArgs</b> -
        */
        private void CarrierButton_Click(object sender, RoutedEventArgs e)
        {
            SQ_TeamClack_TermProj.admin_CarrierConfig p1 = new SQ_TeamClack_TermProj.admin_CarrierConfig();
            AdminFrame.NavigationService.Navigate(p1);
        }

        /*!
         * \brief This function allows for the navigation to the table config section of the admin menu.
         * \details The function is used in the "table config" menu button to navigate the user from the default page to the table config page.
         * \param sender - <b>object</b> - 
         * \param e - <b>RoutedEventArgs</b> -
        */
        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            SQ_TeamClack_TermProj.admin_TableConfig p1 = new SQ_TeamClack_TermProj.admin_TableConfig();
            AdminFrame.NavigationService.Navigate(p1);
        }
    }
}
