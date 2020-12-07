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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// This functions as the backup screen for the admin menu.
    /// </summary>
    public partial class admin_Backup : Page
    {
        /*!
         * \brief The constructor for the backup screen.
         * \details Constructs the backup screen and sets some of the textblock values to display the appropiate variables.
        */
        public admin_Backup()
        {
            InitializeComponent();
            DBMS_IP.Text = "IP: " + SQ_TeamClack_TermProj.MainWindow.selectedIP;
            DBMS_PORT.Text = "PORT: " + SQ_TeamClack_TermProj.MainWindow.selectedPort;
            LogFileBlock.Text = SQ_TeamClack_TermProj.MainWindow.logfileLocation;
        }

        /*!
         * \brief This allows for opening a save dialog box for the log file. 
         * \details When the user (Admin) selects this button with this function they will be able to save and set a new log txt file to save all logs to.
        */
        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog logFile = new SaveFileDialog();
            logFile.Filter = "Text file(*.txt)|*.txt|All Files (*.*)|*.*";
            if (logFile.ShowDialog() == true)
            {
                File.WriteAllText(logFile.FileName, " ");
                LogFileBlock.Text = logFile.FileName;
                SQ_TeamClack_TermProj.MainWindow.logfileLocation = logFile.FileName;
            }
        }

        /*!
         * \brief This allows for opening the ipconfiguration window.
         * \details When the user (Admin) selects the button with this function they will be able to configure 
         *  the ip and port using the ipconfiguration window which will pop up.
        */
        private void ConfigButton_Click(object sender, RoutedEventArgs e)
        {
            SQ_TeamClack_TermProj.admin_IPConfiguration ipconfig = new SQ_TeamClack_TermProj.admin_IPConfiguration();
            ipconfig.ShowDialog();
            DBMS_IP.Text = "IP: " + SQ_TeamClack_TermProj.MainWindow.selectedIP;
            DBMS_PORT.Text = "PORT: " + SQ_TeamClack_TermProj.MainWindow.selectedPort;


        }

        /*!
         * \brief This allows for viewing the contents of the log file.
         * \details When the user (Admin) selects the button with this function a window will open which contains the contents of the log function.
        */
        private void OpenLogButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SQ_TeamClack_TermProj.admin_LogFile log = new SQ_TeamClack_TermProj.admin_LogFile();
                log.ShowDialog();
            }
            catch (Exception)
            {
                LogFileBlock.Text = " Error: No Log File Created/Selected";
            }
        }
    }
}
