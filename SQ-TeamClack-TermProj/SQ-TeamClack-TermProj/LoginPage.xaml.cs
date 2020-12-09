using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for loginPage.xaml
    /// </summary>
    public partial class loginPage : Page
    {
        private User localUser;

        /*!
         * \brief CONSTRUCTOR -  This constructor constructs the login page.
         * \details This constructor constructs the login page and sets the page's localUser to the localUser passed in as a parameter.
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
        */

        public loginPage(User localUser)
        {
            InitializeComponent();

            this.localUser = localUser;
        }

        /*!
         * \brief This handler handles the button that submits the user credentials.
         * \details This handler takes the user entered credentials and submits it for verification.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

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
                        // Go to admin page
                        File.AppendAllText(@"Log\Log.txt", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Admin logged in.\n");
                        adminMenu adminM = new adminMenu(localUser);
                        this.NavigationService.Navigate(adminM);
                        break;

                    case "PlannerAcc":
                        // Go to planner page
                        File.AppendAllText(@"Log\Log.txt", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Planner logged in.\n");
                        plannerMenu plannerM = new plannerMenu(localUser);
                        this.NavigationService.Navigate(plannerM);
                        break;

                    case "BuyerAcc":
                        File.AppendAllText(@"Log\Log.txt", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Buyer logged in.\n");
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