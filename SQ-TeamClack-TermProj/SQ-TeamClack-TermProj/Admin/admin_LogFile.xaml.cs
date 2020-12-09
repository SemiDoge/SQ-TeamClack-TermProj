using System.IO;
using System.Windows;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for admin_LogFile.xaml
    /// </summary>
    public partial class admin_LogFile : Window
    {
        //private User localUser;

        /*!
         * \brief CONSTRUCTOR - This constructor constructs the Table Config window.
         * \details This constructor constructs the Table Config page and sets the page's localUser to the localUser passed in as a parameter.
         * \param <b>void</b>
        */

        public admin_LogFile()
        {
            InitializeComponent();

            StreamReader readFile = new StreamReader(File.OpenRead(MainWindow.logfileLocation));
            LogFileDisplayBlock.Text = readFile.ReadToEnd();
            readFile.Dispose();
        }
    }
}