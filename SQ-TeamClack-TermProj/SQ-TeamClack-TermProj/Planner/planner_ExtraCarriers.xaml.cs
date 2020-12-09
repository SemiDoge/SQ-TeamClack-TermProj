using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for planner_ExtraCarriers.xaml
    /// </summary>
    public partial class planner_ExtraCarriers : Window
    {
        public planner_ExtraCarriers()
        {
            InitializeComponent();
        }

        public string ResponseText
        {
            get { return tripInput.Text; }
            set { tripInput.Text = value; }
        }

        private void addTripBTN_Click(object sender, RoutedEventArgs e)
        {
            if (tripInput.Text != "")
            {
                // Return value from textbox to previous window
                // Then return to previous window
                DialogResult = true;
                this.Close();
            }
        }
    }
}
