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
using System.IO;

namespace SQ_TeamClack_TermProj
{
    /// <summary>
    /// This file serves as the window for reading the contents of the log file.
    /// </summary>
    public partial class admin_LogFile : Window
    {
        public admin_LogFile()
        {
            InitializeComponent();
            StreamReader readFile = new StreamReader(File.OpenRead(SQ_TeamClack_TermProj.MainWindow.logfileLocation));
            LogFileDisplayBlock.Text = readFile.ReadToEnd();
            readFile.Dispose();
        }
    }
}
