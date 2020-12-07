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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string usernameMain = "";
        private User localUser = new User("login", "");
        public static String selectedIP;
        public static String selectedPort;
        public static String logfileLocation;

        public MainWindow()
        {
            InitializeComponent();
            loginPage login = new loginPage(localUser);
            mainDisplay.NavigationService.Navigate(login);
            selectedIP = "000.000.0.000";
            selectedPort = "0000";
            logfileLocation = "...";
        }
    }
}
