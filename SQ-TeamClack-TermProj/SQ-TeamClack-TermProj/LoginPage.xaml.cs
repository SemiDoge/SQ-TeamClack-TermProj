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

namespace testUI
{
    /// <summary>
    /// Interaction logic for loginPage.xaml
    /// </summary>
    public partial class loginPage : Page
    {
        public user localUser = new user();

        public loginPage()
        {
            InitializeComponent();
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
            }

            // Check if both fields were valid
            if (passwordValid == true && usernameValid == true)
            {
                // assign username to user class
                localUser.setUsername(usernameInput.Text);

                // Go to next page
                buyerMenu buyerM = new buyerMenu();
                this.NavigationService.Navigate(buyerM);
            }
            else
            {
                return;
            }
        }
    }
}
