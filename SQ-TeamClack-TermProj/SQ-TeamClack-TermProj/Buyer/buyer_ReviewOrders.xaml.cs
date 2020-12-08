using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System.Configuration;

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
            InitializeComponent();

            this.localUser = localUser;
            UsernameLabel.Content = localUser.USERNAME;
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
         * \details This handler makes to change the page to the review customers page. This handler needs to also make sure that the user means to leave the order creation process, in progress.
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
         * \brief This handler handles when the user clicks the "Review Orders" button.
         * \details This handler calls the queryOpenOrders method.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void ReviewOrdersBTN_Click(object sender, RoutedEventArgs e)
        {
            queryOpenOrders();
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
            StringBuilder cmdSB = new StringBuilder("SELECT OrderID, Origin, Destination, MarkedForAction, OrderDate FROM Orders;");
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
                            orderDate = reader["OrderDate"].ToString(),
                            origin = reader["Origin"].ToString(),
                            destination = reader["Destination"].ToString(),
                            MARKEDFORACTION = bool.Parse(reader["MarkedForAction"].ToString())
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
        }


        /*!
         * \brief This handler handles the logic behind clicking the Invoice Order button.
         * \details 
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void invoiceOrderBTN_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw;
            StringBuilder sb = new StringBuilder();
            SaveFileDialog invoiceFile = new SaveFileDialog();
            invoiceFile.Filter = "Text file(*.txt)|*.txt|All Files (*.*)|*.*";
            invoiceFile.Title = "Save an Invoice (.txt)";

            sb.Append("========================================================================\n");
            sb.Append("Order Number: " + p.orderID + "\n");
            sb.Append("Customer: " + p.clientName + "\n");
            sb.Append("Origin: " + p.origin + "\n");
            sb.Append("Destination: " + p.destination + "\n");
            sb.Append("Total Travel Time: " + calculateTotalTravelTime(p.origin, p.destination, p.jobType).ToString() + "\n");
            sb.Append("Total Intermediary Time: " + +"\n");
            sb.Append("Total Travel Time: " + +"\n");
            sb.Append("Total Surcharge Cost: " + +"\n");
            sb.Append("Total Rate Cost: " + +"\n");
            sb.Append("Total Final Cost: " + +"\n");
            sb.Append("========================================================================");

            if (invoiceFile.ShowDialog() == true)
            {
                sw = new StreamWriter(invoiceFile.FileName);
            }
        }

        /*!
         * \brief This handler handles the logic behind clicking the Invoice Order button.
         * \details 
         * \param origin - <b>string</b> -
         * \param destination - <b>string</b> -
         * \param job_type - <b>int</b> -
        */
        private Object[] calculateTotalTravelTime(string origin, string destination, int job_type)
        {
            //string[] cities = { "Windsor", "London", "Hamilton", "Toronto", "Oshawa", "Belleville", "Kingston", "Ottawa" };

            switch (job_type)
            {
                case 0:

                    break;
                case 1:
                    break;
            }

            contractParams order = (contractParams)orderList.SelectedItem;

            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;

            int originInt = 0;

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


            int destinationInt = 0;


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

            int travelDifference = 0;
            int distance = 0;
            double time = 0.0;

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

                }
            }
            else if (originInt > destinationInt)
            {
                travelDifference = originInt - destinationInt;

                for (int i = originInt; i < travelDifference; i--)
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
                }
            }

            Object[] temp = { distance, time };

            return temp;

        }

    }
}