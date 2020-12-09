using System.Windows;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for admin_AddCarrier.xaml
    /// </summary>
    public partial class admin_AddCarrier : Window
    {

        /*!
         * \brief CONSTRUCTOR - This constructor constructs the IP Config window.
         * \details This constructor initializes all the properties that are needed in order to use the IP Config window.
         * \param <b>void</b>
        */

        public admin_AddCarrier()
        {
            InitializeComponent();
        }


        /*!
         * \brief This handler handles when the user clicks the "Submit" button.
         * \details This handler validates the add carrier window.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void submitBTN_Click(object sender, RoutedEventArgs e)
        {
            if (carrierNameInput.Text != "" && cityInput.Text == "" && ftlRateInput.Text == "" && ltlRateInput.Text == "" && reefChargeInput.Text == "")
            {
                DialogResult = true;
            }
        }

        public string ResponseTextCarrierName { get { return carrierNameInput.Text; } set { carrierNameInput.Text = value; } }
        public string ResponseTextCity { get { return cityInput.Text; } set { cityInput.Text = value; } }
        public string ResponseTextFTL { get { return ftlRateInput.Text; } set { ftlRateInput.Text = value; } }
        public string ResponseTextLTL { get { return ltlRateInput.Text; } set { ltlRateInput.Text = value; } }
        public string ResponseTextReefCharge { get { return reefChargeInput.Text; } set { reefChargeInput.Text = value; } }
    }
}