using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQ_TeamClack_TermProj
{
    class Planner
    {
        private ulong PlannerID;
        private string firstName;
        private string lastName;

        /*!
         * \brief CONSTRUCTOR
         * \details
         * 
         * \param ...
         */
        public Planner()
        {
        
        }

        /*!
         * \brief This function allows the planner to select the carrier for transportation.
         * \details
         */
        public void selectCarrier()
        {
            //todo selectCarrier() logic
        }

        /*!
         * \brief This function calculates the route from the trip, given the start and finish.
         * \details
         * \param start - <b>string</b> - The starting point of the route.
         * \param finish - <b>string</b> - The ending point of the route.
         */
        public void createRoute(string start, string finish)
        {
            //todo createRoute() logic
        }

        /*!
         * \brief This function increments the trip time.
         * \details
         */
        public void incrementTripTime()
        {
            //todo incrementTripTime() logic
        }

        /*!
         * \brief This function displays the order summary.
         * \details
         * \param orderID - <b>ulong</b> - The ID of the order.
         * \return <b>string</b> - A string that contains a summary of the order.
         */
        public string displayOrderSummary(ulong orderID)
        {
            StringBuilder strBr = new StringBuilder();

            //todo displayOrderSummary() logic

            return strBr.ToString();
        }

        /*!
         * \brief This function displays the summary report.
         * \details
         * \param orderID - <b>ulong</b> - The ID of the order.
         * \return <b>string</b> - A string that contains the summary report.
         */
        public string generateSummaryReport(ulong orderID)
        {
            StringBuilder strBr = new StringBuilder();

            //todo generateSummaryReport() logic

            return strBr.ToString();
        }
    }
}
