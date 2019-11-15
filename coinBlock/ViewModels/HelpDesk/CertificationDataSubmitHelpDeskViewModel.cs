using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Model.HelpDesk;
using coinBlock.Utility;
using System.Collections.ObjectModel;
using coinBlock.ViewModels.DepositWithdraw;
using coinBlock.Model.MyPage;
using coinBlock.Views;
using System.Windows.Forms;
using System.IO;
using coinBlock.Model.Common;

namespace coinBlock.ViewModels.HelpDesk
{
    [POCOViewModel]
    public class CertificationDataSubmitHelpDeskViewModel
    {
        public virtual ObservableCollection<ComboBoxStrData> ParentCode { get; set; }
        public virtual ComboBoxStrData SelParent { get; set; }
        public virtual ObservableCollection<ComboBoxStrData> ChildCode { get; set; }
        public virtual ComboBoxStrData SelChild { get; set; }
        public virtual ObservableCollection<ResponseCertContentListModel> CertSubmitList { get; set; }

        public virtual string userEmail { get; set; } = MainViewModel.LoginDataModel.userEmail;
        public virtual string userMobile { get; set; }
        public virtual string userReqMessage { get; set; } = string.Empty;

        public virtual string sFileName1 { get; set; }
        public virtual string sFileName2 { get; set; }
        public virtual string sFileName3 { get; set; }

        public virtual string certified_img01 { get; set; }
        public virtual string certified_img02 { get; set; }
        public virtual string certified_img03 { get; set; }
        public virtual string btn_file_upload { get; set; }
        public virtual string btn_file_upload_on { get; set; }
        public virtual string btn_cert_request { get; set; }


        public virtual bool IsBusy { get; set; }
        Alert alert = null;// new Alert();

        protected CertificationDataSubmitHelpDeskViewModel()
        {            
            ImageSet();
        }

        public static CertificationDataSubmitHelpDeskViewModel Create()
        {
            return ViewModelSource.Create(() => new CertificationDataSubmitHelpDeskViewModel());
        }

