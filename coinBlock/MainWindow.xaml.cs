using coinBlock.Model;
using coinBlock.Utility;
using coinBlock.Views;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using coinBlock.ViewModels;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Mvvm;

namespace coinBlock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ////RabbitMQClient rc = new RabbitMQClient();
                //wb1.Navigate("https://www.ibk.co.kr/member/mainLogin.ibk?cert=cert&logintype=BANK&returnURL=https%3A%2F%2Fmybank.ibk.co.kr%2Fuib%2Fjsp%2Fonline%2Finq%2Finq10%2Finq1010%2FCINQ101000_i.jsp");
                //wb2.Navigate("https://www.kbstar.com/");
                //MainBorder.Margin = new Thickness(5); 

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (MainViewModel.mQClient != null)
            {
                MainViewModel.mQClient.ClientClose();
            }
            e.Cancel = false;
        }

        /// <summary>
        /// 네트워크 드라이브 연결 설정
        /// </summary>
        void SetNetDrive()
        {
            //NetDriveLib nd = new NetDriveLib();
            //nd.ConnectNetworkDrive("z:", @"\\qnap.cmesoft.co.kr\Log", "Log", "log!");
        }

        /// <summary>
        /// Log 파일 위치 네크워크 드라이브로 설정
        /// </summary>
        void SetLog()
        {
            //LogManager.Configuration.Variables["basedir"] = "z:";
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            try
            {
                uiLanding.Visibility = Visibility.Collapsed; string str = WebApiLib.MaintenanceCheck();
                if (str != null)
                {
                    Messenger.Default.Send("Maintenance");

                    string[] st = str.Split('|');

                    //st[0]:점검내용(한글)
                    //st[1]:점검내용(영문)
                    //st[2]:점검시간
                    //st[3]:공지사항 창 width
                    //st[4]:공지사항 창 height

                    if (LoginViewModel.LanguagePack.IndexOf("ko") > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        //예시
                        //sb.AppendLine("긴급 패치 보안 업데이트로 인한 시스템 점검 중입니다.");
                        //sb.AppendLine("일시 : 2019.01.11 23:00 ~ 2019.01.12 02:00");
                        //Alert alert = new Alert(sb.ToString(), 400, 150);
                        //alert.ShowDialog();

                        sb.AppendLine(st[0]);
                        sb.AppendLine("일시 : " + st[2]);

                        Alert alert = new Alert(sb.ToString(), int.Parse(st[3]), int.Parse(st[4]));
                        alert.ShowDialog();
                        return;
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine(st[1]);
                        sb.AppendLine("Schedule: " + st[2]);

                        Alert alert = new Alert(sb.ToString(), int.Parse(st[3]), int.Parse(st[4]));
                        alert.ShowDialog();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ClickCount.Equals(1))
                {
                    base.OnMouseLeftButtonDown(e);
                    this.DragMove();
                }
                else if (e.ClickCount.Equals(2))
                {
                    System.Windows.Controls.Image imgNorMax = (System.Windows.Controls.Image)uiMax.Template.FindName("btnNorMax", uiMax);


                    if (this.WindowState == WindowState.Maximized)
                    {
                        imgNorMax.Source = new BitmapImage(new Uri("Images/btn_header_maximize.png", UriKind.Relative));
                        this.ResizeMode = ResizeMode.CanResize;
                        this.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        imgNorMax.Source = new BitmapImage(new Uri("Images/btn_header_restore.png", UriKind.Relative));

                        //현재 모니터 SIZE 정보 가져오기
                        Rectangle aa = new Rectangle((int)this.Left, (int)this.Top, (int)this.Width, (int)this.Height);
                        Screen scr = Screen.FromRectangle(aa);

                        var ratio = Math.Max(Screen.PrimaryScreen.WorkingArea.Width / SystemParameters.PrimaryScreenWidth, Screen.PrimaryScreen.WorkingArea.Height / SystemParameters.PrimaryScreenHeight);

                        //최대 SIZE 설정 및 위치 설정
                        this.ResizeMode = ResizeMode.NoResize;
                        this.Left = scr.WorkingArea.Left / ratio;
                        this.Top = scr.WorkingArea.Top / ratio;
                        this.MaxWidth = scr.WorkingArea.Width / ratio;
                        this.MaxHeight = scr.WorkingArea.Height / ratio;

                        this.WindowState = WindowState.Maximized;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void uiMin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void uiMax_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Controls.Image imgNorMax = (System.Windows.Controls.Image)uiMax.Template.FindName("btnNorMax", uiMax);

                if (this.WindowState == WindowState.Maximized)
                {
                    imgNorMax.Source = new BitmapImage(new Uri("Images/btn_header_maximize.png", UriKind.Relative));
                    this.ResizeMode = ResizeMode.CanResize;
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    imgNorMax.Source = new BitmapImage(new Uri("Images/btn_header_restore.png", UriKind.Relative));

                    //현재 모니터 SIZE 정보 가져오기
                    Rectangle aa = new Rectangle((int)this.Left, (int)this.Top, (int)this.Width, (int)this.Height);
                    Screen scr = Screen.FromRectangle(aa);

                    var ratio = Math.Max(Screen.PrimaryScreen.WorkingArea.Width / SystemParameters.PrimaryScreenWidth, Screen.PrimaryScreen.WorkingArea.Height / SystemParameters.PrimaryScreenHeight);

                    //최대 SIZE 설정 및 위치 설정
                    this.ResizeMode = ResizeMode.NoResize;
                    this.Left = scr.WorkingArea.Left / ratio;
                    this.Top = scr.WorkingArea.Top / ratio;
                    this.MaxWidth = scr.WorkingArea.Width / ratio;
                    this.MaxHeight = scr.WorkingArea.Height / ratio;

                    this.WindowState = WindowState.Maximized;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void uiExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Alert alert = new Alert(Localization.Resource.Login_3, Alert.ButtonType.YesNo);
                    if (alert.ShowDialog() == true)
                    {
                        App.Current.Shutdown();
                    }
                }));
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private Screen GetScreen()
        {
            try
            {
                var aa = new System.Drawing.Rectangle((int)this.Left, (int)this.Top, (int)this.Width, (int)this.Height);
                return Screen.FromRectangle(aa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}