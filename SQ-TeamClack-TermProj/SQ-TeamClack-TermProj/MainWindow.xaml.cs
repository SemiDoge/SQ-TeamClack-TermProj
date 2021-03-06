﻿/*
	FILE			: MainWindow.xaml.cs
	PROGRAMMER		: Tommy Ngo, Isaiah Andrews, Taimoor Salam, Jaydan Zabar
	FIRST VERSION	: 10/21/2020
    LATEST VERSION  : 12/07/2020
	DESCRIPTION		: This project implements the Transport Management System. This program allows various users to
                      the purchase, schedule, monitor and bill organizations who desire to have cargo shipped via
                      truck or train.
*/

using System;
using System.Windows;


/*! \mainpage T.M.S: The Definitive Edition
 *
 * \section intro_sec Introduction To the T.M.System
 * In the TMSystem there are three types of accounts: the Admin, Buyer, and Planner.
 *
 * \subsection Admin The Admin Account
 * This is the introduction.
 *
 * \subsection Buyer The Buyer Account
 * This is the introduction.
 *
 * \subsection Planner The Planner Account
 * This is the introduction.
 *
 *
 * 
 *
 * \authors Tommy Ngo, Isaiah Andrews, Taimoor Salam, Jaydan Zabar
 */

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