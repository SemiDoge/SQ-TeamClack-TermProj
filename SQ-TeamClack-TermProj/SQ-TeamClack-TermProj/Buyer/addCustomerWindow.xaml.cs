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
