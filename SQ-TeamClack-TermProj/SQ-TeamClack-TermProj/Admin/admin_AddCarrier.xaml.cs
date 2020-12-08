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
    /// Interaction logic for admin_AddCarrier.xaml
    /// </summary>
    public partial class admin_AddCarrier : Window
    {
        public admin_AddCarrier()
        {
            InitializeComponent();
        }

        private void submitBTN_Click(object sender, RoutedEventArgs e)
        {
            if (carrierNameInput.Text != "" && cityInput.Text == "" && ftlRateInput.Text == "" && ltlRateInput.Text == "" && reefChargeInput.Text == "")
            {
                DialogResult = true;
            }
        }

        public string ResponseTextCarrierName
        {
            get { return carrierNameInput.Text; }
            set { carrierNameInput.Text = value; }
        }

        public string ResponseTextCity
        {
            get { return cityInput.Text; }
            set { cityInput.Text = value; }
        }

        public string ResponseTextFTL
        {
            get { return ftlRateInput.Text; }
            set { ftlRateInput.Text = value; }
        }

        public string ResponseTextLTL
        {
            get { return ltlRateInput.Text; }
            set { ltlRateInput.Text = value; }
        }

        public string ResponseTextReefCharge
        {
            get { return reefChargeInput.Text; }
            set { reefChargeInput.Text = value; }
        }
    }
}
