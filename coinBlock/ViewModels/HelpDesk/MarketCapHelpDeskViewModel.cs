using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Model;
using coinBlock.Utility;
using System.Collections.ObjectModel;
using coinBlock.Model.HelpDesk;
using System.Windows.Threading;

namespace coinBlock.ViewModels
{
    [POCOViewModel]
    public class MarketCapHelpDeskViewModel
    {
        public virtual string SC { get; } = CommonLib.StandardCurcyNm;
        public virtual ObservableCollection<MarketCapData> Market { get; set; }
        DispatcherTimer RepeatTimer;

        protected MarketCapHelpDeskViewModel()
        {
            RepeatTimer = new DispatcherTimer();
            RepeatTimer.Tick += RepeatTimer_Tick;
            RepeatTimer.Interval = new TimeSpan(0, 1, 0);
            RepeatTimer.Start();
        }    

        public static MarketCapHelpDeskViewModel Create()
        {
            return ViewModelSource.Create(() => new MarketCapHelpDeskViewModel());
        }

        public void Loaded()
        {
            try
            {
                GetData();
                RepeatTimer.Start();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        
        public void UnLoaded()
        {
            try
            {
                RepeatTimer.Stop();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetData()
        {
            try
            {
                using (RequestGetMarketCapModel req = new RequestGetMarketCapModel())
                {
                    using (ResponseGetMarketCapModel res = await WebApiLib.AsyncCall<ResponseGetMarketCapModel, RequestGetMarketCapModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            Market = new ObservableCollection<MarketCapData>();
                            string UpDown = string.Empty;
                            string Color = string.Empty;
                            int count = 1;
                            foreach(ResponseGetMarketCapListModel item in res.data.list)
                            {
                                if(item.change > 0)
                                {
                                    UpDown = "▲";
                                    Color = "#FF0000";
                                }
                                else
                                {
                                    UpDown = "▼";
                                    Color = "#0000FF";
                                }

                                Market.Add(ViewModelSource.Create(() => new MarketCapData { Num = count, imgPath = "/Images/img_ex_" + item.symbol + ".png", CoinName = item.coinName, MarketCap = item.marketCap, Price = item.price, Volume =item.volume, CalculCoin = item.cirSupply, ChgPrice = item.change.ToString()+"% "+UpDown, ChgPriceColor = Color }));

                                count++;
                            }
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void RepeatTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                GetData();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }

    public class MarketCapData
    {
        public virtual int Num { get; set; }
        public virtual string imgPath { get; set; }
        public virtual string CoinName { get; set; }
        public virtual decimal MarketCap { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal Volume { get; set; }
        public virtual decimal CalculCoin { get; set; }
        public virtual string ChgPrice { get; set; }
        public virtual string ChgPriceColor { get; set; }
    }
}