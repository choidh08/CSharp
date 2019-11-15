using coinBlock.Utility;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace coinBlock.ViewModels
{
    [POCOViewModel]
    public class MainWindowViewModel
    {
        public static MainWindowViewModel Create()
        {
            return ViewModelSource.Create(() => new MainWindowViewModel());
        }

        public MainWindowViewModel()
        {
            //App.Current.MainWindow.WindowState = System.Windows.WindowState.Normal;
        }

        public void Load()
        {
            //SetNetDrive();
            //SetLog();
        }

        /// <summary>
        /// 네트워크 드라이브 연결 설정
        /// </summary>
        void SetNetDrive()
        {
            NetDriveLib nd = new NetDriveLib();
            nd.ConnectNetworkDrive("z:", @"\\qnap.cmesoft.co.kr\Log", "Log", "log!");
        }

        /// <summary>
        /// Log 파일 위치 네크워크 드라이브로 설정
        /// </summary>
        void SetLog()
        {
            LogManager.Configuration.Variables["basedir"] = "z:";
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            //uiLanding.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //base.OnMouseLeftButtonDown(e);
            App.Current.MainWindow.DragMove();
        }

        private void uiMin_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void uiMax_Click(object sender, RoutedEventArgs e)
        {
            if (App.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                App.Current.MainWindow.WindowState = WindowState.Normal;
            }
            else
            {
                App.Current.MainWindow.WindowState = WindowState.Maximized;
            }
        }

        private void uiExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Close();
        }
    }
}
