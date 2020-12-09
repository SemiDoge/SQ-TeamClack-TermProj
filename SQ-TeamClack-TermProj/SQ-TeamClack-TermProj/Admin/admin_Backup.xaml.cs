using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.IO;
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
         * \brief CONSTRUCTOR - This constructor constructs the admin backup page.
         * \details This constructor constructs the Table Config page and sets the page's localUser to the localUser passed in as a parameter.
         * \param <b>void</b>
        */

        public admin_Backup(User localUser)
        {
            InitializeComponent();

            this.localUser = localUser;

            LogFileBlock.Text = MainWindow.logfileLocation;
            File.AppendAllText(@"Log\Log.log", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Admin loaded backup page.\n");
        }

        /*!
         * \brief This handler handles when the user clicks the "..." button.
         * \details This handler allows for the user to navigate to the login page and logout.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog logFile = new OpenFileDialog();
            logFile.Filter = "Log file(*.log)|*.log";

            if (logFile.ShowDialog() == true)
            {
                LogFileBlock.Text = logFile.FileName;
                SQ_TeamClack_TermProj.MainWindow.logfileLocation = logFile.FileName;
            }
        }

        /*!
         * \brief This handler handles when the user clicks the "Configure" button.
         * \details This handler opens a window that asks if the admin wants to change the ip of the database.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void ConfigButton_Click(object sender, RoutedEventArgs e)
        {
            admin_IPConfig ipconfig = new admin_IPConfig();
            ipconfig.ShowDialog();
            DBMS_IP.Text = "IP: " + ipconfig.ResponseTextIP;
            DBMS_PORT.Text = "PORT: " + ipconfig.ResponseTextPort;
        }

        /*!
         * \brief This handler handles when the user clicks the "Open Log" button.
         * \details This handler opens up the log file.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
        */

        private void OpenLogButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                admin_LogFile log = new admin_LogFile();
                log.ShowDialog();
            }
            catch (Exception)
            {
                LogFileBlock.Text = "Error: No Log File Created.";
            }

            File.AppendAllText(@"Log\Log.log", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Admin opened log file.\n");
        }

        /*!
         * \brief This handler handles when the user clicks the "Initiate Backup" button.
         * \details This handler opens up a save dialog and asks the user where they would like to save their backup of the database.
         * \param sender <b>object</b>
         * \param e <b>RoutedEventArgs</b>
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

            File.AppendAllText(@"Log\Log.log", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ": Admin backed up database.\n");
        }

        /*!
         * \brief This method dumps the database to a file.
         * \details This method uses MySQLBackup to dump the database to an .sql file of the users choice.
         * \param filePath - <b>string</b> - A string that contains the file path that the user would like to save the database dump to.
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