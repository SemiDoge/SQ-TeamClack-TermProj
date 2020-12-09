using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for planner_OrderStatus.xaml
    /// </summary>
    public partial class planner_OrderStatus : Page
    {
        private User localUser;
        contractParams newContract;

        public planner_OrderStatus(User localUser)
        {
            InitializeComponent();
            this.localUser = localUser;
            File.AppendAllText(@"Log\Log.log", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Planner loaded order status.\n");
        }

        private void incrementTimeBTN_Click(object sender, RoutedEventArgs e)
        {
            contractParams temp = (contractParams)orderList.SelectedItem;

            temp.duration -= 24;

            if (temp.duration < 0)
            {
                temp.duration = 0;
                temp.markedForAction = true;
            }

            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("UPDATE Orders SET ETA=" + temp.duration.ToString() + ", MarkedForAction=" + temp.markedForAction.ToString() + " WHERE OrderID=" + temp.orderID.ToString() + ";");

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySqlCommand cmd = new MySqlCommand(cmdSB.ToString(), connection);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
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
            File.AppendAllText(@"Log\Log.log", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Planner incremented time for an order.\n");
            planner_OrderStatus newPage = new planner_OrderStatus(localUser);
            this.NavigationService.Navigate(newPage);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Then add all the entries from the database into the list
            // Then set the orderList.ItemsSource equal to the list
            // EX: orderList.ItemsSource = orderL;
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT OrderID, ETA, MarkedForAction FROM Orders;");
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
                            duration = int.Parse(reader["ETA"].ToString()),
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

        private void allTimeReportBTN_Click(object sender, RoutedEventArgs e)
        {
            createSummaryReport();
            File.AppendAllText(@"Log\Log.log", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Planner created summary report.\n");
        }

        private void createSummaryReport()
        {
            StringBuilder sb = new StringBuilder();
            MySqlDataReader reader = null;
            bool shownDialog = false;

            //Open up a save file dialog so that the user can choose where they want the invoice saved
            SaveFileDialog invoiceFile = new SaveFileDialog();
            invoiceFile.Filter = "Text file(*.txt)|*.txt|All Files (*.*)|*.*";
            invoiceFile.Title = "Save an Invoice (.txt)";

            if (invoiceFile.ShowDialog() == true)
            {
                shownDialog = true;
            }

            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT OrderID, CustomerName, Origin, Destination, MarkedForAction, Quantity, JobType, OrderDate, CarrierName, Van_Type FROM Orders;");

            sb.Append("Displaying summary for " + getNumberOfOrders() + " orders.\n");

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySqlCommand cmd = new MySqlCommand(cmdSB.ToString(), connection);
                try
                {
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        newContract = new contractParams {
                            orderID = ulong.Parse(reader["OrderID"].ToString()),
                            orderDate = reader["OrderDate"].ToString(),
                            clientName = reader["CustomerName"].ToString(),
                            jobType = int.Parse(reader["JobType"].ToString()),
                            quantity = int.Parse(reader["Quantity"].ToString()),
                            origin = reader["Origin"].ToString(),
                            destination = reader["Destination"].ToString(),
                            vanType = int.Parse(reader["Van_Type"].ToString()),
                            carrierName = reader["CarrierName"].ToString()
                            
                        };

                        var temp = simulateTravelRoute(newContract.origin, newContract.destination, newContract.quantity, newContract.jobType);


                        //Building the invoice
                        sb.Append("========================================================================\n");
                        sb.Append("Order Number: " + newContract.orderID + "\n");
                        sb.Append("Customer: " + newContract.clientName + "\n");
                        sb.Append("Origin: " + newContract.origin + "\n");
                        sb.Append("Destination: " + newContract.destination + "\n");
                        sb.Append("Total Travel Time: " + temp.travelTime.ToString() + "\n");
                        sb.Append("Total Intermediary Time: " + temp.travelIntTime.ToString() + "\n");
                        sb.Append("Total Travel Distance: " + temp.travelDist.ToString() + "\n");
                        sb.Append("Total Surcharge Cost: " + temp.surchargeCost + "\n");
                        sb.Append("Total Rate Cost: " + temp.totalRateCost.ToString() + "\n");
                        sb.Append("Total Final Cost: " + temp.totalFinalCost.ToString() + "\n");
                        sb.Append("========================================================================\n");

                        //writing the invoice to the file
                        if (shownDialog == true)
                        {
                            using (StreamWriter sw = new StreamWriter(invoiceFile.FileName, true))
                            {
                                sw.WriteLine(sb.ToString());
                                sb.Clear();
                            }
                        }

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


        private int getNumberOfOrders()
        {
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT COUNT(*) FROM Orders;");
            int orderCount = 0;

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySqlCommand cmd = new MySqlCommand(cmdSB.ToString(), connection);
                try
                {
                    connection.Open();
                    orderCount = int.Parse(cmd.ExecuteScalar().ToString());
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

            return orderCount;
        }
    }
}
