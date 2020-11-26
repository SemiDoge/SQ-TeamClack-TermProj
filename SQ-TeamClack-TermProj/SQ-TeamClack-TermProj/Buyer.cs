using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
         * \brief
         * \details
         */
        private void selectCity()
        {
            //todo selectCity() logic
        }

        /*!
         * \brief
         * \details
         * \param
         */
        private void generateInvoice(ulong buyerID)
        {
            //todo generateInvoice() logic
        }

        public void reviewExistingCustomer()
        {
            //todo reviewExistingCustomer() logic
        }

        /*!
         * \brief
         * \details
         * \param
         */
        public void acceptNewCustomer(string str)
        {
            //todo acceptNewCustomer() logic
        }

        /*!
         * \brief
         * \details
         * \param
         * \return 
         */
        public Order inintateNewOrder()
        {
            Order ordRet = new Order();

            //todo inintateNewOrder() logic

            return ordRet;
        }

    }


}
