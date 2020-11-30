using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost; port=3306; UID=root; PASSWORD=E3e25475515!; DATABASE=Omnicorp");

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Items;");
            connection.Open();

            cmd.Connection = connection;

            MySqlDataReader myReader = cmd.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            myReader.Read();
            for (int i = 0; i < 6; i++)
            {
                sb.Append(myReader[i].ToString());
                i++;
            }


            connection.Close();

            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }
    }
}
