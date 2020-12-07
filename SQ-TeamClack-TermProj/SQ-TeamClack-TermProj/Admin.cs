using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQ_TeamClack_TermProj
{
    class Admin
    {
        private ulong AdminID;
        private string userName;
        private string password;

        /*!
         * \brief ...
         * \details
         */
        public Admin(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        /*!
         * \brief This function allows an admin to configure the application settings.
         * \details 
         */
        public void adminConfig()
        {
            //todo adminConfig() logic
        }

        /*!
         * \brief This function allows an admin to access and view the log files.
         * \details
         */
        public void reviewLogFiles()
        {
            //todo reviewLogFiles() logic
        }

        /*!
         * \brief This function allows an admin to modify the prices in the rate tables.
         * \details
         * \param adminID - <b>ulong</b> - The ID of the admin user; used for security and audit purposes.
         */
        public void modifyRateTables(ulong adminID)
        {
            //todo modifyRateTables() logic
        }

        /*!
         * \brief This function allows an admin to modify the registered carrier data.
         * \details
         * \param adminID - <b>ulong</b> - The ID of the admin user; used for security and audit purposes. 
         */
        public void modifyCarrierData(ulong adminID)
        {
            //todo modifyCarrierData() logic
        }

        /*!
         * \brief This function allows an admin to modify the routes within the route table.
         * \details
         * \param adminID - <b>ulong</b> - The ID of the admin user; used for security and audit purposes.
         */
        public void modifyRouteTable(ulong adminID)
        {
            //todo modifyRouteTable() logic
        }

        /*!
         * \brief This function allows an admin to back-up all of the data within the system.
         * \details
         * \param adminID - <b>ulong</b> - The ID of the admin user; used for security and audit purposes.
         */
        public void initiateBackup(ulong adminID)
        {
            //todo initiateBackup() logic
        }
    }
}
