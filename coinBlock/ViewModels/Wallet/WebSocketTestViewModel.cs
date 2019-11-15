using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.POCO;
using System.Linq;
using coinBlock.Model;
using coinBlock.Utility;
using System.Windows;
using System.Windows.Threading;
using System.Collections.Generic;
using coinBlock.ViewModels.Trading;
using coinBlock.Model.Trading;
using coinBlock.Views.Dashboard;
using System.Threading.Tasks;
using coinBlock.Views;
using coinBlock.Model.WebSocket;
using DevExpress.Mvvm.UI;
using System.ComponentModel;
using coinBlock.Model.Common;

namespace coinBlock.ViewModels.Wallet
{
    [POCOViewModel]
    public class WebSocketTestViewModel
    {
        protected IDialogService DialogService { get { return this.GetService<IDialogService>(); } }

        public virtual string AAA { get; set; } = "init";
        public virtual string BtcData { get; set; } = "init";
        public virtual string EthData { get; set; } = "init";
        
        public virtual decimal value { get; set; } = Convert.ToDecimal(111.123);

        public virtual string floatformat { get; set; } = "n1";

        public static WebSocketTestViewModel Create()
        {
            return ViewModelSource.Create(() => new WebSocketTestViewModel());
        }

        public WebSocketTestViewModel()
        {
            
        }

        public void Loaded()
        {
            
        }

        public void WebAddressChg()
        {   
        }

        public virtual ObservableCollection<ResponseTradingNotConListModel> NotConList { get; set; }
        public virtual void Cancel()
        {
            //try
            //{
            //    List<string> userList = new List<string>();
            //    userList.Add("banbanguy@naver.com");
            //    userList.Add("nodo1017@gmail.com");
            //    userList.Add("visop@naver.com");
            //    userList.Add("z01lkss@naver.com");
            //    userList.Add("jbd76@naver.com");


            //    var enumValList = StringEnum.EnumToList<EnumLib.ExchangeCurrencyCode>().ToList();
            //    foreach (var coin in enumValList)
            //    {
            //        EnumLib.ExchangeCurrencyCode KindOfCoin = coin;

            //        foreach (string uuu in userList)
            //        {
            //            using (RequestTradingNotConModel req = new RequestTradingNotConModel())
            //            {
            //                req.userEmail = uuu;//MainViewModel.LoginDataModel.userEmail;
            //                req.curcyCd = StringEnum.GetStringValue(KindOfCoin);
            //                req.listSize = 8000;

            //                using (ResponseTradingNotConModel res = await WebApiLib.AsyncCall<ResponseTradingNotConModel, RequestTradingNotConModel>(req))
            //                {
            //                    AAA = uuu;
            //                    BtcData = res.data.list.Count.ToString();
            //                    EthData = "0";

            //                    foreach (ResponseTradingNotConListModel item in res.data.list)
            //                    {
            //                        //미체결 주문 취소 구현   
            //                        using (RequestTradingTradeCancelModel req2 = new RequestTradingTradeCancelModel())
            //                        {
            //                            req2.userEmail = uuu;//계정정보
            //                            req2.orderNo = item.orderNo; //주문번호
            //                            req2.regIp = MainViewModel.LoginDataModel.regIp;

            //                            using (ResponseTradingTradeCancelModel res2 = await WebApiLib.AsyncCall<ResponseTradingTradeCancelModel, RequestTradingTradeCancelModel>(req2))
            //                            {
            //                                EthData = (int.Parse(EthData) + 1).ToString();
            //                                System.Threading.Thread.Sleep(100);
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    MessageBox.Show("헐어얼 돌았냐?? 다돌았다");
            //}
            //catch (Exception ex)
            //{
            //    SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            //}
        }
    }
}