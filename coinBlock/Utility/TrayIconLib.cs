using DevExpress.Mvvm;
using coinBlock.Model;
using coinBlock.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace coinBlock.Utility
{
    public class TrayIconLib
    {
        private System.Windows.Forms.NotifyIcon trayIcon;

        private static TrayIconLib _instance;

        public static TrayIconLib Instance()
        {
            if (_instance == null)
            {
                _instance = new TrayIconLib();
            }
            return _instance;
        }

        protected TrayIconLib()
        {
            try
            {
                //var myThread = new Thread(delegate ()
                //{
                    trayIcon = new System.Windows.Forms.NotifyIcon();
                    trayIcon.Icon = Properties.Resources.B_logo;                    
                    trayIcon.Click += m_notifyIcon_Click;
                    trayIcon.BalloonTipClicked += TrayIcon_BalloonTipClicked;
                    trayIcon.Visible = true;

                    //Application.Run();
                //});

                //myThread.SetApartmentState(ApartmentState.STA);
                //myThread.Start();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void TrayIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            //NotifyIcon tempTray = ((NotifyIcon)sender);

            //if (tempTray.Tag != null)
            //{
            //    if (tempTray.Tag.Equals("CMMC00000000685"))
            //    {
            //        Messenger.Default.Send(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_1"), Id = "NoticeHelpDesk", IconPath = "/Images/ico_nav_notice.png" });
            //    }
            //}
        }

        /// <summary>
        /// 트레이 아이콘 클릭시 창 크기 기본 크기 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_notifyIcon_Click(object sender, EventArgs e)
        {
            try
            {
                App.Current.MainWindow.WindowState = System.Windows.WindowState.Normal;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 풍선 알림 보여 주기
        /// </summary>
        /// <param name="message"></param>
        public void ShowMesssage(string message, string code)
        {
            try
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
                {
                    //trayIcon.BalloonTipTitle = "CoinBlock";
                    trayIcon.BalloonTipTitle = MainViewModel.LoginDataModel.title;
                    trayIcon.BalloonTipText = message;
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        trayIcon.Tag = code;
                    }
                    trayIcon.ShowBalloonTip(300);
                }));
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void Close()
        {
            try
            {
                trayIcon.Dispose();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}