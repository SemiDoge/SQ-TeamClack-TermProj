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
using System.IO;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for planner_CompleteOrder.xaml
    /// </summary>
    public partial class planner_CompleteOrder : Page
    {
        private User localUser;
        private contractParams tempOrder;
        contractParams tempQO;
        int MAX_QUANTITY = 26;
        string tempTripCount = "";

        /*!
         * \brief CONSTRUCTOR - This constructor constructs the complete order page.
         * \details This constructor constructs the complete order page and sets the page's localUser to the localUser passed in as a parameter.
         * \param <b>void</b>
        */
        public planner_CompleteOrder(User localUser)
        {
            InitializeComponent();
            this.localUser = localUser;
            File.AppendAllText(@"Log\Log.txt", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Planner loaded completed order.\n");
        }

        /*!
         * \brief This handler handles when the user clicks the "Submit" button.
         * \details 
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Then add all the entries from the database into the list
            // Then set the orderList.ItemsSource equal to the list
            // EX: orderList.ItemsSource = orderL;
            // Then add all carriers that correspond to the city set by the buyer into the Combo Box

            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT OrderID, Origin FROM Orders WHERE MarkedForAction=False;");
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
                            origin = reader["Origin"].ToString()
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
         * \brief This handler handles when the user clicks the "Submit" button.
         * \details This method stores the completed order information into the Orders database.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            int tripCount = 1;
            int jobType;
            int quantity;
            string origin;
            string destination;
            int originInt = 0;
            int destinationInt = 0;
            int travelDifference = 0;
            int distance = 0;
            double time = 4;
            double intTime = 0;

            string temp = carrierBox.Text;
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT JobType, Quantity, Origin, Destination FROM Orders WHERE OrderID=" + tempOrder.orderID + ";");
            MySqlDataReader reader = null;
            

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySqlCommand cmd = new MySqlCommand(cmdSB.ToString(), connection);
                try
                {
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    tempQO = new contractParams { jobType = int.Parse(reader["JobType"].ToString()),
                        quantity = int.Parse(reader["Quantity"].ToString()),
                        origin = reader["Origin"].ToString(),
                        destination = reader["Destination"].ToString(), };
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

            jobType = tempQO.jobType;
            quantity = tempQO.quantity;
            origin = tempQO.origin;
            destination = tempQO.destination;

            switch (jobType)
            {
                case 0:
                    tripCount = 1;
                    break;
                case 1:
                    var addTrip = new planner_ExtraCarriers();
                    if (quantity > 26)
                    {
                        if (addTrip.ShowDialog() == true)
                        {
                            // Move return string from addtrip window into variable
                            tempTripCount = addTrip.ResponseText;
                            // Convert to int
                            tripCount = Int32.Parse(tempTripCount);
                        }
                    }
                    break;
            }

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

                    switch (jobType)
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

                    switch (jobType)
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

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {                
                // update order info
                cmdSB = new StringBuilder("UPDATE Orders SET CarrierName='" + temp + "', NumberOfTrips=" + tripCount.ToString() + ", ETA=" + intTime.ToString() + ", MarkedForAction=True WHERE OrderID=" + tempOrder.orderID + ";");
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

            File.AppendAllText(@"Log\Log.txt", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Planner completed an order.\n");
            planner_CompleteOrder newPage =  new planner_CompleteOrder(localUser);
            this.NavigationService.Navigate(newPage);
        }


        /*!
         * \brief This handler handles when the user selects something in the carrierBox.
         * \details 
         * \param sender <b>object</b>
         * \param e <b>SelectionChangedEventArgs</b>
        */
        private void carrierBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            submitButton.IsEnabled = true;
        }

        /*!
         * \brief This handler handles when the user selects something in the.
         * \details 
         * \param sender <b>object</b>
         * \param e <b>SelectionChangedEventArgs</b>
        */
        private void orderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tempOrder = (contractParams)orderList.SelectedItem;
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT ca.CarrierName FROM Carrier ca, CarrierCities cac WHERE ca.CarrierID = cac.CarrierID AND cac.CityName='" + tempOrder.origin + "';");
            MySqlDataReader reader = null;

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySqlCommand cmd = new MySqlCommand(cmdSB.ToString(), connection);
                try
                {
                    connection.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        carrierBox.Items.Add(reader["CarrierName"].ToString());
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

            carrierBox.IsEnabled = true;
        }
    }
}
