﻿using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for buyer_ReviewOrders.xaml
    /// </summary>
    public partial class buyer_ReviewOrders : Page
    {
        private User localUser;
        private contractParams p;

        /*!
         * \brief CONSTRUCTOR - This constructor constructs the Review Order page.
         * \details This constructor constructs the review orders page and sets the page's localUser to the localUser passed in as a parameter.
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
        */

        public buyer_ReviewOrders(User localUser)
        {
            File.AppendAllText(@"Log\Log.log", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Buyer loaded review order page.\n");
            InitializeComponent();

            this.localUser = localUser;
            Loaded += MyWindow_Loaded;
        }

        /*!
         * \brief This handler handles to on load functionality.
         * \details This handler is calls the queryOpenOrders method on page load.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            queryOpenOrders();
        }

        /*!
         * \brief This handler handles when the user clicks the "Initiate Order" button.
         * \details This handler is superfluous as it would take the user back to the page they are currently on.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void InitiateOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            // Go to initiate order page
            buyer_InitiateOrder initiateOrder = new buyer_InitiateOrder(localUser);
            this.NavigationService.Navigate(initiateOrder);
        }

        /*!
         * \brief This handler handles when the user clicks the "Review Customers" button.
         * \details This handler changes the page to the review customers page. This handler needs to also make sure that the user means to leave the order creation process, in progress.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void ReviewCustomersBTN_Click(object sender, RoutedEventArgs e)
        {
            // Go to Review Customers page
            buyer_ReviewCustomers reviewCustomers = new buyer_ReviewCustomers(localUser);
            this.NavigationService.Navigate(reviewCustomers);
        }

        /*!
         * \brief DISABLED - This handler handles when the user clicks the "Review Orders" button.
         * \details This handler calls the queryOpenOrders method.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void ReviewOrdersBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        /*!
         * \brief This handler is an event handler for the logout button.
         * \details This handler is to allow the user to navigate back to the login page and log out. This handler needs to also make sure that the user means to leave the order creation process, in progress.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            //log the user out
            localUser.logout();

            // Return to login page
            loginPage login = new loginPage(localUser);
            this.NavigationService.Navigate(login);
        }

        /*!
         * \brief This method fills a List View with all of the active orders.
         * \details This method queries the Orders table from the Omnicorp database via the localuser connection string.
         * \param <b>void</b>
        */
        private void queryOpenOrders()
        {
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT OrderID, CustomerName, Origin, Destination, MarkedForAction, Quantity, JobType, OrderDate FROM Orders;");
            MySqlDataReader reader = null;

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(cmdSB.ToString(), connection);
                try
                {
                    connection.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        orderList.Items.Add(new contractParams
                        {
                            orderID = ulong.Parse(reader["OrderID"].ToString()),
                            clientName = reader["CustomerName"].ToString(),
                            orderDate = reader["OrderDate"].ToString(),
                            origin = reader["Origin"].ToString(),
                            destination = reader["Destination"].ToString(),
                            quantity = int.Parse(reader["Quantity"].ToString()),
                            jobType = int.Parse(reader["JobType"].ToString()),
                            markedForAction = bool.Parse(reader["MarkedForAction"].ToString())
                        });
                    }
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
        }

        /*!
         * \brief This handler handles the logic behind a selection in the List View.
         * \details This handler handles what happens when the user selects an contract entry in the List View.
         * \param sender <b>object</b>
         * \param e <b>SelectionChangedEventArgs</b>
         */
        private void orderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            p = (contractParams)orderList.SelectedItem;
            invoiceOrderBTN.IsEnabled = true;
        }


        /*!
         * \brief This handler handles the logic behind clicking the Invoice Order button.
         * \details This handler handles the creation of the invoice txt file.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void invoiceOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            //Open up a save file dialog so that the user can choose where they want the invoice saved
            SaveFileDialog invoiceFile = new SaveFileDialog();
            invoiceFile.Filter = "Text file(*.txt)|*.txt|All Files (*.*)|*.*";
            invoiceFile.Title = "Save an Invoice (.txt)";

            var temp = simulateTravelRoute(p.origin, p.destination, p.quantity, p.jobType);

            //Building the invoice
            sb.Append("========================================================================\n");
            sb.Append("Order Number: " + p.orderID + "\n");
            sb.Append("Customer: " + p.clientName + "\n");
            sb.Append("Origin: " + p.origin + "\n");
            sb.Append("Destination: " + p.destination + "\n");
            sb.Append("Total Travel Time: " + temp.travelTime.ToString() + "\n");
            sb.Append("Total Intermediary Time: " + temp.travelIntTime.ToString() + "\n");
            sb.Append("Total Travel Distance: " + temp.travelDist.ToString() + "\n");
            sb.Append("Total Surcharge Cost: " + temp.surchargeCost + "\n");
            sb.Append("Total Rate Cost: " + temp.totalRateCost.ToString() + "\n");
            sb.Append("Total Final Cost: " + temp.totalFinalCost.ToString() + "\n");
            sb.Append("========================================================================");

            //writing the invoice to the file
            if (invoiceFile.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(invoiceFile.FileName))
                {
                    sw.WriteLine(sb.ToString());
                }
            }
        }

        /*!
         * \brief This method simulates the route that the trucks would need to take in order to reach their destinations.
         * \details This method calculates all of the information that is needed to create a accurate invoice of a shipping job.
         * \param origin - <b>string</b> - The origin city of the shipping contract.
         * \param destination - <b>string</b> - The destination city of the shipping contract.
         * \param job_type - <b>int</b> - A integer representation of the type of truck used for the job. (‘0’ for FTL, ‘1’ for LTL)
         * \return <b>invoiceOutParams</b> - This method returns a struct that contains all of the information gleaned from the simulation to populate the invoice.
        */
        private invoiceOutParams simulateTravelRoute(string origin, string destination, int quantity, int job_type)
        {
            invoiceOutParams ret;
            contractParams order = (contractParams)orderList.SelectedItem;

            int originInt = 0;
            int destinationInt = 0;
            int travelDifference = 0;
            int distance = 0;

            double time = 0.0;
            double intTime = 0.0;
            double rateCost = 0;
            double surchargeCost = 0.0;
            double totalCost = 0.0;

            //assigning the origin city a integer representation
            switch (origin)
            {
                case "Windsor":
                    originInt = 0;
                    break;

                case "London":
                    originInt = 1;
                    break;

                case "Hamilton":
                    originInt = 2;
                    break;

                case "Toronto":
                    originInt = 3;
                    break;

                case "Oshawa":
                    originInt = 4;
                    break;

                case "Belleville":
                    originInt = 5;
                    break;

                case "Kingston":
                    originInt = 6;
                    break;

                case "Ottawa":
                    originInt = 7;
                    break;
            }

            //assigning the destination city a integer representation
            switch (destination)
            {
                case "Windsor":
                    destinationInt = 0;
                    break;

                case "London":
                    destinationInt = 1;
                    break;

                case "Hamilton":
                    destinationInt = 2;
                    break;

                case "Toronto":
                    destinationInt = 3;
                    break;

                case "Oshawa":
                    destinationInt = 4;
                    break;

                case "Belleville":
                    destinationInt = 5;
                    break;

                case "Kingston":
                    destinationInt = 6;
                    break;

                case "Ottawa":
                    destinationInt = 7;
                    break;
            }


            //
            if (originInt < destinationInt)
            {
                travelDifference = destinationInt - originInt;

                for (int i = originInt; i < travelDifference; i++)
                {
                    switch (i)
                    {
                        case 0:
                            distance += 191;
                            time += 2.5;
                            break;

                        case 1:
                            distance += 128;
                            time += 1.75;
                            break;

                        case 2:
                            distance += 68;
                            time += 1.25;
                            break;

                        case 3:
                            distance += 60;
                            time += 1.3;
                            break;

                        case 4:
                            distance += 134;
                            time += 1.65;
                            break;

                        case 5:
                            distance += 82;
                            time += 1.2;
                            break;

                        case 6:
                            distance += 196;
                            time += 2.5;
                            break;

                        case 7:
                            distance += 0;
                            time += 0;
                            break;
                    }

                    switch (job_type)
                    {
                        case 0:
                            intTime += time;
                            break;
                        case 1:
                            intTime = intTime + time + 2.0;
                            break;
                    }
                }
            }
            else if (originInt > destinationInt)
            {
                travelDifference = originInt - destinationInt;

                for (int i = originInt; i > 0; i--)
                {
                    switch (i)
                    {
                        case 0:
                            distance += 191;
                            time += 2.5;
                            break;

                        case 1:
                            distance += 128;
                            time += 1.75;
                            break;

                        case 2:
                            distance += 68;
                            time += 1.25;
                            break;

                        case 3:
                            distance += 60;
                            time += 1.3;
                            break;

                        case 4:
                            distance += 134;
                            time += 1.65;
                            break;

                        case 5:
                            distance += 82;
                            time += 1.2;
                            break;

                        case 6:
                            distance += 196;
                            time += 2.5;
                            break;

                        case 7:
                            distance += 0;
                            time += 0;
                            break;
                    }

                    switch (job_type)
                    {
                        case 0:
                            intTime += time;
                            break;
                        case 1:
                            intTime = intTime + time + 2.0;
                            break;
                    }

                }
            }

            //calculating the rate cost
            switch (job_type)
            {
                case 0:
                    rateCost = 4.985 * distance;
                    break;
                case 1:
                    rateCost = (0.2995 * distance) * quantity;
                    break;
            }

            //calculating the surcharge 
            if (intTime > 24)
            {
                for (int i = 0; i < intTime; i += 24)
                {
                    surchargeCost += 150;
                }
            }

            totalCost = surchargeCost + rateCost;

            //returning the struct with all of the calculated data
            return new invoiceOutParams { travelTime = time, travelIntTime = intTime, travelDist = distance, surchargeCost = surchargeCost, totalRateCost = rateCost, totalFinalCost = totalCost };
        }
    }
}