        public void Loaded()
        {
            try
            {
                GetUserInfo();
                GetCertList();
                GetCmmcCode(string.Empty);
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
                Clear();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void OnSelParentChanged()
        {
            try
            {
                if(SelParent?.Value != null)
                    GetCmmcCode(SelParent.Value);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetCmmcCode(string Code)
        {
            try
            {
                using (RequestCertSelectCodeModel req = new RequestCertSelectCodeModel())
                {
                    req.cmmUpperCd = Code;

                    using (ResponseCertSelectCodeModel res = await WebApiLib.AsyncCall<ResponseCertSelectCodeModel, RequestCertSelectCodeModel>(req))
                    {
                        if(res.resultStrCode == "000")
                        {
                            if(string.IsNullOrWhiteSpace(Code))
                            {
                                ParentCode = new ObservableCollection<ComboBoxStrData>();
                                foreach (ResponseCertSelectCodeListModel item in res.data.list)
                                {
                                    ParentCode.Add(new ComboBoxStrData { Name = LocalizationLib.GetLocalizaionString("CertificationDataSubmit_" + item.cmmCd), Value = item.cmmCd });
                                }
                                if (ParentCode.Count > 0)
                                {
                                    SelParent = ParentCode[0];
                                }
                            }
                            else
                            {
                                ChildCode = new ObservableCollection<ComboBoxStrData>();
                                ChildCode.Add(new ComboBoxStrData { Name = Localization.Resource.Common_Alert_16, Value = "00" });
                                foreach (ResponseCertSelectCodeListModel item in res.data.list)
                                {
                                    ChildCode.Add(new ComboBoxStrData { Name = LocalizationLib.GetLocalizaionString("CertificationDataSubmit_" + item.cmmCd), Value = item.cmmCd });
                                }
                                if (ChildCode.Count > 0)
                                {
                                    SelChild = ChildCode[0];
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

        public async void GetUserInfo()
        {
            try
            {
                using (RequestGetUserInfoModel req = new RequestGetUserInfoModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseGetUserInfoModel res = await WebApiLib.AsyncCall<ResponseGetUserInfoModel, RequestGetUserInfoModel>(req))
                    {
                        userMobile = res.data.userMobile;                
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public async void GetCertList()
        {
            try
            {
                using (RequestCertContentModel req = new RequestCertContentModel())
                {
                    req.userEmail = userEmail;

                    using (ResponseCertContentModel res = await WebApiLib.AsyncCall<ResponseCertContentModel, RequestCertContentModel>(req))
                    {
                        if(res.resultStrCode =="000")
                        {
                            for (int i = 0; i < res.data.list.Count; i++)
                            {
                                res.data.list[i].no = (i + 1).ToString();
                            }

                            CertSubmitList = res.data.list;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdCertSubmit()
        {
            try
            {
                IsBusy = true;

                if (SelChild.Value.Equals("00"))
                {
                    alert = new Alert(Localization.Resource.CertificationDataSubmit_Pop_2);
                    alert.ShowDialog();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(sFileName1) || string.IsNullOrWhiteSpace(sFileName2))
                {
                    alert = new Alert(Localization.Resource.CertifyMyPage_6_1);
                    alert.ShowDialog();
                    return;
                }
                else if (userReqMessage.Length > 500)
                {
                    alert = new Alert(Localization.Resource.CertificationDataSubmit_Pop_3);
                    alert.ShowDialog();
                    return;
                }

                using (RequestCertDataSubmitModel req = new RequestCertDataSubmitModel())
                {
                    req.userEmail = userEmail;
                    req.userMobile = userMobile;
                    req.certGrade = SelParent.Value;
                    req.certSubGrade = SelChild.Value;
                    req.certMsg = userReqMessage;

                    using (ResponseCertDataSubmitModel res = WebApiLib.SyncCall<ResponseCertDataSubmitModel, RequestCertDataSubmitModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            string sCertCode = res.data.certCode;

                            try
                            {
                                using (RequestCertFileUploadModel req2 = new RequestCertFileUploadModel())
                                {
                                    req2.userEmail = userEmail;
                                    req2.fileSn = "1";
                                    req2.certCode = sCertCode;

                                    using (ResponseCertFileUploadModel res2 = WebApiLib.UpLoad<ResponseCertFileUploadModel, RequestCertFileUploadModel>(req2, sFileName1))
                                    {
                                        if (res2.resultStrCode == "000")
                                        {
                                            using (RequestCertFileUploadModel req3 = new RequestCertFileUploadModel())
                                            {
                                                req3.userEmail = userEmail;
                                                req3.fileSn = "2";
                                                req3.certCode = sCertCode;

                                                using (ResponseCertFileUploadModel res3 = WebApiLib.UpLoad<ResponseCertFileUploadModel, RequestCertFileUploadModel>(req3, sFileName2))
                                                {
                                                    if (res3.resultStrCode == "000")
                                                    {
                                                        if (!string.IsNullOrWhiteSpace(sFileName3))
                                                        {
                                                            using (RequestCertFileUploadModel req4 = new RequestCertFileUploadModel())
                                                            {
                                                                req4.userEmail = userEmail;
                                                                req4.fileSn = "3";
                                                                req4.certCode = sCertCode;

                                                                using (ResponseCertFileUploadModel res4 = WebApiLib.UpLoad<ResponseCertFileUploadModel, RequestCertFileUploadModel>(req4, sFileName3))
                                                                {
                                                                    if (res4.resultStrCode == "000")
                                                                    {
                                                                        IsBusy = false;

                                                                        bool bCheck = true;
                                                                        if (SelParent.Value.Equals("CMMP00000000142") && SelChild.Value.Equals("CMMP00000000986"))
                                                                        {
                                                                            bCheck = AuthOtpYn();
                                                                        }

                                                                        if (bCheck)
                                                                        {
                                                                            alert = new Alert(Localization.Resource.CertificationDataSubmit_Pop_1);
                                                                            alert.ShowDialog();

                                                                            Clear();
                                                                            GetCertList();
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            IsBusy = false;

                                                            bool bCheck = true;
                                                            if (SelParent.Value.Equals("CMMP00000000142") && SelChild.Value.Equals("CMMP00000000986"))
                                                            {
                                                                bCheck = AuthOtpYn();
                                                            }

                                                            if (bCheck)
                                                            {
                                                                alert = new Alert(Localization.Resource.CertificationDataSubmit_Pop_1);
                                                                alert.ShowDialog();

                                                                Clear();
                                                                GetCertList();
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
                                alert = new Alert(Localization.Resource.CertifyMyPage_Alert_7, 300);
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
            finally
            {
                IsBusy = false;
            }
        }

        public bool AuthOtpYn()
        {
            try
            {
                using (RequestUserCertModel req = new RequestUserCertModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.otpCertYn = "N";

                    using (ResponseUserCertModel res = WebApiLib.SyncCall<ResponseUserCertModel, RequestUserCertModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                return false;
            }
        }

        public void CmdFileUpload(string sNum)
        {
            try
            {                
                OpenFileDialog file = new OpenFileDialog();
                file.RestoreDirectory = true;
                file.Multiselect = false;                
                file.Filter = "Images Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";

                if (file.ShowDialog() == DialogResult.OK)
                {
                    IsBusy = true;

                    long size = new FileInfo(file.FileName).Length;
                    long mb = size / 1024 / 1024;

                    if (mb >= 2)
                    {
                        Alert alert = new Alert(Localization.Resource.CertifyMyPage_6_3, 320);
                        alert.ShowDialog();
                        return;
                    }

                    if (sNum.Equals("1"))
                    {
                        sFileName1 = file.FileName;
                    }
                    else if (sNum.Equals("2"))
                    {
                        sFileName2 = file.FileName;
                    }
                    else if (sNum.Equals("3"))
                    {
                        sFileName3 = file.FileName;
                    }

                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void Clear()
        {
            try
            {
                GetCmmcCode(string.Empty);
                userReqMessage = string.Empty;
                sFileName1 = string.Empty;
                sFileName2 = string.Empty;
                sFileName3 = string.Empty;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            certified_img01 = sLanguage + "certified_img01.png";
            certified_img02 = sLanguage + "certified_img02.png";
            certified_img03 = sLanguage + "certified_img03.png";
            btn_file_upload = sLanguage + "btn_file_upload.png";
            btn_file_upload_on = sLanguage + "btn_file_upload_on.png";
            btn_cert_request = sLanguage + "btn_kyc_cert_request.png";
        }
    }
}