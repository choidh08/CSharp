using coinBlock.Utility;
using coinBlock.ViewModels;
using System;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace coinBlock
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            try
            {
                //    //콜손 프로그램으로 설정하고 사용
                //    //콘솔 디버그 출력용
                //    TextWriterTraceListener textWriterTraceListener = new TextWriterTraceListener(Console.Out);
                //    Debug.Listeners.Add(textWriterTraceListener);

                //IniFileLib ini = new IniFileLib();
                //string Lv = ini.GetCheckID("Language", "Country");
                //if (!string.IsNullOrWhiteSpace(Lv))
                //{
                //    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Lv);
                //}
                //else
                //{
                //    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                //}

                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ko-KR");
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }       

        Mutex mutex;
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                //중복 실행 방지
                string mutexName = Assembly.GetExecutingAssembly().GetName().Name;
                mutex = new Mutex(false, mutexName);

                if (mutex.WaitOne(0, false))
                {
                    base.OnStartup(e);
                }
                else
                {
                    MessageBox.Show(Localization.Resource.Common_Alert_5);
                    Application.Current.Shutdown();
                }
            }
            catch (Exception)
            {
                Application.Current.Shutdown();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                SysLog.Info("Exit");
                if (MainViewModel.mQClient != null)
                {
                    MainViewModel.mQClient.ClientClose();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                SysLog.Info("StartUp");
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}
