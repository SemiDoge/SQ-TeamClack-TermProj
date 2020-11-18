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

        public Planner()
        {

        }

        public void selectCarrier()
        {
            //todo selectCarrier() logic
        }

        public void createRoute(string start, string finish)
        {
            //todo createRoute() logic
        }

        public void incrementTripTime()
        {
            //todo incrementTripTime() logic
        }

        public string displayOrderSummary(ulong orderID)
        {
            StringBuilder strBr = new StringBuilder();

            //todo displayOrderSummary() logic

            return strBr.ToString();
        }

        public string generateSummaryReport(ulong orderID)
        {
            StringBuilder strBr = new StringBuilder();

            //todo generateSummaryReport() logic

            return strBr.ToString();
        }
    }
}
