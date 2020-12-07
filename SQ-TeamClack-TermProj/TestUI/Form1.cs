using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace TestUI
{
    public partial class Form1 : Form
    {
        const int CITY = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fillComboBox(CITY);
        }

        private void fillComboBox(int comboNum)
        {
            string conStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                switch (comboNum)
                {
                    case CITY:
                        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("SELECT DISTINCT CityName FROM City;", connection);
                        try
                        {
                            connection.Open();
                            MySqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                this.comboBox1.Items.Add(reader["CityName"].ToString());
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        finally
                        {
                            connection.Close();
                        }
                        break;
                    case 2:
                        "SELECT DISTINCT CarrierName FROM Carrier c,  WHERE "
                        break;
                }
            }
        }
    }
}
