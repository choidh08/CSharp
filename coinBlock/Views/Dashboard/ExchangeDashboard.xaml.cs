using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Charts;
using System.Data;
using System.Collections.ObjectModel;
using coinBlock.Model.Dashboard;
using coinBlock.Model;
using coinBlock.Utility;
using DevExpress.Mvvm;
using System.Threading.Tasks;

namespace coinBlock.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for DashboardTrading.xaml
    /// </summary>
    public partial class ExchangeDashboard : UserControl
    {
        System.Windows.Forms.WebBrowser web = new System.Windows.Forms.WebBrowser();
        string NowCoin = string.Empty;
        Dictionary<string, EnumLib.ExchangeCurrencyCode> NowDict;
        string sSelTime = string.Empty;

        int webWidth = 0;
        int webHeight = 0;

        public ExchangeDashboard()
        {
            InitializeComponent();
            web.ScriptErrorsSuppressed = true;
            wfh.Child = web;
            web.ScrollBarsEnabled = false;
            web.Resize += Web_Resize;

            Messenger.Default.Register<Dictionary<string, Dictionary<string, EnumLib.ExchangeCurrencyCode>>>(this, ShowChart);
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

        public void UrlCall(Dictionary<string, EnumLib.ExchangeCurrencyCode> Temp, bool resize = false)
        {
            try
            {
                if (Temp == null)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(StringEnum.GetStringValue(Temp.Values.First())))
                {
                    NowCoin = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BTC);
                }

                NowCoin = StringEnum.GetStringValue(Temp.Values.First());
                NowDict = Temp;

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
                    string Url = ConfigLib.WebUrl + "bt.chartInit.dp/proc.go?headViewYn=N&coinInfo=" + NowCoin + "&height=" + webHeight + "px&width=" + webWidth + "px&marketCd=" + marketCd;
                    //string Url = ConfigLib.WebUrl + "bt.chartInit.dp/proc.go?headViewYn=N&marketCd=" + marketCd + "&coinInfo = " + NowCoin + "&width=" + webWidth + "px&height=" + webHeight + "px";
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
    }
}
