using System;
﻿using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for admin_TableConfig.xaml
    /// </summary>
    public partial class admin_TableConfig : Page
    {
        private User localUser;

        /*!
         * \brief CONSTRUCTOR - This constructor constructs the Table Config page.
         * \details This constructor constructs the Table Config page and sets the page's localUser to the localUser passed in as a parameter.
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
        */

        public admin_TableConfig(User localUser)
        {
            InitializeComponent();

            this.localUser = localUser;
            carrierAdd.IsEnabled = false;
        }

        /*!
         * \brief This handler handles what happens when the user clicks on the "List Routes" button.
         * \details This handler calls the queryRouteTable function when called.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void routeButton_Click(object sender, RoutedEventArgs e)
        {
            queryRouteTable();
        }

        /*!
         * \brief DISABLED - Mock up button
         * \details
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void carrierAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        /*!
         * \brief This method populates the routeList List View.
         * \details This method queries the Omnicorp database in order to populate the shipping routes.
         * \param <b>void</b>
        */

        private void queryRouteTable()
        {
            // Connect to database
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT Destination, Distance, Time, West, East FROM Route;");
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
                        routeList.Items.Add(new routeParams
                        {
                            destination = reader["Destination"].ToString(),
                            distance = int.Parse(reader["Distance"].ToString()),
                            time = double.Parse(reader["Time"].ToString()),
                            west = reader["West"].ToString(),
                            east = reader["East"].ToString()
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