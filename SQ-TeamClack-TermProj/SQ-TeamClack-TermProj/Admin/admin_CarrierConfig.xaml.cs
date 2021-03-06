﻿using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for admin_CarrierConfig.xaml
    /// </summary>
    public partial class admin_CarrierConfig : Page
    {
        private User localUser;

        /*!
         s \brief CONSTRUCTOR - This constructor constructs the Carrier Config page.
         * \details This constructor constructs the Carrier Config page and sets the page's localUser to the localUser passed in as a parameter.
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
        */

        public admin_CarrierConfig(User localUser)
        {
            InitializeComponent();

            this.localUser = localUser;
        }

        /*!
         * \brief This handler handles when the user clicks the "List Carriers" button.
         * \details This handler calls the queryCarrier method to populate the carrierList List View.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void carrierButton_Click(object sender, RoutedEventArgs e)
        {
            queryCarrier();
        }

        /*!
         * \brief This handler handles the adding new carriers to the Omnicorp database.
         * \details This handler runs an UPDATE command to change anything about the carrier information that the user wishes.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void carrierAdd_Click(object sender, RoutedEventArgs e)
        {
            admin_AddCarrier addCarrier = new admin_AddCarrier();
            addCarrier.ShowDialog();

            carrierParams carrier = new carrierParams { carrierName = addCarrier.ResponseTextCarrierName, cityName = addCarrier.ResponseTextCity, FTLRate = double.Parse(addCarrier.ResponseTextFTL), LTLRate = double.Parse(addCarrier.ResponseTextLTL), reefCharge = double.Parse(addCarrier.ResponseTextReefCharge) };

            // Connect to database
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("INSERT INTO Carrier(CarrierName) VALUES ('" + carrier.carrierName + "'); INSERT INTO CarrierCities (CityName, FTLRate, LTLRate, reefCharge) VALUES ('" + carrier.cityName + "', " + carrier.FTLRate + ", " + carrier.LTLRate + ", " + carrier.reefCharge + ");");

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(cmdSB.ToString(), connection);
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

            carrierList.DataContext = null;
            carrierList.Items.Clear();
            queryCarrier();
        }

        /*!
         * \brief This handler handles the updating of the carrier information.
         * \details This handler runs an UPDATE command to change anything about the carrier information that the user wishes.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void carrierModify_Click(object sender, RoutedEventArgs e)
        {
            carrierParams temp = (carrierParams)carrierList.SelectedItem;

            admin_AddCarrier addCarrier = new admin_AddCarrier();
            addCarrier.ShowDialog();

            carrierParams carrier = new carrierParams { carrierName = addCarrier.ResponseTextCarrierName, cityName = addCarrier.ResponseTextCity, FTLRate = double.Parse(addCarrier.ResponseTextFTL), LTLRate = double.Parse(addCarrier.ResponseTextLTL), reefCharge = double.Parse(addCarrier.ResponseTextReefCharge) };

            // Connect to database
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("UPDATE Carrier SET CarrierName='" + carrier.carrierName + "' WHERE CarrierName='" + temp.carrierName + ";  UPDATE CarrierCities SET FTLRate=" + carrier.FTLRate + ", LTLRate=" + carrier.LTLRate + ", reefCharge=" + carrier.reefCharge + " WHERE CarrierID IN (SELECT CarrierID FROM Carrier WHERE CarrierName=" + temp.carrierName + ");");

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(cmdSB.ToString(), connection);
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

            carrierList.DataContext = null;
            carrierList.Items.Clear();
            queryCarrier();
        }

        /*!
         * \brief This method populates the carrierList List View.
         * \details This method queries the Omnicorp database in order to populate a List View with the carriers known to Omnicorp.
         * \param <b>void</b>
        */

        private void queryCarrier()
        {
            // Connect to database
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT DISTINCT ca.CarrierName, cac.CityName, cac.FTLA, cac.LTLA, cac.FTLRate, cac.LTLRate, cac.reefCharge FROM CarrierCities cac, Carrier ca;");
            MySqlDataReader reader = null;

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(cmdSB.ToString(), connection);
                try
                {
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    // Once connected, fill textbox with information
                    while (reader.Read())
                    {
                        carrierList.Items.Add(new carrierParams
                        {
                            carrierName = reader["CarrierName"].ToString(),
                            cityName = reader["CityName"].ToString(),
                            FTLA = double.Parse(reader["FTLA"].ToString()),
                            LTLA = double.Parse(reader["LTLA"].ToString()),
                            FTLRate = double.Parse(reader["FTLRate"].ToString()),
                            LTLRate = double.Parse(reader["LTLRate"].ToString()),
                            reefCharge = double.Parse(reader["reefCharge"].ToString())
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
    }
}