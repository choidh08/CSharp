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
using coinBlock.Model.HelpDesk;
using coinBlock.Model.MyPage;
using coinBlock.Model.AdditionalService;
using coinBlock.Views.Common;
using System.Text;
using coinBlock.Model.Common;
using coinBlock.Views.AdditionalService;

namespace coinBlock.ViewModels
{
    [POCOViewModel]
    public class MainViewModel
    {
        public virtual IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }
        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }

        public virtual decimal TotAsset { get; set; } = 0;
        public virtual WsCoinPriceDataModel CoinPriceDataModel { get; set; }
        public static ResponseLoginDataModel LoginDataModel = null;
        public virtual ResponseMainAssetListModel UsdtAssetModel { get; set; }
        public virtual ResponseMainAssetListModel KrwAssetModel { get; set; }
        public virtual MenuModel SelectedMenu { get; set; }
        public ObservableCollection<MenuModel> Menus { get; set; }
        public ObservableCollection<MenuModel> QuickMenus { get; set; }

        public virtual string NoticeMessage { get; set; }
        public virtual int NoticeNum { get; set; } = 0;

        public virtual bool AutoTradeCheck { get; set; } = true;
        public virtual bool AutoCoinTradeCheck { get; set; } = true;

        public virtual string btn_bookmark { get; set; }
        public virtual string btn_bookmark_on { get; set; }
        public virtual string btn_quick_button_notice { get; set; }
        public virtual string btn_quick_button_notice_on { get; set; }

        public virtual Visibility AssetCoinVisible { get; set; }
        public virtual Visibility AssetNonCoinVisible { get; set; }

        public virtual string Main_Head_1 { get; set; }
        public virtual string Main_Head_2 { get; set; }
        public virtual string Main_Head_3 { get; set; }
        public virtual string Main_Head_4 { get; set; }
        public virtual string Main_Head_5 { get; set; }
        public virtual string Main_Head_6 { get; set; }

        public virtual string SC { get; set; } = CommonLib.StandardCurcyNm;
        public virtual string SD { get; set; } = CommonLib.StandardCurcyCd;

        bool AutoCheck = false;
        bool AutoCoinCheck = false;
        bool bPushMessage = true;
        bool AutoLoop = true;

        public static ObservableCollection<ResponseMainAssetListModel> Asset;

        /// <summary>
        /// 자산현황, 시세, 자동거래 타이머
        /// </summary>
        DispatcherTimer RepeatTimer;

        TrayIconLib TrayIcon;
        BookMark bm;

        public static RabbitMQClient mQClient;
        public static List<AutoOrderVM> AutoOrderData;
        public static ResponseTradingCoinAutoTradeSelDataModel AutoCoinData;

        List<string> lstNotice = new List<string>();

        int RepeatCnt = 0;

        /// <summary>
        /// 차트 팝업 공통사용
        /// </summary>
        public static ExchangeChartDashboard ecd = null;
        public static ResponseCoinDataModel CoinData;
        public static MkCoinList CoinList;
        public static ResponseGetMarketLimitDataModel MarketLimitData;

        public static MainViewModel Create()
        {
            return ViewModelSource.Create(() => new MainViewModel());
        }

        public MainViewModel()
        {
            try
            {
                App.Current.Exit += Current_Exit;
                //로그인 처리 메신저 등록(로그인 성공시)
                Messenger.Default.Register<ResponseLoginDataModel>(this, LoginMessenger);

                //서브 화면에서 특정 페이지 요청 메신저 등록(서브 화면에서 다른 화면 호출시)
                Messenger.Default.Register<MenuModel>(this, PageCallMessenger);

                //자동거래 호출 페이지 요청 메신저 등록(서브 화면에서 다른 화면 호출시)
                Messenger.Default.Register<AutoOrderVM>(this, AutoTradeMessenger);

                Messenger.Default.Register<string>(this, CoinSettingCall);

                //상단 호출 메신저 등록
                Messenger.Default.Register<string>(this, AssetsCall);

                //퀵메뉴 재호출
                Messenger.Default.Register<string>(this, QuickMenusCall);

                //코인별 시세 소켓에서 받은 데이터 처리
                Messenger.Default.Register<WsCoinPriceDataModel>(this, WsCoinPriceData);

                //트레이 아이콘
                TrayIcon = TrayIconLib.Instance();

                Menus = new ObservableCollection<MenuModel>();
                QuickMenus = new ObservableCollection<MenuModel>();
                AutoOrderData = new List<AutoOrderVM>();
                CoinPriceDataModel = new WsCoinPriceDataModel();
                Asset = new ObservableCollection<ResponseMainAssetListModel>();

                //타이머
                RepeatTimer = new DispatcherTimer();
                RepeatTimer.Tick += RepeatTimer_Tick;
                RepeatTimer.Interval = new TimeSpan(0, 0, 1);
                RepeatTimer.Start();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        ~MainViewModel()
        {
            try
            {
                if (mQClient != null)
                {
                    mQClient.ClientClose();
                    mQClient = null;
                }
                if (RepeatTimer != null)
                {
                    RepeatTimer.Stop();
                    RepeatTimer = null;
                }
                if (TrayIcon != null)
                {
                    TrayIcon.Close();
                    TrayIcon = null;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                if (mQClient != null)
                {
                    mQClient.ClientClose();
                    mQClient = null;
                }
                if (RepeatTimer != null)
                {
                    RepeatTimer.Stop();
                    RepeatTimer = null;
                }
                if (TrayIcon != null)
                {
                    TrayIcon.Close();
                    TrayIcon = null;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CoinSettingCall(string msg)
        {
            if (msg.Equals("CoinSetting"))
            {
                CoinSetting();
            }
        }

        public void CoinSetting()
        {
            try
            {
                int Lv = CertifyCheck();

                using (RequestGetMarketLimitModel req = new RequestGetMarketLimitModel())
                {
                    using (ResponseGetMarketLimitModel res = WebApiLib.SyncCall<ResponseGetMarketLimitModel, RequestGetMarketLimitModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            MarketLimitData = res.data;
                        }
                    }
                }

                using (RequestGetMkCoinListModel req = new RequestGetMkCoinListModel())
                {
                    using (ResponseGetMkCoinListModel res = WebApiLib.SyncCall<ResponseGetMkCoinListModel, RequestGetMkCoinListModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            CoinList = new MkCoinList();

                            foreach (ResponseGetMkCoinListListtModel item in res.data.list)
                            {
                                if (item.mkType.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)))
                                {
                                    CoinList.KRW.Add(item);
                                }
                                else if (item.mkType.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.ETH)))
                                {
                                    CoinList.ETH.Add(item);
                                }
                                else if (item.mkType.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.USDT)))
                                {
                                    CoinList.USDT.Add(item);
                                }
                                else if (item.mkType.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BTC)))
                                {
                                    CoinList.BTC.Add(item);
                                } 
                            }
                        }
                    }
                }

                //추후 DB 연결해서 만들기
                using (RequestGetCoinMaxMinModel req = new RequestGetCoinMaxMinModel())
                {
                    using (ResponseGetCoinMaxMinModel res = WebApiLib.SyncCall<ResponseGetCoinMaxMinModel, RequestGetCoinMaxMinModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            CoinData = new ResponseCoinDataModel();
                            CoinData.list = new ObservableCollection<ResponseCoinListModel>();

                            CoinData.mkKRW = res.data.mkKRW;
                            CoinData.mkUSDT = res.data.mkUSDT;
                            CoinData.mkETH = res.data.mkETH;
                            CoinData.mkBTC = res.data.mkBTC;

                            foreach (ResponseGetCoinMaxMinListModel item in res.data.list)
                            {
                                if (!item.cnkndcd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)))
                                {
                                    if (Lv.Equals(2))
                                    {
                                        CoinData.list.Add(new ResponseCoinListModel() { CoinName = item.cnkndnm, CoinCode = item.cnkndcd, CoinFloatCnt = item.coinDecimal, TradeBidding = item.asking, TradeMaxCnt = item.tradeMaxAmt, TradeMinCnt = item.tradeMinAmt, TransferMaxCnt = item.maxAmt2, TransferMinCnt = item.minAmt2, TransferDayCnt = item.maxAmtDay2, TransferFee = item.cnSndFee, TradeFillMaxPrc = item.tradeMaxAmt, TradeFillMinPrc = item.tradeMinPrc, IsTagYn = item.isTag, CoinDecimal = item.coinDecimal, CashDecimal = item.cashDecimal });
                                    }
                                    else if (Lv.Equals(3))
                                    {
                                        CoinData.list.Add(new ResponseCoinListModel() { CoinName = item.cnkndnm, CoinCode = item.cnkndcd, CoinFloatCnt = item.coinDecimal, TradeBidding = item.asking, TradeMaxCnt = item.tradeMaxAmt, TradeMinCnt = item.tradeMinAmt, TransferMaxCnt = item.maxAmt3, TransferMinCnt = item.minAmt3, TransferDayCnt = item.maxAmtDay3, TransferFee = item.cnSndFee, TradeFillMaxPrc = item.tradeMaxAmt, TradeFillMinPrc = item.tradeMinPrc, IsTagYn = item.isTag, CoinDecimal = item.coinDecimal, CashDecimal = item.cashDecimal });
                                    }
                                    else
                                    {
                                        CoinData.list.Add(new ResponseCoinListModel() { CoinName = item.cnkndnm, CoinCode = item.cnkndcd, CoinFloatCnt = item.coinDecimal, TradeBidding = item.asking, TradeMaxCnt = item.tradeMaxAmt, TradeMinCnt = item.tradeMinAmt, TransferMaxCnt = 0, TransferMinCnt = 0, TransferDayCnt = 0, TransferFee = item.cnSndFee, TradeFillMaxPrc = item.tradeMaxAmt, TradeFillMinPrc = item.tradeMinPrc, IsTagYn = item.isTag, CoinDecimal = item.coinDecimal, CashDecimal = item.cashDecimal });
                                    }
                                }
                                else
                                {
                                    CommonLib.StandardCurcyNm = item.cnkndnm;
                                    CommonLib.StandardCurcyCd = item.cnkndcd;
                                    SC = CommonLib.StandardCurcyNm;
                                    SD = CommonLib.StandardCurcyCd;

                                    CoinData.CashDecimal = item.cashDecimal;

                                    if (Lv.Equals(2))
                                    {
                                        CoinData.WithdrawMaxPrc = item.maxAmt2;
                                        CoinData.WithdrawMinPrc = item.minAmt2;
                                        CoinData.WithdrawDayPrc = item.maxAmtDay2;
                                        CoinData.WithdrawFee = item.cnSndFee;
                                        CoinData.TradeFillMinPrc = item.tradeMinAmt;
                                        CoinData.TradeFillMaxPrc = item.tradeMaxAmt;
                                        CoinData.Lv = Lv;
                                    }
                                    else if (Lv.Equals(3))
                                    {
                                        CoinData.WithdrawMaxPrc = item.maxAmt3;
                                        CoinData.WithdrawMinPrc = item.minAmt3;
                                        CoinData.WithdrawDayPrc = item.maxAmtDay3;
                                        CoinData.WithdrawFee = item.cnSndFee;
                                        CoinData.TradeFillMinPrc = item.tradeMinAmt;
                                        CoinData.TradeFillMaxPrc = item.tradeMaxAmt;
                                        CoinData.Lv = Lv;
                                    }
                                    else
                                    {
                                        CoinData.WithdrawMaxPrc = 0;
                                        CoinData.WithdrawMinPrc = 0;
                                        CoinData.WithdrawDayPrc = 0;
                                        CoinData.WithdrawFee = 0;
                                        CoinData.TradeFillMinPrc = item.tradeMinAmt;
                                        CoinData.TradeFillMaxPrc = item.tradeMaxAmt;
                                        CoinData.Lv = Lv;
                                    }
                                }
                            }
                        }
                    }
                }

                if (CoinPriceDataModel.list.Count.Equals(0))
                {
                    foreach (ResponseCoinListModel item in CoinData.list)
                    {
                        DispatcherService.BeginInvoke(() =>
                        {
                            if (!item.CoinCode.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.USDT)))
                            {
                                CoinPriceDataModel.list.Add(ViewModelSource.Create(() => new WsCoinPriceListModel() { curcyCd = item.CoinCode, NowPriceLangView = Main_Head_5, coinImage = "/Images/ico_coinstatus_" + item.CoinName + ".png", coinDisplayName = item.CoinName }));
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void NoticeList()
        {
            using (RequestSelectBoardModel req = new RequestSelectBoardModel())
            {
                req.boardId = StringEnum.GetStringValue(EnumLib.BoardCode.Notice);
                req.lang = LoginViewModel.LanguagePack.Split('/')[2].Split('-')[0];
                req.pageIndex = 1;

                using (ResponseSelectBoardModel res = await WebApiLib.AsyncCall<ResponseSelectBoardModel, RequestSelectBoardModel>(req))
                {
                    if (res.resultStrCode == "000")
                    {
                        if (res.data.list.Count > 0)
                        {
                            int iCnt = 0;

                            foreach (ResponseSelectBoardListModel item in res.data.list)
                            {
                                if (iCnt < 5)
                                {
                                    lstNotice.Add(item.boardTitle);
                                    iCnt++;
                                }
                            }

                            if (lstNotice.Count > 0)
                            {
                                NoticeMessage = lstNotice[0];
                            }
                        }
                    }
                }
            }
        }

        public void WsCoinPriceData(WsCoinPriceDataModel item)
        {
            try
            {
                CoinMarketPrice(item);

                if (AutoTradeCheck)
                {
                    AutoTradeCheck = false;
                    AutoTrade(item);
                    AutoTradeCheck = true;
                }

                //if (AutoCoinTradeCheck)
                //{
                //    AutoCoinTradeCheck = false;
                //    AutoCoinTrade(item);
                //    AutoCoinTradeCheck = true;
                //}
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void CoinMarketPrice(WsCoinPriceDataModel item)
        {
            try
            {
                if (item.marketCode == CommonLib.StandardCurcyCd)
                {   
                    foreach (ResponseMainAssetListModel item2 in Asset.OrderBy(o => o.curcyCd))
                    {
                        var prc = item.list.ToList().Where(w => w.curcyCd == item2.curcyCd).FirstOrDefault();

                        if (prc == null)
                        {
                            prc = new WsCoinPriceListModel();
                            prc.curcyCd = item2.curcyCd;
                            prc.posCnAmt = item2.posCnAmt;
                            prc.impCnAmt = item2.impCnAmt;
                            prc.totCoinAmt = item2.totCoinAmt;

                            if (item2.kwdPrc > 0)
                            {
                                prc.exchangeCode = "( " + "≈" + item2.kwdPrc.ToString("n0") + " " + SC + ")";
                            }
                            else
                            {
                                prc.exchangeCode = "( " + item2.kwdPrc.ToString("n0") + " " + SC + ")";
                            }

                            DispatcherService.BeginInvoke(() =>
                            {
                                CoinPriceDataModel.list.Where(w => w.curcyCd == prc.curcyCd).Select(s => { s.CoinPrcData = prc.CoinPrcData; s.UpDown = prc.UpDown; s.DataColor = prc.DataColor; s.posCnAmt = prc.posCnAmt; s.impCnAmt = prc.impCnAmt; s.totCoinAmt = prc.totCoinAmt; s.kwdPrc = prc.kwdPrc; s.exchangeCode = prc.exchangeCode; return s; }).ToList();
                            });
                        }
                        else
                        {
                            EnumLib.ExchangeCurrencyCode ecc = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(prc.curcyCd);

                            if (ecc == EnumLib.ExchangeCurrencyCode.KRW)// || ecc == EnumLib.ExchangeCurrencyCode.BMC)
                            {
                                return;
                            }

                            prc.NowPriceLangView = Main_Head_5;
                            prc.coinImage = "/Images/ico_coinstatus_" + ecc.ToString() + ".png";
                            prc.coinDisplayName = ecc.ToString();

                            decimal tempsellTranPrcPer = 0;

                            if (prc.ytdPrc == 0)
                            {
                                tempsellTranPrcPer = 0;
                            }
                            else
                            {
                                tempsellTranPrcPer = ((((decimal)prc.coinPrc - (decimal)prc.ytdPrc) / (decimal)prc.ytdPrc) * 100);
                            }
                            if (tempsellTranPrcPer > 0)
                            {
                                prc.UpDown = "▲";
                                prc.DataColor = "#fc7777";
                            }
                            else if (tempsellTranPrcPer == 0)
                            {
                                prc.UpDown = "－";
                                prc.DataColor = "#bdbdbd";
                            }
                            else
                            {
                                prc.UpDown = "▼";
                                prc.DataColor = "#0093cf";
                            }
                            prc.CoinPrcData = Convert.ToSingle(prc.coinPrc).ToString("#,0.###") + "(" + prc.UpDown + Convert.ToSingle(tempsellTranPrcPer).ToString("#,0.##") + "%)";

                            //var tempAs = Asset.Where(w => w.curcyCd == prc.curcyCd).FirstOrDefault();
                            //if (tempAs != null)
                            //{
                            prc.posCnAmt = item2.posCnAmt;
                            prc.impCnAmt = item2.impCnAmt;
                            prc.totCoinAmt = item2.totCoinAmt;

                            if (item2.kwdPrc > 0)
                            {
                                //prc.exchangeCode = "( " + "≈" + item2.kwdPrc.ToString(prc.floatFormat) + " " + SC + ")";
                                prc.exchangeCode = "( " + "≈" + item2.kwdPrc.ToString("n0") + " " + SC + ")";
                            }
                            else
                            {
                                //prc.exchangeCode = "( " + item2.kwdPrc.ToString(prc.floatFormat) + " " + SC + ")";
                                prc.exchangeCode = "( " + item2.kwdPrc.ToString("n0") + " " + SC + ")";
                            }
                            //}

                            DispatcherService.BeginInvoke(() =>
                            {
                                CoinPriceDataModel.list.Where(w => w.curcyCd == prc.curcyCd).Select(s => { s.CoinPrcData = prc.CoinPrcData; s.UpDown = prc.UpDown; s.DataColor = prc.DataColor; s.posCnAmt = prc.posCnAmt; s.impCnAmt = prc.impCnAmt; s.totCoinAmt = prc.totCoinAmt; s.kwdPrc = prc.kwdPrc; s.exchangeCode = prc.exchangeCode; return s; }).ToList();
                            });
                        }
                    }
                }

                //foreach (WsCoinPriceListModel prc in item.list.OrderBy(o => o.curcyCd))
                //{
                //    EnumLib.ExchangeCurrencyCode ecc = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(prc.curcyCd);

                //    if (ecc == EnumLib.ExchangeCurrencyCode.KRW || ecc == EnumLib.ExchangeCurrencyCode.BMC)
                //    {
                //        return;
                //    }

                //    prc.NowPriceLangView = Main_Head_5;
                //    prc.coinImage = "/Images/ico_coinstatus_" + ecc.ToString() + ".png";
                //    prc.coinDisplayName = ecc.ToString();

                //    decimal tempsellTranPrcPer = 0;

                //    if (prc.ytdPrc == 0)
                //    {
                //        tempsellTranPrcPer = 0;
                //    }
                //    else
                //    {
                //        tempsellTranPrcPer = ((((decimal)prc.coinPrc - (decimal)prc.ytdPrc) / (decimal)prc.ytdPrc) * 100);
                //    }
                //    if (tempsellTranPrcPer > 0)
                //    {
                //        prc.UpDown = "▲";
                //        prc.DataColor = "#fc7777";
                //    }
                //    else if (tempsellTranPrcPer == 0)
                //    {
                //        prc.UpDown = "－";
                //        prc.DataColor = "#bdbdbd";
                //    }
                //    else
                //    {
                //        prc.UpDown = "▼";
                //        prc.DataColor = "#0093cf";
                //    }
                //    prc.CoinPrcData = Convert.ToSingle(prc.coinPrc).ToString("#,0.###") + "(" + prc.UpDown + Convert.ToSingle(tempsellTranPrcPer).ToString("#,0.##") + "%)";

                //    var tempAs = Asset.Where(w => w.curcyCd == prc.curcyCd).FirstOrDefault();
                //    if (tempAs != null)
                //    {
                //        prc.posCnAmt = tempAs.posCnAmt;
                //        prc.impCnAmt = tempAs.impCnAmt;
                //        prc.totCoinAmt = tempAs.totCoinAmt;

                //        if (tempAs.kwdPrc > 0)
                //        {
                //            prc.exchangeCode = "( " + "≈" + tempAs.kwdPrc.ToString("#,0") + " " + SC + ")";
                //        }
                //        else
                //        {
                //            prc.exchangeCode = "( " + tempAs.kwdPrc.ToString("#,0") + " " + SC + ")";
                //        }
                //    }

                //    DispatcherService.BeginInvoke(() =>
                //    {
                //        CoinPriceDataModel.list.Where(w => w.curcyCd == prc.curcyCd).Select(s => { s.CoinPrcData = prc.CoinPrcData; s.UpDown = prc.UpDown; s.DataColor = prc.DataColor; s.posCnAmt = prc.posCnAmt; s.impCnAmt = prc.impCnAmt; s.totCoinAmt = prc.totCoinAmt; s.kwdPrc = prc.kwdPrc; s.exchangeCode = prc.exchangeCode; return s; }).ToList();
                //    });
                //}
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                await Task.Delay(1000);
            }
        }

        public void LogOut()
        {
            try
            {
                DispatcherService.BeginInvoke(() =>
                {
                    List<IDocument> docList = DocumentManagerService.Documents.ToList();
                    for (int i = 0; i < docList.Count(); i++)
                    {
                        IDocument item = docList.ToList()[i];
                        item.DestroyOnClose = true;
                        item.Close();
                    }

                    LoginDataModel = null;
                    Menus.Clear();
                    QuickMenus.Clear();
                    //mQClient.ClientClose();
                    CoinPriceDataModel.list.Clear();
                    Messenger.Default.Send("LogOut");
                });
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void LoginMessenger(ResponseLoginDataModel responseLoginDataModel)
        {
            try
            {
                //DispatcherService.BeginInvoke(() =>
                //{
                //    //공지사항
                //    Views.NoticePopup note = new Views.NoticePopup(NoticePopup.KindNotice.Notice);
                //    note.Title = Localization.Resource.NoticePopup_1;
                //    note.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                //    note.WindowStyle = WindowStyle.None;
                //    note.Show();
                //});
                
                if (!responseLoginDataModel.notice.Equals(string.Empty))
                    TrayIcon.ShowMesssage(responseLoginDataModel.notice.ToString(), string.Empty);

                //로그인 정보 설정
                LoginDataModel = responseLoginDataModel;

                //사용자코인 확인
                CoinCheck();

                CoinSetting();

                //MQ 
                mQClient = RabbitMQClient.Instance();

                //메뉴 생성
                CreateMenu();

                //빠른메뉴 생성
                CreateQuickMenu();

                ImageSet();

                GetAsset();

                //CoinMarketPrice(mQClient.WsCoinPriceData);

                //if (CertifyCheck() < 3)
                //{
                //    //인증이 미완료 일경우
                //    MenuSelected(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_6_2"), Id = "CertifyMyPage", IconPath = "/Images/ico_nav_cert_center.png" });
                //}
                //else
                //{
                //메뉴 생성 후 거래소 현황 메뉴 선택(기본 화면)
                MenuSelected(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_1_1"), Id = "ExchangeDashboard" });
                //}
                //MenuSelected(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("테스트"), Id = "WebSocketTest" });//테스트 페이지

                GetAutoBuySell();
                //GetAutoCoinExchange();
                NoticeList();

                Task.Run(() =>
                {
                    DispatcherService.BeginInvoke(() =>
                    {
                        using (RequestGetPopupListModel req = new RequestGetPopupListModel())
                        {
                            req.langCd = LoginViewModel.LanguagePack.Split('/')[2].Split('-')[0];

                            using (ResponseGetPopupListModel res = WebApiLib.SyncCall<ResponseGetPopupListModel, RequestGetPopupListModel>(req))
                            {
                                if (res.resultStrCode == "000")
                                {
                                    if (res.data.listCnt > 0)
                                    {
                                        CommonPopup pop = new CommonPopup(res.data.list);
                                        pop.ShowDialog();
                                    }
                                }
                            }
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        int CertifyCheck()
        {
            int val = 0;
            string otpYn = string.Empty;
            try
            {
                CommonLib.GetOtpYn(ref otpYn);

                //이메일 인증(1)
                if (LoginDataModel.emailCertYn == "Y")
                {
                    val++;
                }

                if (otpYn.Equals("N"))
                {
                    if (!string.IsNullOrWhiteSpace(LoginDataModel.otpNo))
                    {
                        val++;

                        using (RequestCertifyInfoModel req = new RequestCertifyInfoModel())
                        {
                            req.userEmail = LoginDataModel.userEmail;

                            using (ResponseCertifyInfoModel res = WebApiLib.SyncCall<ResponseCertifyInfoModel, RequestCertifyInfoModel>(req))
                            {
                                if (res.resultStrCode == "000")
                                {
                                    string kycStatus = string.Empty;
                                    kycStatus = res.data.kycCertYn;
                                    LoginDataModel.kycStatus = kycStatus;
                                    if (kycStatus.Equals("1"))
                                    {
                                        //val++;
                                        val += 1;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        using (RequestCertifyInfoModel req = new RequestCertifyInfoModel())
                        {
                            req.userEmail = LoginDataModel.userEmail;

                            using (ResponseCertifyInfoModel res = WebApiLib.SyncCall<ResponseCertifyInfoModel, RequestCertifyInfoModel>(req))
                            {
                                if (res.resultStrCode == "000")
                                {
                                    string kycStatus = string.Empty;
                                    kycStatus = res.data.kycCertYn;
                                    LoginDataModel.kycStatus = kycStatus;
                                    if (kycStatus.Equals("1"))
                                    {
                                        //val++;
                                        val += 2;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(LoginDataModel.otpNo))
                    {
                        val++;
                    }

                    using (RequestCertifyInfoModel req = new RequestCertifyInfoModel())
                    {
                        req.userEmail = LoginDataModel.userEmail;

                        using (ResponseCertifyInfoModel res = WebApiLib.SyncCall<ResponseCertifyInfoModel, RequestCertifyInfoModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                string kycStatus = string.Empty;
                                kycStatus = res.data.kycCertYn;
                                LoginDataModel.kycStatus = kycStatus;
                                if (kycStatus.Equals("1"))
                                {
                                    val++;
                                }
                            }
                        }
                    }
                }
             

                ////휴대폰 인증(2)
                //if (LoginDataModel.smsCertYn == "Y")
                //{
                //    val++;
                //}
                ////기본인증_계좌(3)
                //if (!string.IsNullOrWhiteSpace(LoginDataModel.accountNo))
                //{
                //    val++;
                //}
                //OPT 등록 여부(4)
                //if (!string.IsNullOrWhiteSpace(LoginDataModel.otpNo))
                //{
                //    val++;
                //}
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            return val;
        }

        //메인 상단 자산정보 호출 Messenger
        public async void AssetsCall(string Message)
        {
            try
            {
                var task = Task.Run(() =>
                {
                    if (Message.Equals("AssetsRefresh"))
                    {
                        GetAsset();
                        Messenger.Default.Send<string>("AssetsRefresh_View");
                    }
                });
                await task;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void QuickMenusCall(string Message)
        {
            try
            {
                if (Message.Equals("QuickMenuRefresh"))
                {
                    CreateQuickMenu();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 코인 자동거래 등록/삭제
        /// </summary>
        /// <param name="menuModel"></param>
        public void AutoTradeMessenger(AutoOrderVM item)
        {
            try
            {
                if (item.OrderType.Equals("B"))
                {
                    if (!item.IsBuyTradeEnabled)
                    {
                        AutoOrderData.Add(item);
                    }
                    else
                    {
                        AutoOrderVM temp = AutoOrderData.Where(x => x.RecCoinType == item.RecCoinType).Where(x => x.OrderCoinType == item.OrderCoinType).Where(x => x.OrderType == item.OrderType).Where(x => x.Rank == item.Rank).First();
                        if (temp != null)
                        {
                            AutoOrderData.Remove(temp);
                        }
                    }
                }
                else if (item.OrderType.Equals("S"))
                {
                    if (!item.IsSellTradeEnabled)
                    {
                        AutoOrderData.Add(item);
                    }
                    else
                    {
                        AutoOrderVM temp = AutoOrderData.Where(x => x.RecCoinType == item.RecCoinType).Where(x => x.OrderCoinType == item.OrderCoinType).Where(x => x.OrderType == item.OrderType).Where(x => x.Rank == item.Rank).First();
                        if (temp != null)
                        {
                            AutoOrderData.Remove(temp);
                        }
                    }
                }
                else if (item.OrderType.Equals("L"))
                {
                    if (!item.IsLossTradeEnabled)
                    {
                        AutoOrderData.Add(item);
                    }
                    else
                    {
                        AutoOrderVM temp = AutoOrderData.Where(x => x.RecCoinType == item.RecCoinType).Where(x => x.OrderCoinType == item.OrderCoinType).Where(x => x.OrderType == item.OrderType).Where(x => x.Rank == item.Rank).First();
                        if (temp != null)
                        {
                            AutoOrderData.Remove(temp);
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
        /// 서브 페이지 에서 다른 화면 호출
        /// </summary>
        /// <param name="menuModel"></param>
        public void PageCallMessenger(MenuModel menuModel)
        {
            try
            {
                MenuSelected(menuModel);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetRtcp()
        {
            try
            {
                using (RequestGetTimeModel req = new RequestGetTimeModel())
                {
                    using (ResponseGetTimeModel res = await WebApiLib.AsyncCall<ResponseGetTimeModel, RequestGetTimeModel>(req))
                    {
                        ResponseGetTimeDataModel dataTime = res.data;
                        RequestBaseModel.RtpParam = Utility.EncodingLib.HmacSha256Encrypt(dataTime.sysTime);
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetRsa()
        {
            try
            {
                Dictionary<string, string> dict = null;

                using (RequestPublicKeyModel req = new RequestPublicKeyModel())
                {
                    using (ResponsePublicKeyModel res = await WebApiLib.AsyncCall<ResponsePublicKeyModel, RequestPublicKeyModel>(req))
                    {                        
                        dict = EncodingLib.AesEncryptKey(res.data.pubKeyModule, res.data.pubKeyExponent);
                        RequestBaseModel.RsaParam = dict["acekey"];
                        RequestBaseModel.GidParam = dict["gid"];
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
                RepeatCnt++;

                if (LoginDataModel == null)
                {
                    if (RepeatCnt % 3 == 0)
                    {
                        GetRtcp();
                        GetRsa();
                        //RepeatCnt = 0;
                    }
                }
                else
                {
                    if (AutoCheck)
                    {
                        Messenger.Default.Send("AutoSettingRefresh");
                        AutoCheck = false;
                    }
                    //if (AutoCoinCheck)
                    //{
                    //    Messenger.Default.Send("AutoCoinSettingRefresh");
                    //    AutoCoinCheck = false;
                    //}
                    if (RepeatCnt % 300 == 0)
                    {
                        GetAsset();
                        RepeatCnt = 0;
                    }
                    //if (RepeatCnt % 180 == 0)
                    //{
                    //    CoinSetting();
                    //}
                    if (RepeatCnt % 3 == 0)
                    {
                        //RepeatCnt = 0;

                        GetRtcp();
                        GetRsa();

                        //자동거래
                        //AutoTrade();

                        if (bPushMessage)
                        {
                            bPushMessage = false;
                            GetPushMessage();
                        }

                        DispatcherService.BeginInvoke(() =>
                        {

                        });
                    }

                    if (RepeatCnt % 5 == 0)
                    {
                        if (lstNotice == null) return;
                        if (lstNotice.Count == 0) return;

                        if (NoticeNum >= lstNotice.Count) NoticeNum = 0;
                        NoticeMessage = lstNotice[NoticeNum];
                        NoticeNum++;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                System.Threading.Thread.Sleep(10);
            }
        }

        async void GetPushMessage()
        {
            int iCnt = 0;
            //DateTime st = DateTime.Now;
            try
            {
                //if (bPushMessage)
                //{
                //bPushMessage = false;

                RequestPushModel requestPushModel = new RequestPushModel();
                requestPushModel.userEmail = LoginDataModel.userEmail;
                using (ResponsePushModel res = await WebApiLib.AsyncCall<ResponsePushModel, RequestPushModel>(requestPushModel))
                {
                    if (res != null)
                    {
                        foreach (var item in res.data.list)
                        {
                            if (item.pushType == "CMMC00000000285")
                            {
                                //공지사항
                                //NoticeMessage = item.pushMsg;
                                //string tempStr = stringCut(NoticeMessage, 50);
                                //foreach (string temp in tempStr.Split('\n'))
                                //{
                                //    lstNotice.Add(temp.Replace("\r", ""));
                                //}
                                //NoticeMessage = lstNotice.ElementAt(0);
                            }
                            else
                            {
                                DispatcherService.BeginInvoke(() =>
                                {
                                    TrayIcon.ShowMesssage(item.pushMsg, item.pushType);
                                });

                                if ((item.pushType == "CMMC00000000286") || (item.pushType == "CMMC00000000287") || (item.pushType == "CMMC00000000651") || (item.pushType == "CMMC00000000652"))
                                {
                                    iCnt++;
                                }
                                else if ((item.pushType == "CMMC00000000405"))
                                {
                                    AutoOrderData.Clear();
                                    GetAutoBuySell();
                                }
                                else if (item.pushType == "CMMC00000000427")
                                {
                                    using (RequestIpRegContentModel req2 = new RequestIpRegContentModel())
                                    {
                                        req2.userEmail = LoginDataModel.userEmail;

                                        using (ResponseIpRegContentModel res2 = await WebApiLib.AsyncCall<ResponseIpRegContentModel, RequestIpRegContentModel>(req2))
                                        {
                                            bool IpCheck = false;
                                            foreach (var item2 in res2.data.list)
                                            {
                                                if (item2.ip == LoginDataModel.regIp)
                                                {
                                                    IpCheck = true;
                                                }
                                            }
                                            if (!IpCheck)
                                            {
                                                LogOut();
                                            }
                                        }
                                    }
                                }
                                else if (item.pushType == "CMMC00000000428")
                                {
                                    LogOut();
                                }
                                else if (item.pushType == "CMMC00000000825")
                                {
                                    Messenger.Default.Send("CardListUpdate");
                                }
                                else if (item.pushType == "CMMC00000001745")
                                {
                                    Messenger.Default.Send("ArbitrageListUpdate");
                                }

                                if (item.pushMsg.IndexOf("KYC") > 0)
                                {
                                    using (RequestCertifyInfoModel req3 = new RequestCertifyInfoModel())
                                    {
                                        req3.userEmail = LoginDataModel.userEmail;

                                        using (ResponseCertifyInfoModel res3 = WebApiLib.SyncCall<ResponseCertifyInfoModel, RequestCertifyInfoModel>(req3))
                                        {
                                            if (res.resultStrCode == "000")
                                            {
                                                CoinSetting();
                                                Messenger.Default.Send("kycStatusUpdate");
                                                LoginDataModel.kycStatus = res3.data.kycCertYn;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                //DateTime et = DateTime.Now;
                //SysLog.Error("st[{0}], ed[{1}], diff[{2}] ", st.ToString("yyyyMMdd HH:mm:ss:FFF"), et.ToString("yyyyMMdd HH:mm:ss:FFF"), (et - st).TotalMilliseconds.ToString());
                bPushMessage = true;

                if (iCnt > 0)
                {
                    Messenger.Default.Send("AssetsRefresh");
                    Messenger.Default.Send("AssetsRefresh_CoinTrading");
                    Messenger.Default.Send("NonCoinListRefresh");
                    Messenger.Default.Send("TradeHistoryRefresh");
                }
            }
        }

        //메인 상단 자산정보 가져오기
        public async void GetAsset()
        {
            try
            {
                RequestMainAssetModel requestMainAssetModel = new RequestMainAssetModel();
                requestMainAssetModel.userEmail = LoginDataModel.userEmail;
                using (ResponseMainAssetModel res = await WebApiLib.AsyncCall<ResponseMainAssetModel, RequestMainAssetModel>(requestMainAssetModel))
                {
                    if (res != null && res.data.list.Count > 0)
                    {
                        DispatcherService.BeginInvoke(() =>
                        {
                            Asset = res.data.list;
                            TotAsset = 0;
                            bool bCoinCheck = false;

                            foreach (var item in res.data.list)
                            {
                                if (item.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.KRW)))
                                {
                                    item.CashDecimal = "n" + CoinData.CashDecimal;
                                    KrwAssetModel = item;
                                }
                                else if (item.curcyCd.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.USDT)))
                                {
                                    UsdtAssetModel = item;
                                }
                                else
                                {
                                    TotAsset += item.kwdPrc;

                                    if (!item.totCoinAmt.Equals(0))
                                    {
                                        bCoinCheck = true;
                                        CoinPriceDataModel.list.Where(w => w.curcyCd == item.curcyCd).ToList().ForEach(s => s.Visible = Visibility.Visible);
                                    }
                                    else
                                    {
                                        CoinPriceDataModel.list.Where(w => w.curcyCd == item.curcyCd).ToList().ForEach(s => s.Visible = Visibility.Collapsed);
                                    }
                                }
                            }

                            if (bCoinCheck)
                            {
                                AssetCoinVisible = Visibility.Visible;
                                AssetNonCoinVisible = Visibility.Collapsed;
                            }
                            else
                            {
                                AssetCoinVisible = Visibility.Collapsed;
                                AssetNonCoinVisible = Visibility.Visible;
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetAutoBuySell()
        {
            try
            {
                bool serviceUse = false;

                using (RequestAutoTradeContentModel req = new RequestAutoTradeContentModel())
                {
                    req.userEmail = LoginDataModel.userEmail;

                    using (ResponseAutoTradeContentModel res = await WebApiLib.AsyncCall<ResponseAutoTradeContentModel, RequestAutoTradeContentModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            if (res.data.cnKndCd == null)
                            {
                                serviceUse = false;
                            }
                            else
                            {
                                serviceUse = true;
                            }
                        }
                    }
                }

                if (serviceUse)
                {
                    EnumLib.ExchangeCurrencyCode TempCoinCode = EnumLib.ExchangeCurrencyCode.KRW;
                    EnumLib.ExchangeCurrencyCode PriceCode = EnumLib.ExchangeCurrencyCode.KRW;

                    using (RequestTradingGetAutoSellModel req = new RequestTradingGetAutoSellModel())
                    {
                        req.userEmail = LoginDataModel.userEmail;

                        using (ResponseTradingGetAutoSellModel res = await WebApiLib.AsyncCall<ResponseTradingGetAutoSellModel, RequestTradingGetAutoSellModel>(req))
                        {
                            foreach (ResponseTradingGetAutoSellListModel item in res.data.list)
                            {
                                TempCoinCode = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(item.cnKndCd);
                                if (item.mkState.Equals(CommonLib.StandardCurcyNm))
                                {
                                    PriceCode = (EnumLib.ExchangeCurrencyCode)Enum.Parse(typeof(EnumLib.ExchangeCurrencyCode), "KRW");
                                }
                                else
                                {
                                    PriceCode = (EnumLib.ExchangeCurrencyCode)Enum.Parse(typeof(EnumLib.ExchangeCurrencyCode), item.mkState);
                                }

                                ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == item.cnKndCd).FirstOrDefault();
                                decimal CommonFloat = cl.CashDecimal;

                                AutoOrderVM vm = ViewModelSource.Create(() => new AutoOrderVM("L", item.udFlag, item.sn, TempCoinCode, PriceCode, CommonFloat));
                                vm.OrderPrc = item.prc;
                                vm.OrderErrorRate = item.prcPer;
                                vm.OrderErrorLowPrc = item.rtMnsPrc;
                                vm.OrderErrorHighPrc = item.rtPlsPrc;
                                vm.OrderCnt = item.amt;
                                AutoOrderData.Add(vm);
                            }
                        }
                    }

                    using (RequestTradingGetAutoBuyModel req = new RequestTradingGetAutoBuyModel())
                    {
                        req.userEmail = LoginDataModel.userEmail;

                        using (ResponseTradingGetAutoBuyModel res = await WebApiLib.AsyncCall<ResponseTradingGetAutoBuyModel, RequestTradingGetAutoBuyModel>(req))
                        {
                            foreach (ResponseTradingGetAutoBuyListModel item in res.data.list)
                            {
                                TempCoinCode = StringEnum.ToEnum<EnumLib.ExchangeCurrencyCode>(item.cnKndCd);
                                if (item.mkState.Equals(CommonLib.StandardCurcyNm))
                                {
                                    PriceCode = (EnumLib.ExchangeCurrencyCode)Enum.Parse(typeof(EnumLib.ExchangeCurrencyCode), "KRW");
                                }
                                else
                                {
                                    PriceCode = (EnumLib.ExchangeCurrencyCode)Enum.Parse(typeof(EnumLib.ExchangeCurrencyCode), item.mkState);
                                }

                                ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == item.cnKndCd).FirstOrDefault();
                                decimal CommonFloat = cl.CashDecimal;

                                if (item.udFlag.Equals("B"))
                                {
                                    AutoOrderVM vm = ViewModelSource.Create(() => new AutoOrderVM("B", item.udFlag, item.sn, TempCoinCode, PriceCode, CommonFloat));
                                    vm.OrderPrc = item.prc;
                                    vm.OrderErrorRate = item.prcPer;
                                    vm.OrderErrorLowPrc = item.rtMnsPrc;
                                    vm.OrderErrorHighPrc = item.rtPlsPrc;
                                    vm.OrderCnt = item.amt;
                                    AutoOrderData.Add(vm);
                                }
                                else if (item.udFlag.Equals("S"))
                                {
                                    AutoOrderVM vm = ViewModelSource.Create(() => new AutoOrderVM("S", item.udFlag, item.sn, TempCoinCode, PriceCode, CommonFloat));
                                    vm.OrderPrc = item.prc;
                                    vm.OrderErrorRate = item.prcPer;
                                    vm.OrderErrorLowPrc = item.rtMnsPrc;
                                    vm.OrderErrorHighPrc = item.rtPlsPrc;
                                    vm.OrderCnt = item.amt;
                                    AutoOrderData.Add(vm);
                                }
                            }
                        }
                    }
                }
                else
                {
                    using (RequestTradingDelAutoBuyModel req = new RequestTradingDelAutoBuyModel())
                    {
                        req.userEmail = LoginDataModel.userEmail;

                        using (ResponseTradingDelAutoBuyModel res = await WebApiLib.AsyncCall<ResponseTradingDelAutoBuyModel, RequestTradingDelAutoBuyModel>(req))
                        {
                            //자동구매_판매 데이터 삭제
                        }
                    }

                    using (RequestTradingDelAutoSellModel req = new RequestTradingDelAutoSellModel())
                    {
                        req.userEmail = LoginDataModel.userEmail;

                        using (ResponseTradingDelAutoSellModel res = await WebApiLib.AsyncCall<ResponseTradingDelAutoSellModel, RequestTradingDelAutoSellModel>(req))
                        {
                            //손절판매 데이터 삭제
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
                AutoCheck = true;
            }
        }

        public async void GetAutoCoinExchange()
        {
            try
            {
                using (RequestTradingCoinAutoTradeSelModel req = new RequestTradingCoinAutoTradeSelModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseTradingCoinAutoTradeSelModel res = await WebApiLib.AsyncCall<ResponseTradingCoinAutoTradeSelModel, RequestTradingCoinAutoTradeSelModel>(req))
                    {
                        if (!string.IsNullOrWhiteSpace(res.data.regDt))
                        {
                            if (AutoCoinData == null)
                                AutoCoinData = new ResponseTradingCoinAutoTradeSelDataModel();

                            AutoCoinData = res.data;
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
                AutoCoinCheck = true;
            }
        }

        //자동거래
        async void AutoTrade(WsCoinPriceDataModel item)
        {
            try
            {
                if (AutoLoop)
                {
                    AutoLoop = false;

                    foreach (var tempList in item.list)
                    {
                        foreach (var tempAuto in AutoOrderData)
                        {
                            if (item.marketCode == StringEnum.GetStringValue(tempAuto.RecCoinType) && tempList.curcyCd.Equals(StringEnum.GetStringValue(tempAuto.OrderCoinType)))
                            {
                                if (tempAuto.OrderType == "B")
                                {
                                    //체결 금액이랑 현재 시세랑 비교
                                    if (tempAuto.conclusionMinPrc != tempList.minPrc)
                                    {
                                        //다르면 현재 시세 입력
                                        tempAuto.conclusionMinPrc = tempList.minPrc;
                                        tempAuto.conclusionBuyCheck = true;
                                    }

                                    if (tempAuto.conclusionBuyCheck)
                                    {
                                        tempAuto.conclusionBuyCheck = false;
                                        //자동 구매가 와 시장가 비교
                                        //오차하한가 <= 시장가 현재 시세 <= 오차상한가
                                        if ((tempAuto.OrderErrorLowPrc <= decimal.Parse(tempList.minPrc.ToString()) && (decimal.Parse(tempList.minPrc.ToString()) <= tempAuto.OrderErrorHighPrc)))
                                        {
                                            decimal assetCnt = 0;

                                            if (item.marketCode.Equals(CommonLib.StandardCurcyCd))
                                            {
                                                using (RequestTradingAbleBuyModel req = new RequestTradingAbleBuyModel())
                                                {
                                                    req.userEmail = LoginDataModel.userEmail;
                                                    req.curcyCd = StringEnum.GetStringValue(tempAuto.OrderCoinType);

                                                    using (ResponseTradingAbleBuyModel res = await WebApiLib.AsyncCall<ResponseTradingAbleBuyModel, RequestTradingAbleBuyModel>(req))
                                                    {
                                                        assetCnt = res.data.price;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                using (RequestTradingCoinToCoinSellModel req = new RequestTradingCoinToCoinSellModel())
                                                {
                                                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                                                    req.curcyCd = StringEnum.GetStringValue(tempAuto.RecCoinType);  //구매하려는 통화
                                                    req.rcvCurcyCd = StringEnum.GetStringValue(tempAuto.OrderCoinType); //결제하려는 통화
                                                    req.mkState = item.marketType;

                                                    using (ResponseTradingCoinToCoinSellModel res = WebApiLib.SyncCall<ResponseTradingCoinToCoinSellModel, RequestTradingCoinToCoinSellModel>(req))
                                                    {
                                                        assetCnt = (decimal)res.data.curcyAmt;
                                                    }
                                                }
                                            }

                                            //사용 가능 금액 체크
                                            //오차하한가 * 개수 <= 현재 금액 
                                            if (((tempAuto.OrderErrorLowPrc * tempAuto.OrderCnt) <= assetCnt))
                                            {
                                                //시장가 구매실행
                                                using (RequestTradingBuyModel req2 = new RequestTradingBuyModel())
                                                {
                                                    req2.apntPhsYn = "N";//시장가
                                                    req2.apntPhsCd = StringEnum.GetStringValue(EnumLib.TradeContentCode.buyMarketPrice);
                                                    req2.userEmail = LoginDataModel.userEmail;//계정정보
                                                    req2.buyCurcyCd = StringEnum.GetStringValue(tempAuto.OrderCoinType);//수령코인
                                                    req2.payCurcyCd = StringEnum.GetStringValue(tempAuto.RecCoinType);//주문코인                                            
                                                    req2.usePrc = decimal.Parse(tempList.minPrc.ToString()) * tempAuto.OrderCnt;
                                                    if (item.marketType.Equals("KRW"))
                                                    {
                                                        req2.mkState = CommonLib.StandardCurcyNm;
                                                    }
                                                    else
                                                    {
                                                        req2.mkState = item.marketType;
                                                    }
                                                    req2.usePointPrc = 0;
                                                    req2.phsPrc = tempAuto.OrderPrc.ToString();//주문가격(사용금액)
                                                    req2.phsAmt = tempAuto.OrderCnt.ToString();//주문수량
                                                    req2.regIp = LoginDataModel.regIp;

                                                    ////자동거래 변수
                                                    //req2.tradeType = "B";
                                                    //req2.autoTradeType = "A";                                                   

                                                    using (ResponseTradingBuyModel res2 = await WebApiLib.AsyncCall<ResponseTradingBuyModel, RequestTradingBuyModel>(req2))
                                                    {
                                                        tempAuto.conclusionBuyCheck = false;
                                                        AutoTradeCheck = true;
                                                        //TrayIcon.ShowMesssage(tempAuto.OrderCoinType + " 자동구매 실행");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //체결 금액이랑 현재 시세랑 비교
                                    if (tempAuto.conclusionMaxPrc != tempList.maxPrc)
                                    {
                                        //다르면 현재 시세 입력
                                        tempAuto.conclusionMaxPrc = tempList.maxPrc;
                                        tempAuto.conclusionSellCheck = true;
                                    }

                                    if (tempAuto.conclusionSellCheck)
                                    {
                                        tempAuto.conclusionSellCheck = false;

                                        if ((tempAuto.OrderErrorLowPrc <= decimal.Parse(tempList.maxPrc.ToString()) && (decimal.Parse(tempList.maxPrc.ToString()) <= tempAuto.OrderErrorHighPrc)))
                                        {
                                            //자동 구매 체크
                                            using (RequestTradingAbleSellModel req = new RequestTradingAbleSellModel())
                                            {
                                                req.userEmail = LoginDataModel.userEmail;
                                                req.curcyCd = StringEnum.GetStringValue(tempAuto.OrderCoinType);

                                                using (ResponseTradingAbleSellModel res = WebApiLib.SyncCall<ResponseTradingAbleSellModel, RequestTradingAbleSellModel>(req))
                                                {
                                                    //판매가능수량 체크
                                                    //판매 수량 <= 현재수량 <= 판매 수량
                                                    if (tempAuto.OrderCnt <= res.data.avaSellAmt)
                                                    {
                                                        //시장가 구매실행
                                                        using (RequestTradingSellModel req2 = new RequestTradingSellModel())
                                                        {
                                                            req2.apntPhsYn = "N";//시장가
                                                            req2.userEmail = LoginDataModel.userEmail;//계정정보
                                                            req2.sellCurcyCd = StringEnum.GetStringValue(tempAuto.OrderCoinType);//주문코인
                                                            req2.rcvCurcyCd = StringEnum.GetStringValue(tempAuto.RecCoinType);//수령코인                                                            
                                                            if (item.marketType.Equals("KRW"))
                                                            {
                                                                req2.mkState = CommonLib.StandardCurcyNm;
                                                            }
                                                            else
                                                            {
                                                                req2.mkState = item.marketType;
                                                            }
                                                            req2.phsPrc = "1"; //SellMarketOrder.OrderPrc.ToString();//"주문가격";
                                                            req2.phsAmt = tempAuto.OrderCnt.ToString();//"주문수량";
                                                            req2.regIp = LoginDataModel.regIp;

                                                            req2.tradeType = "S";
                                                            req2.autoTradeType = "A";

                                                            using (ResponseTradingSellModel res2 = await WebApiLib.AsyncCall<ResponseTradingSellModel, RequestTradingSellModel>(req2))
                                                            {
                                                                tempAuto.conclusionSellCheck = false;
                                                                AutoTradeCheck = true;
                                                                //TrayIcon.ShowMesssage(tempAuto.OrderCoinType + " 자동판매 실행");
                                                            }
                                                        }
                                                    }
                                                }
                                            }
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
            finally
            {
                AutoLoop = true;
                AutoTradeCheck = true;
            }
        }

        void AutoCoinTrade(WsCoinPriceDataModel item)
        {
            try
            {
                if (AutoCoinData == null) return;

                decimal? selPrc = 0;
                decimal? chgPrc = 0;

                bool PopupCheck = false;

                foreach (var tempList in item.list)
                {
                    if (AutoCoinData.selCnKndCd.Equals(tempList.curcyCd))
                    {
                        selPrc = tempList.coinPrc;
                    }
                    if (AutoCoinData.chgCnKndCd.Equals(tempList.curcyCd))
                    {
                        chgPrc = tempList.coinPrc;
                    }
                }

                //보유, 교환코인 0% 이상
                if (AutoCoinData.selSetRate > 0 && AutoCoinData.chgSetRate > 0)
                {
                    if (AutoCoinData.selSetPrc < selPrc && AutoCoinData.chgSetPrc < chgPrc)
                    {
                        PopupCheck = true;
                    }
                }
                //보유, 교환코인 0% 이하
                else if (AutoCoinData.selSetRate < 0 && AutoCoinData.chgSetRate < 0)
                {
                    if (AutoCoinData.selSetPrc > selPrc && AutoCoinData.chgSetPrc > chgPrc)
                    {
                        PopupCheck = true;
                    }
                }
                //보유코인은 0% 이상, 교환 코인은 0% 이하
                else if (AutoCoinData.selSetRate > 0 && AutoCoinData.chgSetRate < 0)
                {
                    if (AutoCoinData.selSetPrc < selPrc && AutoCoinData.chgSetPrc > chgPrc)
                    {
                        PopupCheck = true;
                    }
                }
                //보유코인은 0% 이하, 교환 코인은 0% 이상
                else if (AutoCoinData.selSetRate < 0 && AutoCoinData.chgSetRate > 0)
                {
                    if (AutoCoinData.selSetPrc > selPrc && AutoCoinData.chgSetPrc < chgPrc)
                    {
                        PopupCheck = true;
                    }
                }

                if (PopupCheck)
                {
                    CoinExchangeToPage();
                }
                //}
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                AutoCoinTradeCheck = true;
            }
        }

        /// <summary>
        /// 메인메뉴 생성
        /// </summary>
        void CreateMenu()
        {
            try
            {
                DispatcherService.BeginInvoke(() =>
                {
                    Menus.Clear();
                    MenuModel Menuitem = new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_1"), IconPath = "/Images/ico_nav_home.png" };
                    Menuitem.Children = new ObservableCollection<MenuModel>();
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_1_1"), Id = "ExchangeDashboard", IconPath = "/Images/ico_nav_exchange.png" });
                    Menus.Add(Menuitem);

                    Menuitem = new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_2"), IconPath = "/Images/ico_nav_coin_buysell.png" };
                    Menuitem.Children = new ObservableCollection<MenuModel>();
                    //CoinTrading                   
                    //foreach (ResponseCoinListModel item in CoinData.list)
                    //{
                    //    if (!item.CoinCode.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BMC)))
                    //    {
                    //        Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString(item.CoinCode), Id = "CoinTrading", IconPath = "/Images/ico_nav_" + item.CoinName + ".png", Param = item, Certify = 1 });
                    //    }
                    //}
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_2_7"), Id = "CoinTrading", IconPath = "/Images/ico_nav_buy,sell_2.png", Certify = 1 });

                    //Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_2_5"), Id = "CoinExchangeReservationTrading", IconPath = "/Images/ico_nav_coin_exchange_auto.png", Certify = 1 });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_2_6"), Id = "TradeHistoryTrading", IconPath = "/Images/ico_nav_all_exchange.png" });

                    Menus.Add(Menuitem);

                    Menuitem = new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_3"), IconPath = "/Images/ico_nav_deposit.png" };
                    Menuitem.Children = new ObservableCollection<MenuModel>();
                    //Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_3_5"), Id = "PrepaidCardDepositWithdraw", IconPath = "/Images/ico_nav_card.png", Certify = 2 });
                    Menuitem.Children.Add(new MenuModel() { Name = string.Format(LocalizationLib.GetLocalizaionString("Main_Menu_3_1"), SC), Id = "RechargeDepositWithdraw", IconPath = "/Images/ico_nav_charge.png", Certify = 2 });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_3_2"), Id = "DepositDepositWithdraw", IconPath = "/Images/ico_nav_deposit-08.png", Certify = 2 });
                    Menuitem.Children.Add(new MenuModel() { Name = string.Format(LocalizationLib.GetLocalizaionString("Main_Menu_3_3"), SC), Id = "WithdrawDepositWithdraw", IconPath = "/Images/ico_nav_withdraw.png", Certify = 2 });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_3_4"), Id = "TransferDepositWithdraw", IconPath = "/Images/ico_nav_send.png", Certify = 2 });
                    Menus.Add(Menuitem);

                    Menuitem = new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_4"), IconPath = "/Images/ico_nav_service.png" };
                    Menuitem.Children = new ObservableCollection<MenuModel>(); 
                    //Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_4_1"), Id = "CouponAdditionalService", IconPath = "/Images/ico_nav_coupon.png" });
                    //Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_4_2"), Id = "AlarmAdditionalService", IconPath = "/Images/ico_nav_price_notice.png" });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("AlramMyPage_2_2_3"), Id = "RequestAdditionalService", IconPath = "/Images/ico_nav_auto_trade.png" });
                    //Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_4_4"), Id = "TrustRequestAdditionalService", IconPath = "/Images/ico_nav_trust.png" });               
                    //Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("ArbitrageRequest_1"), Id = "ArbitrageAdditionalService", IconPath = "/Images/ico_nav_coin_exchange_auto.png" });
                    //Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_4_5"), Id = "LendingRequestAdditionalService", IconPath = "/Images/ico_nav_lending.png" });
                    Menus.Add(Menuitem);

                    Menuitem = new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5"), IconPath = "/Images/ico_nav_customer.png" };
                    Menuitem.Children = new ObservableCollection<MenuModel>();
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_1"), Id = "NoticeHelpDesk", IconPath = "/Images/ico_nav_notice.png" });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_2"), Id = "FaqHelpDesk", IconPath = "/Images/ico_nav_faq.png" });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_3"), Id = "QnaHelpDesk", IconPath = "/Images/ico_nav_qna.png" });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_4"), Id = "FeeHelpDesk", IconPath = "/Images/ico_nav_fee.png" });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_5"), Id = "MarketCapHelpDesk", IconPath = "/Images/ico_nav_market.png" });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_6"), Id = "CoinTrendHelpDesk", IconPath = "/Images/ico_nav_coin_trends.png" });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_7"), Id = "CertificationDataSubmitHelpDesk", IconPath = "/Images/ico_nav_cert_doc.png" });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_8"), Id = "CoinInfoHelpDesk", IconPath = "/Images/ico_nav_csinfo.png" });

                    Menus.Add(Menuitem);

                    Menuitem = new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_6"), IconPath = "/Images/ico_nav_mypage.png" };
                    Menuitem.Children = new ObservableCollection<MenuModel>();
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_6_1"), Id = "MemberInfoMyPage", IconPath = "/Images/ico_nav_userinfo_manage.png" });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_6_2"), Id = "CertifyMyPage", IconPath = "/Images/ico_nav_cert_center.png" });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_6_3"), Id = "LoginHistoryMyPage", IconPath = "/Images/ico_nav_connect.png" });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_6_4"), Id = "AssetsMyPage", IconPath = "/Images/ico_nav_asset.png" });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_6_5"), Id = "TradingHistoryMyPage", IconPath = "/Images/ico_nav_my_trade.png" });
                    Menuitem.Children.Add(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_6_6"), Id = "AlarmMyPage", IconPath = "/Images/ico_nav_alarm.png" });
                    Menus.Add(Menuitem);

                    //메뉴 3뎁스 테스트
                    //ObservableCollection<MenuModel> Menuitem1 = new ObservableCollection<MenuModel>();
                    //Menuitem1.Add(new MenuModel() { Name = "마이페이지_1_1", Id = "View1" });
                    //Menuitem1.Add(new MenuModel() { Name = "마이페이지_1_2", Id = "View1" });
                    //Menuitem = new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_6"), IconPath = "/Image/ico_gnb_mypage.png" };
                    //Menuitem.Children = new ObservableCollection<MenuModel>();
                    //Menuitem.Children.Add(new MenuModel() { Name = "회원정보관리", Id = "View1", Children = Menuitem1 });
                    //Menus.Add(Menuitem);
                });
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 빠른 메뉴 생성
        /// </summary>
        async void CreateQuickMenu()
        {
            try
            {
                ObservableCollection<ResponseBookMarkMenuListModel> sMenuList;
                using (RequestBookMarkMenuModel req = new RequestBookMarkMenuModel())
                {
                    req.userEmail = LoginDataModel.userEmail;

                    using (ResponseBookMarkMenuModel res = await WebApiLib.AsyncCall<ResponseBookMarkMenuModel, RequestBookMarkMenuModel>(req))
                    {
                        sMenuList = res.data.list;
                        sMenuList = new ObservableCollection<ResponseBookMarkMenuListModel>(sMenuList.OrderBy(x => x.sn));
                    }
                }

                DispatcherService.BeginInvoke(() =>
                {
                    QuickMenus.Clear();

                    if (sMenuList.Count.Equals(0))
                    {
                        //foreach (ResponseCoinListModel item in CoinData.list)
                        //{
                        //    if (!item.CoinCode.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BMC)))
                        //    {
                        //        QuickMenus.Add(new MenuModel() { NickName = LocalizationLib.GetLocalizaionString(item.CoinCode + "_Quick"), Name = LocalizationLib.GetLocalizaionString(item.CoinCode), Id = "CoinTrading", IconPath = "/Images/ico_nav_" + item.CoinName + ".png", ToolTip = LocalizationLib.GetLocalizaionString(item.CoinCode + "_Quick_ToolTip"), Param = item, Certify = 1 });
                        //    }
                        //}
                        QuickMenus.Add(new MenuModel() { NickName = LocalizationLib.GetLocalizaionString("Main_QuickMenu_20"), Name = LocalizationLib.GetLocalizaionString("Main_Menu_2_7"), Id = "CoinTrading", IconPath = "/Images/ico_nav_buy,sell_2.png", Certify = 1 });
                        QuickMenus.Add(new MenuModel() { NickName = string.Format(LocalizationLib.GetLocalizaionString("Main_QuickMenu_1"), SC), Name = string.Format(LocalizationLib.GetLocalizaionString("Main_Menu_3_1"), SC), Id = "RechargeDepositWithdraw", IconPath = "/Images/ico_nav_charge.png", ToolTip = string.Format(LocalizationLib.GetLocalizaionString("Main_QuickMenu_ToolTip_11"), SC), Certify = 2 });
                        QuickMenus.Add(new MenuModel() { NickName = string.Format(LocalizationLib.GetLocalizaionString("Main_QuickMenu_2"), SC), Name = string.Format(LocalizationLib.GetLocalizaionString("Main_Menu_3_3"), SC), Id = "WithdrawDepositWithdraw", IconPath = "/Images/ico_nav_withdraw.png", ToolTip = string.Format(LocalizationLib.GetLocalizaionString("Main_QuickMenu_ToolTip_12"), SC), Certify = 2 });
                        QuickMenus.Add(new MenuModel() { NickName = LocalizationLib.GetLocalizaionString("Main_QuickMenu_7"), Name = LocalizationLib.GetLocalizaionString("Main_Menu_3_2"), Id = "DepositDepositWithdraw", IconPath = "/Images/ico_nav_deposit-08.png", Certify = 2 });
                        QuickMenus.Add(new MenuModel() { NickName = LocalizationLib.GetLocalizaionString("Main_QuickMenu_8"), Name = LocalizationLib.GetLocalizaionString("Main_Menu_3_4"), Id = "TransferDepositWithdraw", IconPath = "/Images/ico_nav_send.png", Certify = 2 });
                        QuickMenus.Add(new MenuModel() { NickName = LocalizationLib.GetLocalizaionString("Main_QuickMenu_9"), Name = LocalizationLib.GetLocalizaionString("Main_Menu_6_5"), Id = "TradingHistoryMyPage", IconPath = "/Images/ico_nav_my_trade.png" });
                        QuickMenus.Add(new MenuModel() { NickName = LocalizationLib.GetLocalizaionString("Main_QuickMenu_10"), Name = LocalizationLib.GetLocalizaionString("Main_Menu_6_4"), Id = "AssetsMyPage", IconPath = "/Images/ico_nav_asset.png" });
                        QuickMenus.Add(new MenuModel() { NickName = LocalizationLib.GetLocalizaionString("Main_QuickMenu_12"), Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_3"), Id = "QnaHelpDesk", IconPath = "/Images/ico_nav_qna.png" });
                    }
                    else
                    {
                        foreach (var item in sMenuList)
                        {
                            string QuickMenu = StringEnum.ToEnum<EnumLib.MenuCode>(item.menuCd).ToString();
                            //코인메뉴 자동 추가                                    
                            if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.BuySell)))
                                QuickMenus.Add(new MenuModel() { NickName = LocalizationLib.GetLocalizaionString("Main_QuickMenu_20"), Name = LocalizationLib.GetLocalizaionString("Main_Menu_2_7"), Id = "CoinTrading", IconPath = "/Images/ico_nav_buy,sell_2.png", Certify = 1 });
                            else if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.Recharge)))
                                QuickMenus.Add(new MenuModel() { NickName = string.Format(LocalizationLib.GetLocalizaionString("Main_QuickMenu_1"), SC), Name = string.Format(LocalizationLib.GetLocalizaionString("Main_Menu_3_1"), SC), Id = "RechargeDepositWithdraw", IconPath = "/Images/ico_nav_charge.png", ToolTip = LocalizationLib.GetLocalizaionString("Main_QuickMenu_ToolTip_11"), Certify = 2 });
                            else if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.Withdraw)))
                                QuickMenus.Add(new MenuModel() { NickName = string.Format(LocalizationLib.GetLocalizaionString("Main_QuickMenu_2"), SC), Name = string.Format(LocalizationLib.GetLocalizaionString("Main_Menu_3_3"), SC), Id = "WithdrawDepositWithdraw", IconPath = "/Images/ico_nav_withdraw.png", ToolTip = LocalizationLib.GetLocalizaionString("Main_QuickMenu_ToolTip_12"), Certify = 2 });
                            else if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.Deposit)))
                                QuickMenus.Add(new MenuModel() { NickName = LocalizationLib.GetLocalizaionString("Main_QuickMenu_7"), Name = LocalizationLib.GetLocalizaionString("Main_Menu_3_2"), Id = "DepositDepositWithdraw", IconPath = "/Images/ico_nav_deposit-08.png", Certify = 2 });
                            else if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.Transfer)))
                                QuickMenus.Add(new MenuModel() { NickName = LocalizationLib.GetLocalizaionString("Main_QuickMenu_8"), Name = LocalizationLib.GetLocalizaionString("Main_Menu_3_4"), Id = "TransferDepositWithdraw", IconPath = "/Images/ico_nav_send.png", Certify = 2 });
                            else if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.TradeHistory)))
                                QuickMenus.Add(new MenuModel() { NickName = LocalizationLib.GetLocalizaionString("Main_QuickMenu_9"), Name = LocalizationLib.GetLocalizaionString("Main_Menu_6_5"), Id = "TradingHistoryMyPage", IconPath = "/Images/ico_nav_my_trade.png" });
                            else if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.MyAssets)))
                                QuickMenus.Add(new MenuModel() { NickName = LocalizationLib.GetLocalizaionString("Main_QuickMenu_10"), Name = LocalizationLib.GetLocalizaionString("Main_Menu_6_4"), Id = "AssetsMyPage", IconPath = "/Images/ico_nav_asset.png" });
                            else if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.QnA)))
                                QuickMenus.Add(new MenuModel() { NickName = LocalizationLib.GetLocalizaionString("Main_QuickMenu_12"), Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_3"), Id = "QnaHelpDesk", IconPath = "/Images/ico_nav_qna.png" });

                        }
                    }
                });
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 메뉴 클릭 해당 페이지 호출(View 단에서 사용 MenuSelectedCommand)
        /// </summary>
        public async void MenuSelected(MenuModel menu)
        {
            try
            {
                var task = Task.Run(() =>
                {
                    DispatcherService.BeginInvoke(() =>
                    {
                        if (menu.Certify != 0)
                        {
                            if (menu.Param != null)
                            {
                                if (menu.Id.Equals("CoinTrading"))
                                {
                                    if (((ResponseCoinListModel)menu.Param).CoinCode.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.SGC)))
                                    {
                                        Alert alert = new Alert(Localization.Resource.Common_Alert_1);
                                        alert.ShowDialog();
                                        return;
                                    }
                                    else if (((ResponseCoinListModel)menu.Param).CoinCode.Equals(StringEnum.GetStringValue(EnumLib.ExchangeCurrencyCode.BMC)))
                                    {
                                        Alert alert = new Alert(Localization.Resource.Common_Alert_1);
                                        alert.ShowDialog();
                                        return;
                                    }
                                }                               
                            }               
                        }

                        if (menu.Id.Equals("ArbitrageAdditionalService"))
                        {
                            using (RequestArbitrageContentModel req = new RequestArbitrageContentModel())
                            {
                                req.userEmail = MainViewModel.LoginDataModel.userEmail;

                                using (ResponseArbitrageContentModel res = WebApiLib.SyncCallEncrypt<ResponseArbitrageContentModel, RequestArbitrageContentModel>(req))
                                {
                                    if (res.resultStrCode == "000")
                                    {
                                        if (res.data.cnKndCd == "")
                                        {
                                            Alert alert = new Alert(Localization.Resource.Arbitrage_SignUpPop_Alert_1, 300, 160);
                                            alert.ShowDialog();
                                            return;
                                        }
                                    }
                                }
                            }
                        }

                        string id = menu.Id;
                        if (menu.Param != null)
                        {
                            if (menu.Id.Equals("CoinTrading"))
                            {
                                id = ((ResponseCoinListModel)menu.Param).CoinName;
                            }
                        }

                        IDocument doc = DocumentManagerService.Documents.FirstOrDefault(x => x.Id.ToString() == id);
                        if (doc == null)
                        {
                            doc = DocumentManagerService.CreateDocument(menu.Id, menu.Param, this);
                            doc.DestroyOnClose = false;
                            doc.Id = id;
                            doc.Title = menu.Name;
                        }
                        doc.Show();

                    });
                });
                await task;
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

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void NoticeToPage()
        {
            try
            {
                MenuSelected(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_1"), Id = "NoticeHelpDesk" });
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void BookMarkToPage()
        {
            try
            {
                if (bm == null || bm.IsLoaded == false)
                {
                    bm = new BookMark();
                    bm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    bm.Show();
                }
                else
                {
                    bm.Show();
                    bm.Activate();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void CoinExchangeToPage()
        {
            try
            {
                DispatcherService.BeginInvoke(() =>
                {
                    CoinExchangePopUpTrading coin = new CoinExchangePopUpTrading();
                    coin.Show();
                });

                using (RequestSendPushModel req = new RequestSendPushModel())
                {
                    req.userEmail = LoginDataModel.userEmail;
                    req.regIp = LoginDataModel.regIp;
                    req.contents = Localization.Resource.CoinExchangeReservationTrading_Alert_4;
                    req.pushType = StringEnum.GetStringValue(EnumLib.PushCode.CoinExchange);
                    using (ResponseSendPushModel res = await WebApiLib.AsyncCall<ResponseSendPushModel, RequestSendPushModel>(req))
                    {
                        //푸쉬성공
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void CoinCheck()
        {
            try
            {
                using (RequestCoinCheckModel req = new RequestCoinCheckModel())
                {
                    req.userEmail = LoginDataModel.userEmail;

                    using (ResponseCoinCheckModel res = await WebApiLib.AsyncCall<ResponseCoinCheckModel, RequestCoinCheckModel>(req))
                    {
                        //코인확인 API 조회 성공
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

                btn_bookmark = sLanguage + "btn_bookmark.png";
                btn_bookmark_on = sLanguage + "btn_bookmark_on.png";
                btn_quick_button_notice = sLanguage + "btn_quick_button_notice.png";
                btn_quick_button_notice_on = sLanguage + "btn_quick_button_notice_on.png";

                Main_Head_1 = LocalizationLib.GetLocalizaionString("Main_Head_1");
                Main_Head_2 = LocalizationLib.GetLocalizaionString("Main_Head_2");
                Main_Head_3 = LocalizationLib.GetLocalizaionString("Main_Head_3");
                Main_Head_4 = string.Format(LocalizationLib.GetLocalizaionString("Main_Head_4"), SC);
                Main_Head_5 = LocalizationLib.GetLocalizaionString("Main_Head_5");
                Main_Head_6 = LocalizationLib.GetLocalizaionString("Main_Head_6");                
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public class MkCoinList
        {
            public ObservableCollection<ResponseGetMkCoinListListtModel> KRW = new ObservableCollection<ResponseGetMkCoinListListtModel>();
            public ObservableCollection<ResponseGetMkCoinListListtModel> ETH = new ObservableCollection<ResponseGetMkCoinListListtModel>();
            public ObservableCollection<ResponseGetMkCoinListListtModel> USDT = new ObservableCollection<ResponseGetMkCoinListListtModel>();
            public ObservableCollection<ResponseGetMkCoinListListtModel> BTC = new ObservableCollection<ResponseGetMkCoinListListtModel>();
        }
    }
}