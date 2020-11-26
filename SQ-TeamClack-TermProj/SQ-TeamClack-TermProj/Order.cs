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
        private Item[] orderManifest; //a better idea for this would be placing it inside of a data structure
        private DateTime orderDate;
        private bool markedForAction = false;
        private string[] tripRoutes;
        private double totalOrderWeight;
        private string carrierData;
        private double fees;

        /*!
         * \brief CONSTRUCTOR
         * \details
         * 
         * \param ...
         */
        public Order()
        {

        }

        /*!
         * \brief ...
         * \details
         */
        private void calculateWeight()
        {
            //todo calculateWeight() logic

            //totalOrderWeight = ;
        }

        /*!
         * \brief ...
         * \details
         * \return
         */
        private DateTime getTime()
        {
            DateTime rDt = new DateTime();

            //todo getTime() logic

            return rDt;
        }

        /*!
         * \brief ...
         * \details
         * \return
         */
        public string queryOrder()
        {
            StringBuilder strBr = new StringBuilder();

            //todo queryOrder() logic

            return strBr.ToString();
        }

        /*!
         * \brief ...
         * \details
         * \return
         */
        public string queryManifest()
        {
            StringBuilder strBr = new StringBuilder();

            //todo queryManifest() logic

            return strBr.ToString();
        }

        /*!
         * \brief ...
         * \details
         */
        public void recalculateWeight()
        {
            //todo recalculateWeight() logic
        }

        /*!
         * \brief ...
         * \details
         */
        public void calculateSubTotal()
        {
            //todo calculateSubTotal() logic
        }

    }
}
