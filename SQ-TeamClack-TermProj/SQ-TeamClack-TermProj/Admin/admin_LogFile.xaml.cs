using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for admin_LogFile.xaml
    /// </summary>
    public partial class admin_LogFile : Window
    {
        User localUser;

        /*!
         * \brief 
         * \details 
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */
        public admin_LogFile(User localUser)
        {
            InitializeComponent();

            StreamReader readFile = new StreamReader(File.OpenRead(SQ_TeamClack_TermProj.MainWindow.logfileLocation));
            LogFileDisplayBlock.Text = readFile.ReadToEnd();
            readFile.Dispose();
        }
    }
}
