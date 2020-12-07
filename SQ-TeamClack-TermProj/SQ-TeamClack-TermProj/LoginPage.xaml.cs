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
using MySql.Data.MySqlClient;
using System.Configuration;

namespace SQ_TeamClack_TermProject
{
    /// <summary>
    /// Interaction logic for loginPage.xaml
    /// </summary>
    public partial class loginPage : Page
    {
        private User localUser;

        public loginPage(User localUser)
        {
            InitializeComponent();

            this.localUser = localUser;
        }

        private void SubmitBTN_Click(object sender, RoutedEventArgs e)
        {
            bool usernameValid = false;
            bool passwordValid = false;

            // Reset error message
            errorLabel.Content = "";

            // Check if username field is blank
            if (usernameInput.Text == "")
            {
                // Display error message
                errorLabel.Content = "ERROR: Username field cannot be blank.\n";
            }
            else
            {
                usernameValid = true;
            }

            // Check if password field is blank
            if (passwordInput.Text == "")
            {
                // Display error message
                errorLabel.Content += "ERROR: Password field cannot be blank.\n";
            }
            else
            {
                passwordValid = true;
                localUser = new User(usernameInput.Text, passwordInput.Text);
            }

            // Check if both fields were valid
            if (passwordValid == true && usernameValid == true && verifyCredentials() == true)
            {
                localUser.CONSTR = getConStr();

                switch (localUser.CONSTR)
                {
                    case "AdminAcc":
                        // Go to buyer page

                        break;
                    case "PlannerAcc":
                        // Go to buyer page

                        break;
                    case "BuyerAcc":
                        // Go to buyer page
                        buyerMenu buyerM = new buyerMenu(localUser);
                        this.NavigationService.Navigate(buyerM);
                        break;
                }

            }
            else
            {
                errorLabel.Content += "ERROR: Invalid credentials.\n";
            }
        }

        /*!
         * \brief This function verfies the login details.
         * \details This function queries the database to verfiy the login details of the user that has signed in.
         * \param <b>void</b>
        */
        public bool verifyCredentials()
        {
            bool success = false;
            int queryRetCnt = 0;
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT COUNT(*) FROM Users WHERE UserName='" + localUser.USERNAME + "' AND Password='" + localUser.PASSWORD + "';");

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(cmdSB.ToString(), connection);
                try
                {
                    connection.Open();
                    queryRetCnt = int.Parse(cmd.ExecuteScalar().ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            if (queryRetCnt == 1)
            {
                success = true;
            }
            else
            {
                success = false;
            }

            return success;
        }

        /*!
         * \brief This function assigns the connection string that is associated with the user.
         * \details This function queries the database and using the role associated with each user determines the connection string that has their appropiate premissions.
         * \param <b>void</b>
        */
        private string getConStr()
        {
            int role = 0;
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT Role FROM Users WHERE UserName='" + localUser.USERNAME + "' AND Password='" + localUser.PASSWORD + "';");

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(cmdSB.ToString(), connection);
                try
                {
                    connection.Open();
                    role = int.Parse(cmd.ExecuteScalar().ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }

            switch (role)
            {
                case 0: //Admin 
                    return "AdminAcc";
                case 1: //Buyer
                    return "BuyerAcc";
                case 2: //Planner
                    return "PlannerAcc";
                default:
                    return "loginAcc";
            }
        }
    }
}
