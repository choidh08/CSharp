using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.POCO;
using coinBlock.Model.HelpDesk;
using coinBlock.Utility;
using coinBlock.Model;

namespace coinBlock.ViewModels
{
    [POCOViewModel]
    public class CoinInfoHelpDeskViewModel
    {
        public virtual ObservableCollection<clsCoinInfo> Coin { get; set; }
        public virtual string SelectedCode { get; set; }
        public virtual string sCoinName { get; set; }
        public virtual string sCoinImage { get; set; }
        public virtual string sGrid_0 { get; set; }
        public virtual string sGrid_1 { get; set; }
        public virtual string sGrid_2 { get; set; }
        public virtual string sGrid_3 { get; set; }
        public virtual string sGrid_4 { get; set; }
        public virtual string sGrid_5 { get; set; }
        public virtual string sCoinInfo { get; set; }
        public virtual string sCoinCharacter { get; set; }

        protected CoinInfoHelpDeskViewModel()
        {
            Loaded();
        }

        public static CoinInfoHelpDeskViewModel Create()
        {
            return ViewModelSource.Create(() => new CoinInfoHelpDeskViewModel());
        }

        public void Loaded()
        {

            try
            {
                Coin = new ObservableCollection<clsCoinInfo>();

                foreach (ResponseCoinListModel item in MainViewModel.CoinData.list)
                {
                    Coin.Add(ViewModelSource.Create(() => new clsCoinInfo { sName = item.CoinName, sCode = item.CoinCode, sBackColor = "#e5e5e5", sForeColor = "#333" }));
                }

                CmdSelect(Coin[0].sCode);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetData(string selCode)
        {
            try
            {
                using (RequestGetCoinInfoModel req = new RequestGetCoinInfoModel())
                {
                    req.langCd = LoginViewModel.LanguagePack.Split('/')[2].Split('-')[0];
                    req.cnKndCd = selCode;

                    using (ResponseGetCoinInfoModel res = await WebApiLib.AsyncCall<ResponseGetCoinInfoModel, RequestGetCoinInfoModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            ResponseGetCoinInfoDataModel item = res.data;
                            
                            sCoinName = item.title;
                            sCoinImage = "/Images/big_" + item.abb + ".png";
                            sGrid_0 = item.firstPushAmt;
                            sGrid_1 = item.pushMth;
                            sGrid_2 = item.mktTotPrc;
                            sGrid_3 = item.curTotAmt;
                            sGrid_4 = item.maxAmt;
                            sGrid_5 = item.pubUrl;
                            sCoinInfo = item.etc1;
                            sCoinCharacter = item.etc2;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdSelect(string code)
        {
            try
            {
                foreach (var item in Coin)
                {
                    if (item.sCode.Equals(code))
                    {
                        item.sBackColor = "#0090d5";
                        item.sForeColor = "#FFF";
                        GetData(item.sCode);
                    }
                    else
                    {
                        item.sBackColor = "#e5e5e5";
                        item.sForeColor = "#333";
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }

    public class clsCoinInfo
    {
        public virtual string sName { get; set; }

        public virtual string sCode { get; set; }

        public virtual string sBackColor { get; set; }

        public virtual string sForeColor { get; set; }
    }
}