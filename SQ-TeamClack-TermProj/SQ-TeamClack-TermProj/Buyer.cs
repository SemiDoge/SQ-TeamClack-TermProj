using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SQ_TeamClack_TermProj
{
    class Buyer
    {
        private ulong BuyerID;
        private string firstName;
        private string lastName;

        /*!
         * \brief ...
         * \details
         * \return
         */
        public Buyer()
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
        public void reviewExistingCustomer(ulong buyerID)
        {
            //todo reviewExistingCustomer() logic
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
