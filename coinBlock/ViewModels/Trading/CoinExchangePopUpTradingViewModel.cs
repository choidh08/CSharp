using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Model.Trading;
using coinBlock.Utility;
using coinBlock.Model;
using coinBlock.Views;

namespace coinBlock.ViewModels
{
    [POCOViewModel]
    public class CoinExchangePopUpTradingViewModel
    {
        public virtual ICurrentWindowService WindowService { get { return null; } }
        public virtual ResponseTradingCoinAutoTradeSelDataModel CoinAutoTradeContent { get; set; }
        public virtual string SelCoin { get; set; }
        public virtual string ChgCoin { get; set; }

        public virtual string img_text_coin_change_alert { get; set; }
        public virtual string btn_popup_menu_move { get; set; }
        public virtual string btn_popup_menu_move_on { get; set; }
        public virtual string btn_popup_trade_now { get; set; }
        public virtual string btn_popup_trade_now_on { get; set; }

        public virtual string nowTime { get; set; }

        public static CoinExchangePopUpTradingViewModel Create()
        {
            return ViewModelSource.Create(() => new CoinExchangePopUpTradingViewModel());
        }

        public CoinExchangePopUpTradingViewModel()
        {

        }

        public void Loaded()
        {
            try
            {
                ImageSet();
                GetAutoCoinSetting();
                nowTime = CommonLib.GetTime;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        //초기화
        public async void GetAutoCoinSetting()
        {
            try
            {
                using (RequestTradingCoinAutoTradeSelModel req = new RequestTradingCoinAutoTradeSelModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseTradingCoinAutoTradeSelModel res = await WebApiLib.AsyncCall<ResponseTradingCoinAutoTradeSelModel, RequestTradingCoinAutoTradeSelModel>(req))
                    {
                        ResponseTradingCoinAutoTradeSelDataModel catTemp = res.data;

                        SelCoin = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(catTemp.selCnKndCd).ToString();
                        ChgCoin = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(catTemp.chgCnKndCd).ToString();
                     
                        CoinAutoTradeContent = catTemp;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        //즉시거래
        public void CmdExecuteNowTrade()
        {
            try
            {
                decimal? OrdAmt;

                using (RequestTradingCoinToCoinBuyModel req = new RequestTradingCoinToCoinBuyModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.curcyCd = CoinAutoTradeContent.chgCnKndCd;  //구매하려는 통화
                    req.payCurcyCd = CoinAutoTradeContent.selCnKndCd; //결제하려는 통화

                    using (ResponseTradingCoinToCoinBuyModel res = WebApiLib.SyncCall<ResponseTradingCoinToCoinBuyModel, RequestTradingCoinToCoinBuyModel>(req))
                    {
                        OrdAmt = res.data.cnAmt; //코인 수량
                    }
                }

                if (OrdAmt < CoinAutoTradeContent.selAmt)
                {
                    Alert alert = new Alert(Localization.Resource.CoinTrading_Alert_21, 330);
                    alert.ShowDialog();
                    return;
                }

                using (RequestTradingCoinSwapModel req = new RequestTradingCoinSwapModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;//계정정보
                    req.buyCurcyCd = CoinAutoTradeContent.chgCnKndCd;//수령코인
                    req.payCurcyCd = CoinAutoTradeContent.selCnKndCd;//주문코인                           
                    req.phsAmt = CoinAutoTradeContent.selAmt.ToString();
                    req.regIp = MainViewModel.LoginDataModel.regIp;

                    using (ResponseTradingCoinSwapModel res = WebApiLib.SyncCall<ResponseTradingCoinSwapModel, RequestTradingCoinSwapModel>(req))
                    {
                        Messenger.Default.Send("AssetsRefresh");
                        Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        //메뉴이동
        public void CmdMoveMenu()
        {
            try
            {
                Messenger.Default.Send(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_2_5"), Id = "CoinExchangeReservationTrading", IconPath = "/Images/ico_nav_coin_chage_auto.png", Certify = 1 });
                WindowService.Close();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        //코인교환 설정 초기화
        public async void Clear()
        {
            try
            {
                using (RequestTradingCoinAutoTradeDelModel req = new RequestTradingCoinAutoTradeDelModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    using (ResponseTradingCoinAutoTradeDelModel res = await WebApiLib.AsyncCall<ResponseTradingCoinAutoTradeDelModel, RequestTradingCoinAutoTradeDelModel>(req))
                    {
                        //삭제까지 됬으면 팝업 닫기.
                        WindowService.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ImageSet()
        {
            try
            {
                string sLanguage = LoginViewModel.LanguagePack;

                img_text_coin_change_alert = sLanguage + "img_text_coin_change_alert.png";
                btn_popup_menu_move = sLanguage + "btn_popup_menu_move.png";
                btn_popup_menu_move_on = sLanguage + "btn_popup_menu_move_on.png";
                btn_popup_trade_now = sLanguage + "btn_popup_trade_now.png";
                btn_popup_trade_now_on = sLanguage + "btn_popup_trade_now_on.png";
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}