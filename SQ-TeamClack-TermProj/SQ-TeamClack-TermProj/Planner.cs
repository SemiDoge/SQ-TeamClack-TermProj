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
         * \brief ...
         * \details
         * \return
         */
        public void selectCarrier()
        {
            //todo selectCarrier() logic
        }

        /*!
         * \brief ...
         * \details
         * \param ...
         * \param ...
         * \return
         */
        public void createRoute(string start, string finish)
        {
            //todo createRoute() logic
        }

        /*!
         * \brief ...
         * \details
         * \return
         */
        public void incrementTripTime()
        {
            //todo incrementTripTime() logic
        }

        /*!
         * \brief ...
         * \details
         * \param ...
         * \return
         */
        public string displayOrderSummary(ulong orderID)
        {
            StringBuilder strBr = new StringBuilder();

            //todo displayOrderSummary() logic

            return strBr.ToString();
        }

        /*!
         * \brief ...
         * \details
         * \param ...
         * \return
         */
        public string generateSummaryReport(ulong orderID)
        {
            StringBuilder strBr = new StringBuilder();

            //todo generateSummaryReport() logic

            return strBr.ToString();
        }
    }
}
