/*
	FILE			: MainWindow.xaml.cs
	PROGRAMMER		: Tommy Ngo, Isaiah Andrews, Taimoor Salam, Jaydan Zabar
	FIRST VERSION	: 10/21/2020
    LATEST VERSION  : 12/07/2020
	DESCRIPTION		: This project implements the Transport Management System. This program allows various users to
                      the purchase, schedule, monitor and bill organizations who desire to have cargo shipped via
                      truck or train.
*/

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
        public static String logfileLocation;


        /*!
         * \brief CONSTRUCTOR - This constructor constucts the main window.
         * \details This constructor constructs the main window. This window is a fiction as all it does is initiate some values and
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        public MainWindow()
        {
            InitializeComponent();
            loginPage login = new loginPage(localUser);
            mainDisplay.NavigationService.Navigate(login);
            logfileLocation = "...";
        }
    }
}
