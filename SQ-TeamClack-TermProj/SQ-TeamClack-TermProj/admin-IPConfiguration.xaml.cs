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
using System.Windows.Shapes;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// This functions as the window for configuring IP and Port for database communications.
    /// </summary>
    public partial class admin_IPConfiguration : Window
    {

        public bool Confirmed;

        public admin_IPConfiguration()
        {
            InitializeComponent();
            Confirmed = false;
        }

        /*!
         * \brief This function allows for the user (admin) confirm their new configuration settings for the IP and Port.
         * \details This window will open when the user (admin) selects configuration in the backup screen of the admin menu, allowing for IP/PORT configuration.
         * \param sender - <b>object</b> - 
         * \param e - <b>RoutedEventArgs</b> -
        */
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            SQ_TeamClack_TermProj.MainWindow.selectedIP = ipbox.Text;
            SQ_TeamClack_TermProj.MainWindow.selectedPort = portbox.Text;
            Confirmed = true;
            this.Close();
        }
    }
}
