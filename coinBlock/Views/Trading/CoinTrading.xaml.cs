using coinBlock.Utility;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;

namespace coinBlock.Views.Trading
{
    /// <summary>
    /// Interaction logic for CoinTrading.xaml
    /// </summary>
    public partial class CoinTrading : UserControl
    {
        DispatcherTimer RepeatTimer;
        System.Windows.Forms.WebBrowser web = new System.Windows.Forms.WebBrowser();
        string NowCoin = string.Empty;
        Dictionary<string, EnumLib.ExchangeCurrencyCode> NowDict;
        string sSelTime = string.Empty;

        int webWidth = 0;
        int webHeight = 0;

        public CoinTrading()
        {
            InitializeComponent();
            web.ScriptErrorsSuppressed = true;
            wfh.Child = web;
            web.ScrollBarsEnabled = false;
            web.Resize += Web_Resize;

            RepeatTimer = new DispatcherTimer();
            RepeatTimer.Tick += RepeatTimer_Tick;
            RepeatTimer.Interval = new TimeSpan(0, 5, 0);

            Messenger.Default.Register<Dictionary<string, Dictionary<string, EnumLib.ExchangeCurrencyCode>>>(this, ShowChart);
        }

        private void RepeatTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.IsLoaded)
                {
                    UrlCall(NowDict);
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void Web_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.IsLoaded)
                {
                    UrlCall(NowDict, true);
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ShowChart(Dictionary<string, Dictionary<string, EnumLib.ExchangeCurrencyCode>> item)
        {
            try
            {
                foreach (KeyValuePair<string, Dictionary<string, EnumLib.ExchangeCurrencyCode>> temp in item)
                {
                    if (this.GetType().Name == temp.Key.ToString().Replace("ViewModel", ""))
                    {
                        UrlCall(temp.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        
        public void UrlCall(Dictionary<string, EnumLib.ExchangeCurrencyCode> Temp, bool resize = false)
        {
            try
            {
                if (Temp == null)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(StringEnum.GetStringValue(Temp.Values.First()))) return;

                NowDict = Temp;

                //string marketCd = StringEnum.GetStringValue((EnumLib.ExchangeCurrencyCode)Enum.Parse(typeof(EnumLib.ExchangeCurrencyCode), Temp.Keys.First().ToString()));
                string marketCd = Temp.Keys.First().ToString();
                string coinInfo = StringEnum.GetStringValue(Temp.Values.First());

                if (resize)
                {
                    if (20 > Math.Abs(webWidth - web.Width))
                    {
                        return;
                    }
                }

                webWidth = web.Width;
                webHeight = web.Height + 160;

                if (web.Url == null)
                {
                    string Url = ConfigLib.WebUrl + "bt.chartInit.dp/proc.go?headViewYn=N&coinInfo=" + StringEnum.GetStringValue(Temp.Values.First()) + "&height=" + webHeight + "px&width=" + webWidth + "px&marketCd=" + marketCd;
                    web.Navigate(Url);
                }
                else
                {
                    object[] param = new object[4];
                    param[0] = marketCd;
                    param[1] = coinInfo;
                    param[2] = webWidth;
                    param[3] = webHeight;

                    web.Document.InvokeScript("htsChart", param);
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void TableView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                if (e.Delta > 0)
                {
                    sv.LineUp();
                }
                else
                {
                    sv.LineDown();
                }
                e.Handled = true;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}
