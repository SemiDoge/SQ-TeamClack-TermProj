using System.IO;
using System.Windows;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for admin_LogFile.xaml
    /// </summary>
    public partial class admin_LogFile : Window
    {
        private User localUser;

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