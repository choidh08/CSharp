using coinBlock.Utility;
using DevExpress.Mvvm;
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
using System.Windows.Threading;

namespace coinBlock.Views
{
    /// <summary>
    /// CoinChart.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CoinChart : UserControl
    {
        DispatcherTimer RepeatTimer;

        System.Windows.Forms.WebBrowser web = new System.Windows.Forms.WebBrowser();
        Dictionary<string, EnumLib.ExchangeCurrencyCode> CoinType;

        bool isLoad = false;

        public CoinChart(Dictionary<string, EnumLib.ExchangeCurrencyCode> sCoinType)
        {
            InitializeComponent();
            CoinType = sCoinType;
            web.ScriptErrorsSuppressed = true;
            wfh.Child = web;
            web.ScrollBarsEnabled = false;
            web.Resize += Web_Resize;
            
            this.Loaded += CoinChart_Loaded;

            RepeatTimer = new DispatcherTimer();
            RepeatTimer.Tick += RepeatTimer_Tick;
            RepeatTimer.Interval = new TimeSpan(0, 5, 0);
        }

        private void RepeatTimer_Tick(object sender, EventArgs e)
        {
            if (this.IsLoaded)
            {
                UrlCall(CoinType);
            }
        }

        private void CoinChart_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isLoad)
            {
                isLoad = true;
                UrlCall(CoinType);
            }
        }

        private void Web_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.IsLoaded)
                {
                    UrlCall(CoinType, true);
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
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (string.IsNullOrWhiteSpace(StringEnum.GetStringValue(Temp.Values.First()))) return;

                    CoinType = Temp;

                    int height = web.Height;
                    int width = web.Width;

                    //string marketNm = Temp.Keys.First().ToString();
                    //string marketCd = StringEnum.GetStringValue((EnumLib.ExchangeCurrencyCode)Enum.Parse(typeof(EnumLib.ExchangeCurrencyCode), marketNm));
                    string marketCd = Temp.Keys.First().ToString();

                    string Url = ConfigLib.WebUrl + "bt.chartInit.dp/proc.go?headViewYn=N&coinInfo=" + StringEnum.GetStringValue(Temp.Values.First()) + "&height=" + height + "px&width=" + width + "px&marketCd=" + marketCd;
                    web.Navigate(Url);
                }));
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void web_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                web.Document.Body.Style = "zoom:100%; overflow-x:hidden; overflow-y:auto";
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void web_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F5)
            {
                e.IsInputKey = true;
            }
        }
    }
}
