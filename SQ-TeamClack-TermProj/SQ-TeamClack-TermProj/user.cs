namespace SQ_TeamClack_TermProj
{
    // Purpose: To hold information tied to the current user of the program
    public class User
    {
        private string userName;
        private string password;
        private string conStr;
        public static string localUsername;
        public static bool orderInProgress;

        public User(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            this.conStr = "loginAcc";
        }

        public string USERNAME { get { return userName; } set { userName = value; } }
        public string PASSWORD { get { return password; } set { password = value; } }
        public string CONSTR { get { return conStr; } set { conStr = value; } }

        /*!
         * \brief This function logs the user out.
         * \details This functions clears all of the information associated with the logged
         * \return <b>void</b>
        */

        public void logout()
        {
            this.userName = "";
            this.password = "";
            this.conStr = "loginAcc";
            // this.orderInProgress = false;
        }

        public void setOrderProgress(bool input)
        {
            orderInProgress = input;
        }

        public bool getOrderProgress()
        {
            return orderInProgress;
        }
    }
}