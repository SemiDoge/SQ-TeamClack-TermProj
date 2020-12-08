using System.Windows;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for addCustomerWindow.xaml
    /// </summary>
    public partial class addCustomerWindow : Window
    {
        public addCustomerWindow()
        {
            InitializeComponent();
        }

        public string ResponseText
        {
            get { return cusNameInput.Text; }
            set { cusNameInput.Text = value; }
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (cusNameInput.Text != "")
            {
                DialogResult = true;
            }
        }
    }
}