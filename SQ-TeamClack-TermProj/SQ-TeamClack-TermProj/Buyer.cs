using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace SQ_TeamClack_TermProj
{
    class Buyer
    {
        private string userName;
        private string password;
        private string conStr;

        private ulong BuyerID;
        private ulong BuyerName;


        /*!
         * \brief ...
         * \details
         * \return
        */
        public Buyer(User user)
        {

        }

        /*!
         * \brief This function allows the buyer to choose from a list of cities in order to deliever.
         * \details
         * \param city - <b>string</b> - The city for carrier nominiaton.
         */
        private void selectCity(string city)
        {
            //todo selectCity() logic
        }

        /*!
         * \brief This function allow the buyer to generate their invoice, given their orders' orderID.
         * \details
         * \param orderID - <b>ulong</b> - The ID of the order.
         */
        private void generateInvoice(ulong orderID)
        {
            //todo generateInvoice() logic
        }

        /*!
          * \brief This function allows the buyer to review existing customer information.
          * \details
          * \param buyerID - <b>ulong</b> - The ID of the buyer.
          */
        public string reviewExistingCustomer(string name)
        {
            string conStr = ConfigurationManager.ConnectionStrings[conStr].ConnectionString;
            StringBuilder cmdSB = new StringBuilder("SELECT COUNT(*) FROM Users WHERE Name='" + this.usernameInput + "' AND Password=" + this.passwordInput + ";");

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(cmdSB.ToString(), connection);
                try
                {
                    connection.Open();
                    // store and return string

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
         * \brief This function allows the buyer to accept new customers.
         * \details
         * \param buyerID - <b>ulong</b> - The ID of the buyer.         
         */
        public void acceptNewCustomer(ulong buyerID)
        {
            //todo acceptNewCustomer() logic
        }

        /*!
         * \brief This function allows for the creation of a new order.
         * \details 
         * \return <b>Order</b> - This newly created Order object/
         */
        public Order inintateNewOrder()
        {
            Order ordRet = new Order();

            //todo inintateNewOrder() logic

            return ordRet;
        }
    }
}
