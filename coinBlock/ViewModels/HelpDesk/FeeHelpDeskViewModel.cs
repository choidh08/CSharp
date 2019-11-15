using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Model;
using coinBlock.Utility;
using coinBlock.Model.HelpDesk;
using System.Windows;
using coinBlock.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace coinBlock.ViewModels.HelpDesk
{
    [POCOViewModel]
    public class FeeHelpDeskViewModel
    {
        public virtual List<ResponseGetCoinMaxMinListModel> FeeList { get; set; }
        public virtual List<ResponseGetCoinMaxMinListModel> FeeNotKrwList { get; set; }
        public virtual int totalHeight { get; set; }
        public virtual int totalSubHeight { get; set; }
     
        public virtual Visibility Cert2Visible { get; set; } = Visibility.Visible;
        public virtual Visibility Cert2CompVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility Cert3Visible { get; set; } = Visibility.Visible;
        public virtual Visibility Cert3CompVisible { get; set; } = Visibility.Collapsed;

        public virtual decimal mkKRW { get; set; }
        public virtual decimal mkUSDT { get; set; }
        public virtual decimal mkETH { get; set; }
        public virtual decimal mkBTC { get; set; }
        public virtual decimal krwFee { get; set; }
        public virtual decimal atmFee { get; set; }
        public virtual string SC { get; } = CommonLib.StandardCurcyNm;

        protected FeeHelpDeskViewModel()
        {
            //GetData();
        }

        public static FeeHelpDeskViewModel Create()
        {
            return ViewModelSource.Create(() => new FeeHelpDeskViewModel());
        }

        public void Loaded()
        {
            try
            {
                if (string.IsNullOrWhiteSpace((MainViewModel.LoginDataModel.otpNo)))
                {
                    Cert2Visible = Visibility.Visible;
                    Cert2CompVisible = Visibility.Collapsed;                    
                }
                else
                {
                    Cert2Visible = Visibility.Collapsed;
                    Cert2CompVisible = Visibility.Visible;
                }
                
                if (!MainViewModel.LoginDataModel.kycStatus.Equals("1"))
                {
                    Cert3Visible = Visibility.Visible;
                    Cert3CompVisible = Visibility.Collapsed;
                }
                else
                {
                    Cert3Visible = Visibility.Collapsed;
                    Cert3CompVisible = Visibility.Visible;
                }

                GetData();
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
                using (RequestWithdrawFeeModel req = new RequestWithdrawFeeModel())
                {
                    using (ResponseWithdrawFeeModel res = await WebApiLib.AsyncCall<ResponseWithdrawFeeModel, RequestWithdrawFeeModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            ObservableCollection<ResponseWithdrawFeeListModel> tempFee = new ObservableCollection<ResponseWithdrawFeeListModel>();
                            tempFee = res.data.list;
                            if (tempFee.Contains(tempFee.Where(x => x.curcyCd.Equals("CMMC00000001165")).First()))
                            {
                                atmFee = tempFee.Where(x => x.curcyCd.Equals("CMMC00000001165")).First().fee;
                            }
                            if (tempFee.Contains(tempFee.Where(x => x.curcyCd.Equals("CMMC00000000204")).First()))
                            {
                                krwFee = tempFee.Where(x => x.curcyCd.Equals("CMMC00000000204")).First().fee;
                            }
                        }
                    }
                }

                using (RequestGetCoinMaxMinModel req = new RequestGetCoinMaxMinModel())
                {
                    using (ResponseGetCoinMaxMinModel res = await WebApiLib.AsyncCall<ResponseGetCoinMaxMinModel, RequestGetCoinMaxMinModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            mkKRW = res.data.mkKRW;
                            mkUSDT = res.data.mkUSDT;
                            mkETH = res.data.mkETH;
                            mkBTC = res.data.mkBTC;

                            if (res.data.list.Count > 0)
                            {
                                ResponseGetCoinMaxMinListModel baseTemp = res.data.list.Where(x => x.cnkndnm == CommonLib.StandardCurcyNm).FirstOrDefault();

                                List<ResponseGetCoinMaxMinListModel> temp = res.data.list.Where(x => x.cnkndnm != "USDT" && x.cnkndnm != CommonLib.StandardCurcyNm).ToList();
                                temp.Insert(0, baseTemp);                          

                                FeeList = temp;
                                totalHeight = 150 + ((FeeList.Count * 30) * 4) - 30;

                                temp.RemoveAt(0); 
                                FeeNotKrwList = temp;
                                totalSubHeight = FeeNotKrwList.Count * 30;
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

        public void CmdGoCertifyPage()
        {
            try
            {
                Messenger.Default.Send(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_6_2"), Id = "CertifyMyPage", IconPath = "/Images/ico_nav_cert_center.png" });
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}