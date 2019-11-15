using coinBlock.Model.DepositWithdraw;
using coinBlock.Utility;
using coinBlock.ViewModels;
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

namespace coinBlock.Views.DepositWithdraw.Popup
{
    /// <summary>
    /// Interaction logic for CurrencyTransferPop.xaml
    /// </summary>
    public partial class CurrencyTransferPop : Window
    {
        decimal _dTransferAmount = 0;
        Alert alert = null;

        public CurrencyTransferPop(string sCheckedNm, string sCheckedId, decimal dTransferAmount)
        {
            InitializeComponent();
            txtUserNm.Text = sCheckedNm;
            txtUserId.Text = sCheckedId;
            txtTransferDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            txtTransferAmount.Text = dTransferAmount.ToString("#,#") + " " + CommonLib.StandardCurcyNm;
            _dTransferAmount = dTransferAmount;
        }

        private void uiComfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (RequestCurrTransferModel req = new RequestCurrTransferModel())
                {
                    req.fromUserEmail = MainViewModel.LoginDataModel.userEmail;
                    req.toUserEmail = txtUserId.Text;
                    req.wdrPrc = _dTransferAmount;
                    req.regIp = MainViewModel.LoginDataModel.regIp;

                    using (ResponseCurrTransferModel res = WebApiLib.SyncCall<ResponseCurrTransferModel, RequestCurrTransferModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            string resultCd = res.data.failCd;

                            if (resultCd.Equals(""))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_8);
                                alert.ShowDialog();

                                this.DialogResult = true;
                                this.Close();
                            }
                            else if (resultCd.Equals("999"))
                            {
                                alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_18, 320);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("996"))
                            {
                                alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_21, 320);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("994"))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_19);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("993"))
                            {
                                alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_23);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("992"))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_15);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("991"))
                            {
                                alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_39);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("990"))
                            {
                                alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_27);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("989"))
                            {
                                alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_T_Common_6);
                                alert.ShowDialog();
                            }
                            else if(resultCd.Equals("984"))
                            {
                                alert = new Alert(string.Format(Localization.Resource.WithdrawDepositWithdraw_T_Common_7 + "\n" + Localization.Resource.WithdrawDepositWithdraw_T_Common_8, CommonLib.StandardCurcyNm), 330);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("983"))
                            {
                                alert = new Alert(Localization.Resource.WithdrawDepositWithdraw_Common_40 + "\n" + Localization.Resource.WithdrawDepositWithdraw_Common_41, 320);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("982"))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_11, 320);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("981"))
                            {
                                alert = new Alert(Localization.Resource.TransferDepositWithdraw_Common_11, 320);
                                alert.ShowDialog();
                            }
                            else if (resultCd.Equals("979"))
                            {
                                alert = new Alert(Localization.Resource.Common_Alert_1);
                                alert.ShowDialog();
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

        private void uiExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
                this.Close();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}
