using coinBlock.Model.AdditionalService;
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

namespace coinBlock.Views.AdditionalService.Popup
{
    /// <summary>
    /// Interaction logic for LendingRequestReapyPopup.xaml
    /// </summary>
    public partial class LendingRequestReapyPopup : Window
    {
        Alert alert = null;
        public string _RenDftCode = string.Empty;

        public LendingRequestReapyPopup()
        {
            InitializeComponent();
        }

        public LendingRequestReapyPopup(string renDftCode)
        {
            InitializeComponent();
            _RenDftCode = renDftCode;
            this.Loaded += LendingRequestReapyPopup_Loaded;
        }

        private void LendingRequestReapyPopup_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (RequestLendingRepayModel req = new RequestLendingRepayModel())
                {
                    req.renDftCode = _RenDftCode;

                    using (ResponseLendingRepayModel res = WebApiLib.SyncCall<ResponseLendingRepayModel, RequestLendingRepayModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            txtApplyAmt.Text = res.data.applyAmt.ToString() + " " + res.data.cnKndNm;
                            txtRemainInterest.Text = res.data.remainInterest.ToString("#,0.########") + " " + res.data.cnKndNm;
                            txtRepayAmt.Text = res.data.totRepayAmt.ToString("#,0.########") + " " + res.data.cnKndNm;
                            txtRepayDt.Text = res.data.repayDt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                alert = new Alert(Localization.Resource.Lending_Alert_15, Alert.ButtonType.YesNo);
                if (alert.ShowDialog() == true)
                {
                    //상환하기 API
                    using (RequestLendingRepayPopModel req = new RequestLendingRepayPopModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.renDftCode = _RenDftCode;
                        req.regIp = MainViewModel.LoginDataModel.regIp;

                        using (ResponseLendingRepayPopModel res = WebApiLib.SyncCall<ResponseLendingRepayPopModel, RequestLendingRepayPopModel>(req))
                        {
                            if (res.resultStrCode == "000")
                            {
                                string resultCd = res.data.failCd;

                                if (resultCd.Equals(""))
                                {
                                    this.DialogResult = true;
                                    this.Close();
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

        private void No_Click(object sender, RoutedEventArgs e)
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
