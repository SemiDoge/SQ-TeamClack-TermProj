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

        public Order()
        {

        }

        private void calculateWeight()
        {
            //todo calculateWeight() logic

            //totalOrderWeight = ;
        }

        private DateTime getTime()
        {
            DateTime rDt = new DateTime();

            //todo getTime() logic

            return rDt;
        }

        public string queryOrder()
        {
            StringBuilder strBr = new StringBuilder();

            //todo queryOrder() logic

            return strBr.ToString();
        }

        public string queryManifest()
        {
            StringBuilder strBr = new StringBuilder();

            //todo queryManifest() logic

            return strBr.ToString();
        }

        public void recalculateWeight()
        {
            //todo recalculateWeight() logic
        }

        public void calculateSubTotal()
        {
            //todo calculateSubTotal() logic
        }

    }
}
