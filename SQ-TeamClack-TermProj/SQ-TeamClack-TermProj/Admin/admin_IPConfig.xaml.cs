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
    /// Interaction logic for admin_IPConfig.xaml
    /// </summary>
    public partial class admin_IPConfig : Window
    {

        public bool Confirmed;
        User localUser;

        /*!
         * \brief CONSTRUCTOR - This constructor constructs the IP Config page.
         * \details This constructor initializes all the properties that are needed in order to use the IP Config page.
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
        */
        public admin_IPConfig(User localUser)
        {
            InitializeComponent();

            this.localUser = localUser;
        }

        /*!
         * \brief 
         * \details 
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (ipbox.Text != "" && portbox.Text != "")
            {
                DialogResult = true;
            }
        }

        public string ResponseTextIP { get { return ipbox.Text; } set { ipbox.Text = value; } }
        public string ResponseTextPort { get { return portbox.Text; } set { portbox.Text = value; } }
    }
}
