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
using System.Data;
using System.Collections.ObjectModel;
using coinBlock.Model.Dashboard;
using coinBlock.Model;
using coinBlock.Utility;
using System.Threading.Tasks;
using System.Drawing;
using coinBlock.Model.Common;
using coinBlock.ViewModels;
using DevExpress.Mvvm.POCO;

namespace coinBlock.Views.Dashboard
{
    /// <summary>
    /// Interaction logic for ExchangeChartDashboard.xaml
    /// </summary>
    public partial class ExchangeChartDashboard : Window
    {
        static bool Isload = false;
        static System.Windows.Forms.WebBrowser web;
        string NowCoin = string.Empty;
        static string NowPriceType = CommonLib.StandardCurcyNm;
        static string sSelTime = StringEnum.GetStringValue(EnumLib.GraphTime.min1);
        public ObservableCollection<btnInfo> btnList;
        static int webWidth = 0;
        static int webHeight = 0;

        public ExchangeChartDashboard()
        {
            ExchangeChartDashboard_Sub(string.Empty, string.Empty, string.Empty);
        }
        public ExchangeChartDashboard(string Coin)
        {
            ExchangeChartDashboard_Sub(Coin, string.Empty, string.Empty);
        }
        public ExchangeChartDashboard(string Coin, string PriceType, string PriceCode)
        {
            ExchangeChartDashboard_Sub(Coin, PriceType, PriceCode);
        }
        public void ExchangeChartDashboard_Sub(string Coin, string PriceType, string PriceCode)
        {
            InitializeComponent();

            web = new System.Windows.Forms.WebBrowser();
            this.Loaded += ExchangeChartDashboard_Loaded;
            this.Closing += ExchangeChartDashboard_Closing;

            web.ScriptErrorsSuppressed = true;
            wfh.Child = web;
            web.DocumentCompleted += web_DocumentCompleted;
            web.Resize += Web_Resize;

            NowCoin = Coin;
            NowPriceType = PriceCode;
            txtPulbic.Text = PriceType;

            btnList = new ObservableCollection<btnInfo>();

            if (PriceType.Equals(CommonLib.StandardCurcyNm))
            {
                foreach (ResponseGetMkCoinListListtModel item in MainViewModel.CoinList.KRW)
                {
                    btnList.Add(ViewModelSource.Create(() => new btnInfo(item.curcyNm, item.curcyCd)));                    
                }
            }
            else if (PriceType.Equals("ETH"))
            {
                foreach (ResponseGetMkCoinListListtModel item in MainViewModel.CoinList.ETH)
                {
                    btnList.Add(ViewModelSource.Create(() => new btnInfo(item.curcyNm, item.curcyCd)));
                }
            }
            else if (PriceType.Equals("USDT"))
            {
                foreach (ResponseGetMkCoinListListtModel item in MainViewModel.CoinList.USDT)
                {
                    btnList.Add(ViewModelSource.Create(() => new btnInfo(item.curcyNm, item.curcyCd)));
                }
            } 
            else if (PriceType.Equals("BTC"))
            {
                foreach (ResponseGetMkCoinListListtModel item in MainViewModel.CoinList.BTC)
                {
                    btnList.Add(ViewModelSource.Create(() => new btnInfo(item.curcyNm, item.curcyCd)));
                }
            }

            itemBtnList.ItemsSource = btnList;

            foreach (btnInfo item in btnList.ToList())
            {
                item.Cancel();

                if (item.curcyNm.Equals(StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(NowCoin).ToString()))
                {
                    item.Select();
                }
            }
        }

        private void ExchangeChartDashboard_Loaded(object sender, RoutedEventArgs e)
        {   
            try
            {
                Isload = true;
                UrlCall(NowCoin, NowPriceType);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void ExchangeChartDashboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Isload = false;
        }

        private void Web_Resize(object sender, EventArgs e)
        {
            try
            {
                UrlCall(NowCoin, NowPriceType, true);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public static void UrlCall(string ExchangeCode)
        {
            try
            {
                UrlCall(ExchangeCode, NowPriceType);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public static void UrlCall(string coinInfo, string priceType, bool resize = false)
        {
            try
            {
                if (Isload)
                {
                    NowPriceType = priceType;
                    if (string.IsNullOrWhiteSpace(coinInfo))
                    {
                        coinInfo = StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BTC);
                    }

                    if (resize)
                    {
                        if (20 > Math.Abs(webWidth - web.Width))
                        {
                            return;
                        }
                    }

                    webWidth = web.Width;
                    webHeight = web.Height + 160;
                
                    string marketCd = NowPriceType;

                    if (web.Url == null)
                    {
                        string Url = ConfigLib.WebUrl + "bt.chartViewHts.dp/proc.go?headViewYn=N&coinInfo=" + coinInfo + "&height=" + webHeight + "px&width=" + webWidth + "px&marketCd=" + marketCd;
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
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void web_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            web.Document.Body.Style = "zoom:100%; overflow-x:hidden; overflow-y:hidden;";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach(btnInfo item in btnList.ToList())
                {
                    item.Cancel();

                    if (item.curcyNm.Equals(((ExchangeChartDashboard.btnInfo)((Button)sender).DataContext).curcyNm))
                    {
                        item.Select();
                        UrlCall(item.curcyCd);
                    }
                }             
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public class btnInfo
        {            
            public virtual string ImgPath { get; set; }            
            public virtual string curcyNm { get; set; }
            public virtual string curcyCd { get; set; }
            public virtual string width { get; set; }
            public virtual string height { get; set; } = "20";

            public btnInfo(string sCurcyNm, string sCurcyCd)
            {
                try
                {
                    curcyNm = sCurcyNm;
                    curcyCd = sCurcyCd;

                    widthInit();
                }
                catch (Exception ex)
                {
                    SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                }
            }

            public void widthInit()
            {
                try
                {
                    ImgPath = "/Images/btn_graph_" + curcyNm + ".png";                   
                    BitmapImage bitt = new BitmapImage();
                    bitt.BeginInit();
                    bitt.UriSource = new Uri(string.Format("pack://application:,,,/Images/btn_graph_{0}.png", curcyNm));
                    bitt.EndInit();

                    width = bitt.PixelWidth.ToString();
                }
                catch (Exception ex)
                {
                    SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                }
            }

            public void Select()
            {
                ImgPath = "/Images/btn_graph_" + curcyNm + "_on.png";                
            }
            public void Cancel()
            {
                ImgPath = "/Images/btn_graph_" + curcyNm + ".png";
            }
        }
    }
}
