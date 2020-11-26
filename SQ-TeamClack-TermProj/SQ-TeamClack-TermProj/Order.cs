using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SQ_TeamClack_TermProj
{
    class Order
    {
        private ulong OrderID;
        private List<Item> orderManifest; //a better idea for this would be placing it inside of a data structure
        private DateTime orderDate;
        private bool markedForAction = false;
        private string[] tripRoutes;
        private double totalOrderWeight;
        private string carrierData;
        private double fees;

        /*!
         * \brief CONSTRUCTOR
         * \details
         * \param ...
         */
        public Order()
        {

        }

        /*!
         * \brief This function calculates the total weight of all the items in the order.
         * \details
         * \param orderID - <b>ulong</b> - The ID of the order.
         */
        private void calculateWeight(ulong orderID)
        {
            //todo calculateWeight() logic

            //totalOrderWeight = ;
        }

        /*!
         * \brief This function gets a time stamp of the current time.
         * \details
         * \return <b>DateTime</b> - This returns a time stamp.
         */
        private DateTime getTime()
        {
            DateTime rDt = new DateTime();

            //todo getTime() logic

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
         * \brief This function allows for the query of the order manifest (an itemized representaion of the order).
         * \details
         * \param orderID -<b>ulong</b>- The ID of the order.
         * \return <b>string</b> - A string that lists out all of the items in the order.
         */
        public string queryManifest(ulong orderID)
        {
            StringBuilder strBr = new StringBuilder();

            //todo queryManifest() logic

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

    }
}
