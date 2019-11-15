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
    /// Interaction logic for LendingRequestExtendPopupViewModel.xaml
    /// </summary>
    public partial class LendingRequestExtendPopup : Window
    {
        Alert alert = null;
        public string _RenDftCode = string.Empty;

        public LendingRequestExtendPopup()
        {
            InitializeComponent();
        }

        public LendingRequestExtendPopup(string renDftCode)
        {
            InitializeComponent();
            _RenDftCode = renDftCode;
            this.Loaded += LendingRequestExtendPopup_Loaded;
        }

        private void LendingRequestExtendPopup_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (RequestLendingExtendModel req = new RequestLendingExtendModel())
                {
                    req.renDftCode = _RenDftCode;

                    using (ResponseLendingExtendModel res = WebApiLib.SyncCall<ResponseLendingExtendModel, RequestLendingExtendModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            txtExtensionRate.Text = string.Format(Localization.Resource.Lending_Alert_1_2, res.data.extensionRate);
                            txtExtensionDt.Text = res.data.extensionDt.ToString();
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
                alert = new Alert(Localization.Resource.Lending_Alert_14, Alert.ButtonType.YesNo);
                if (alert.ShowDialog() == true)
                {
                    //연장하기 API
                    using (RequestLendingExtendPopModel req = new RequestLendingExtendPopModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;
                        req.mthCmt = "12";
                        req.renDftCode = _RenDftCode;

                        using (ResponseLendingExtendPopModel res = WebApiLib.SyncCall<ResponseLendingExtendPopModel, RequestLendingExtendPopModel>(req))
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

        private void No_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
