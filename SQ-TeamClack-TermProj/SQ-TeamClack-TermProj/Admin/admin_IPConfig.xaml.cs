using System.Windows;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for admin_IPConfig.xaml
    /// </summary>
    public partial class admin_IPConfig : Window
    {
        public bool Confirmed;
        private User localUser;

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