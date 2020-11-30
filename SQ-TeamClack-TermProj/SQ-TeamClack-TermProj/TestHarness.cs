using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SQ_TeamClack_TermProj
{
    class TestHarness
    {
        static void Main(string[] args)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost; port=3306; UID=root; PASSWORD=E3e25475515!; DATABASE=Omnicorp");

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Items;");
            connection.Open();

            cmd.Connection = connection;

            string x = cmd.ExecuteScalar().ToString();


            connection.Close();

            Console.WriteLine(x);
            Console.ReadLine();
        }
    }
}
