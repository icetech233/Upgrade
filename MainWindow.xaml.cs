using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Upgrade
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetAppInfo();
        }

        private void GetAppInfo()
        {
            string filePath = @"E:\nanbo\Upgrade\bin\Debug\JXCommunication.exe";
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(filePath);

            // ProductName	"玖欣网关助手"    string
            // ProductVersion  "1.0.0.1"    string
            var productName = versionInfo.ProductName;
            var productVersion = versionInfo.ProductVersion;
            //(int major, int minor, int build, int revision)
            Version v1 = Version.Parse(productVersion);

            Console.WriteLine();
        }
        
        private void titleArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void MinWin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Environment.Exit(0);
        }
    }
}
