﻿using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Text;
using System.Windows;

namespace SQ_TeamClack_TermProj
{
    internal class Order
    {
        private ulong OrderID;
        private DateTime orderDate;
        private bool markedForAction;
        private string customerName;
        private int jobType;
        private int quantity;
        private string origin;
        private string destination;
        private int duration;
        private int vanType;

        //properties
        public ulong ORDERID { get { return OrderID; } }

        public string CUSTOMERNAME { get { return customerName; } }
        public int JOBTYPE { get { return jobType; } }
        public int QUANTITY { get { return quantity; } }
        public string ORIGIN { get { return origin; } }
        public string DESTINATION { get { return destination; } }
        public int VANTYPE { get { return vanType; } }
        public bool MARKEDFORACTION { get { return markedForAction; } set { markedForAction = value; } }

        /*!
         * \brief CONSTRUCTOR
         * \details
         * \param param - <b>orderParams</b> - A object that contains all of the parameters of the order.
         */

        public Order(contractParams param)
        {
            Random rand = new Random();
            OrderID = ulong.Parse(rand.Next(100000, 100000000).ToString());

            orderDate = DateTime.Now;
            markedForAction = false;

            customerName = param.clientName;
            jobType = param.jobType;
            quantity = param.quantity;
            origin = param.origin;
            destination = param.destination;
            vanType = param.vanType;
        }

        /*!
         * \brief This function returns when the order was created.
         * \details This function returns a copy of the time stamp that was created when the order was created.
         * \return <b>DateTime</b> - This returns a time stamp.
         */

        public DateTime getTimeStamp()
        {
            DateTime rDt = new DateTime();

            rDt = orderDate;

            return rDt;
        }

        /*!
         * \brief This function allows for the querying of an order.
         * \details
         * \param orderID -<b>ulong</b>- The ID of the order.
         * \return <b>string</b> - A string that lists out all of the important order information.
         */

        public string queryOrder(ulong orderID)
        {
            StringBuilder strBr = new StringBuilder();

            //todo queryOrder() logic

            return strBr.ToString();
        }

        /*!
         * \brief This function calculates the sub-total of the order.
         * \details
        */

        public void calculateSubTotal()
        {
            //todo calculateSubTotal() logic
        }

        /*!
         * \brief This function calculates the sub-total of the order.
         * \details
        */

        public void commitOrder(string userConStr)
        {
            string conStr = ConfigurationManager.ConnectionStrings[userConStr].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("UPDATE INTO Orders(CustomerName, JobType, Quantity, Origin, Destination, Van_Type) VALUES ('" + this.customerName + "', " + this.jobType + ", " + this.quantity + ", '" + this.origin + "', '" + this.destination + "', " + this.vanType + ");");

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
        }
    }
}