using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.ObjectModel;
using coinBlock.Model.DepositWithdraw;
using coinBlock.Utility;
using coinBlock.Model;
using System.Windows;
using System.Windows.Threading;
using coinBlock.Model.MyPage;
using coinBlock.Views;
using System.Collections.Generic;

namespace coinBlock.ViewModels.DepositWithdraw
{
    [POCOViewModel]
    public class DepositDepositWithdrawViewModel
    {
        DispatcherTimer RepeatTimer;

        public virtual ObservableCollection<ResponseCoinDepositListModel> CoinDepositList { get; set; }
        public virtual DepositInfo_All DepositList { get; set; }

        public virtual bool IsBusy { get; set; }
        bool AddrCheck = true;

        public static DepositDepositWithdrawViewModel Create()
        {
            return ViewModelSource.Create(() => new DepositDepositWithdrawViewModel());
        }
        protected DepositDepositWithdrawViewModel()
        {
            try
            {
                DepositList = new DepositInfo_All();
                foreach (ResponseCoinListModel item in MainViewModel.CoinData.list)
                {
                    DepositList.list.Add(ViewModelSource.Create(() => new DepositInfo(item)));
                }

                RepeatTimer = new DispatcherTimer();
                RepeatTimer.Tick += RepeatTimer_Tick;
                RepeatTimer.Interval = new TimeSpan(0, 5, 0);
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
                GetCoinAddressYn();
                SearchCoinDepositList();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void Loaded()
        {
            try
            {
                GetCoinAddressYn();
                SearchCoinDepositList();
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

        //전자지갑주소 유무
        public async void GetCoinAddressYn()
        {
            try
            {
                using (RequestCoinAddressTableModel req = new RequestCoinAddressTableModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    using (ResponseCoinAddressTableModel res = await WebApiLib.AsyncCall<ResponseCoinAddressTableModel, RequestCoinAddressTableModel>(req))
                    {
                        if (res != null)
                        {
                            foreach (var item in res.data.list)
                            {
                                foreach (DepositInfo itemDepo in DepositList.list)
                                {
                                    if (item.curcyCd.Equals(itemDepo.OrderCode))
                                    {
                                        if (!item.accNo.Equals(string.Empty))
                                        {
                                            itemDepo.Address = item.accNo;
                                            itemDepo.DestinationTag = item.destiTag;
                                            itemDepo.OutVisible = Visibility.Collapsed;
                                            itemDepo.InVisible = Visibility.Visible;
                                        }
                                        else
                                        {
                                            itemDepo.OutVisible = Visibility.Visible;
                                            itemDepo.InVisible = Visibility.Collapsed;
                                        }
                                    }
                                }
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
        /// <summary>
        /// 보유코인 갯수 조회
        /// </summary>
        public async void SearchCoinAssets()
        {
            try
            {
                RequestMainAssetModel requestMainAssetModel = new RequestMainAssetModel();
                requestMainAssetModel.userEmail = MainViewModel.LoginDataModel.userEmail;
                using (ResponseMainAssetModel res = await WebApiLib.AsyncCall<ResponseMainAssetModel, RequestMainAssetModel>(requestMainAssetModel))
                {
                    if (res != null)
                    {
                        foreach (var item in res.data.list)
                        {
                            foreach (DepositInfo itemDepo in DepositList.list)
                            {
                                if (item.curcyCd.Equals(itemDepo.OrderCode))
                                {
                                    itemDepo.Amt = item.totCoinAmt;
                                }
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
        /// <summary>
        /// 코인 입금 내역 조회
        /// </summary>
        public async void SearchCoinDepositList()
        {
            try
            {
                Messenger.Default.Send("AssetsRefresh");
                SearchCoinAssets();

                using (RequestCoinDepositModel req = new RequestCoinDepositModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseCoinDepositModel res = await WebApiLib.AsyncCall<ResponseCoinDepositModel, RequestCoinDepositModel>(req))
                    {
                        foreach(ResponseCoinDepositListModel item in res.data.list)
                        {
                            if (item.exFlag.Equals("LD"))
                            {
                                item.orderDesc = Localization.Resource.Lending_1_1;
                            }
                            else if (item.exFlag.Equals("X"))
                            {
                                item.orderDesc = Localization.Resource.TradingHistory_1_19;
                            }
                            else if (item.exFlag.Equals("D"))
                            {
                                item.orderDesc = Localization.Resource.TradingHistory_1_7;
                            }
                            else if (item.exFlag.Equals("E"))
                            {
                                item.orderDesc = Localization.Resource.TradingHistory_1_20;
                            }
                        }

                        CoinDepositList = res.data.list;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 전자지갑주소 생성
        /// </summary>
        public void CmdGetCoinAddress(DepositInfo di)
        {
            try
            {
                if (CommonLib.UserFuncCheck() == "Y")
                {
                    Alert alert = new Alert(Localization.Resource.Common_Alert_1);
                    alert.ShowDialog();
                    return;
                }

                if (AddrCheck)
                {
                    IsBusy = true;
                    AddrCheck = false;

                    using (RequestGetCoinAddressModel req = new RequestGetCoinAddressModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.curcyCd = di.OrderCode;
                        req.regIp = MainViewModel.LoginDataModel.regIp;

                        using (ResponseGetCoinAddressModel res = WebApiLib.SyncCall<ResponseGetCoinAddressModel, RequestGetCoinAddressModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                if (res.data.failCd.Equals(""))
                                {
                                    if (di.isTagY == Visibility.Visible)
                                    {
                                        di.DestinationTag = res.data.destiTag;
                                    }

                                    di.Address = res.data.accNo;
                                    di.OutVisible = Visibility.Collapsed;
                                    di.InVisible = Visibility.Visible;

                                    if (di.OrderName.Equals("BMC") || di.OrderName.Equals("SECRET") || di.OrderName.Equals("YOA") || di.OrderName.Equals("ADM"))
                                    {
                                        GetCoinAddressYn();
                                    }
                                }
                                else if (res.data.failCd.Equals("777"))
                                {
                                    Alert alert = new Alert(Localization.Resource.Common_Alert_1);
                                    alert.ShowDialog();
                                    return;
                                }

                                IsBusy = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                AddrCheck = true;
                IsBusy = false;
            }
        }

        public void CmdCopyAddress(string address)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(address))
                {
                    Clipboard.SetText(address);

                    Alert alert = new Alert(Localization.Resource.DepositDepositWithdraw_Alert_1);
                    alert.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }

    public class DepositInfo
    {
        public virtual string OrderName { get; set; }
        public virtual string OrderCode { get; set; }

        public virtual string Address { get; set; }
        public virtual string TagName { get; set; }
        public virtual string DestinationTag { get; set; }
        public virtual decimal Amt { get; set; }

        public virtual Visibility OutVisible { get; set; } = Visibility.Visible;
        public virtual Visibility InVisible { get; set; } = Visibility.Collapsed;

        public virtual string img_deposit { get; set; }
        public virtual string btn_ewallet { get; set; }
        public virtual string btn_ewallet_on { get; set; }
        public virtual string btn_address_copy { get; set; }
        public virtual string btn_address_copy_on { get; set; }
        public virtual string btn_address_copy_press { get; set; }

        public virtual Visibility isTagY { get; set; } = Visibility.Collapsed;
        public virtual Visibility isTagN { get; set; } = Visibility.Collapsed;

        public DepositInfo(ResponseCoinListModel item)
        {
            OrderName = item.CoinName;
            OrderCode = item.CoinCode;

            string sLanguage = LoginViewModel.LanguagePack;
            img_deposit = "/Images/img_deposit_" + OrderName + ".png";
            btn_ewallet = sLanguage + "btn_ewallet_" + OrderName + ".png";
            btn_ewallet_on = sLanguage + "btn_ewallet_" + OrderName + "_on.png";
            btn_address_copy = sLanguage + "btn_address_copy.png";
            btn_address_copy_on = sLanguage + "btn_address_copy_on.png";
            btn_address_copy_press = sLanguage + "btn_address_copy_press.png";

            if (item.IsTagYn.Equals("Y"))
            {
                isTagY = Visibility.Visible;
                if (OrderName.Equals("XRP"))
                {
                    TagName = "DestinationTag";
                }
                else if (OrderName.Equals("VSTC") || OrderName.Equals("XEM"))
                {
                    TagName = "Message";
                }
                else if(OrderName.Equals("XLM") || OrderName.Equals("EOS"))
                {
                    TagName = "Memo";
                }
            }
            else
            {
                isTagN = Visibility.Visible;
            }
        }
    }
    public class DepositInfo_All
    {
        public virtual List<DepositInfo> list { get; set; }
        public DepositInfo_All()
        {
            list = new List<DepositInfo>();
        }
    }
}