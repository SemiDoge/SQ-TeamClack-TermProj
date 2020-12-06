using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testUI
{
    // Purpose: To hold information tied to the current user of the program
    public class user
    {
        public static string localUsername;

        public void setUsername(string input)
        {
            localUsername = input;
        }
        public string getUsername()
        {
            return localUsername;
        }

    }
}
