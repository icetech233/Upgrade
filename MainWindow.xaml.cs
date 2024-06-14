using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
using Upgrade.Core;

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
            taskUpdateInfo = jxUpdate.UpdateInfoAsync();
            GetAppInfo();
        }
        Task taskUpdateInfo;
        bool isExistsJxToolExe = false;
        string currentDirectory;
        const string jxExeFileName = "JXCommunication.exe";
        JxUpdate jxUpdate = new JxUpdate();

        private void GetAppInfo()
        {
            // 获取当前程序执行的目录
            currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string jxExePath = System.IO.Path.Combine(currentDirectory, jxExeFileName);
            // 
            if (System.IO.File.Exists(jxExePath))
            {
                isExistsJxToolExe = true;
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(jxExePath);
                // ProductName	"玖欣网关助手"   string
                // ProductVersion  "1.0.0.1"   string
                var productName = versionInfo.ProductName;
                var productVersion = versionInfo.ProductVersion;
                //(int major, int minor, int build, int revision)
                Version v1 = Version.Parse(productVersion);

            }

            //
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
            this.WindowState = WindowState.Minimized;
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Environment.Exit(0);
        }

        private void UpdateApp_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;

            taskUpdateInfo.Wait();
            // 正式业务
            Version serverVersion = jxUpdate.ServerVersion;
            var safeDownloadUrl = jxUpdate.SafeDownloadUrl;
            //serverVersion 转成 string

            // 下载zip 到 temp 解压文件
            // 检查程序 是否启动 有强制关闭

            // 删除旧版文件
            try
            {
                downloadZip();

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }finally
            {
                button.IsEnabled = true;
            }
        }


        private void downloadZip() 
        {
            HttpClient client = new HttpClient();

            client.Dispose();
        }
    }
}
