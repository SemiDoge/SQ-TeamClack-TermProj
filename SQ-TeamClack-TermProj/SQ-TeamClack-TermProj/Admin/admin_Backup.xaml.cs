using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// Interaction logic for admin_Backup.xaml
    /// </summary>
    public partial class admin_Backup : Page
    {
        private User localUser;

        /*!
         * \brief CONSTRUCTOR -
         * \details
         * \param localUser - <b>User</b> - This User object keeps track of all of the session data.
        */

        public admin_Backup(User localUser)
        {
            InitializeComponent();

            this.localUser = localUser;

            LogFileBlock.Text = SQ_TeamClack_TermProj.MainWindow.logfileLocation;
        }

        /*!
         * \brief
         * \details
         * \param param - <b>orderParams</b> - A object that contains all of the parameters of the order.
        */

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog logFile = new SaveFileDialog();
            logFile.Filter = "Text file(*.txt)|*.txt|All Files (*.*)|*.*";

            if (logFile.ShowDialog() == true)
            {
                LogFileBlock.Text = logFile.FileName;
                SQ_TeamClack_TermProj.MainWindow.logfileLocation = logFile.FileName;
            }
        }

        /*!
         * \brief
         * \details
         * \param param - <b>orderParams</b> - A object that contains all of the parameters of the order.
        */

        private void ConfigButton_Click(object sender, RoutedEventArgs e)
        {
            admin_IPConfig ipconfig = new admin_IPConfig(localUser);
            ipconfig.ShowDialog();
            DBMS_IP.Text = "IP: " + ipconfig.ResponseTextIP;
            DBMS_PORT.Text = "PORT: " + ipconfig.ResponseTextPort;
        }

        /*!
         * \brief
         * \details
         * \param param - <b>orderParams</b> - A object that contains all of the parameters of the order.
        */

        private void OpenLogButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                admin_LogFile log = new admin_LogFile(localUser);
                log.ShowDialog();
            }
            catch (Exception)
            {
                LogFileBlock.Text = "Error: No Log File Created.";
            }
        }

        /*!
         * \brief
         * \details
         * \param param - <b>orderParams</b> - A object that contains all of the parameters of the order.
        */

        private void BackupBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog logFile = new SaveFileDialog();
            logFile.Filter = "SQL file(*.sql)|*.sql|All Files (*.*)|*.*";
            logFile.Title = "Dump the database (.sql)";

            if (logFile.ShowDialog() == true)
            {
                dumpDB(logFile.FileName);
            }
        }

        /*!
         * \brief
         * \details
         * \param param - <b>orderParams</b> - A object that contains all of the parameters of the order.
        */

        private void dumpDB(string filePath)
        {
            // Connect to database
            string conStr = ConfigurationManager.ConnectionStrings[localUser.CONSTR].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(conStr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        try
                        {
                            cmd.Connection = connection;
                            connection.Open();
                            mb.ExportToFile(filePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }
    }
}