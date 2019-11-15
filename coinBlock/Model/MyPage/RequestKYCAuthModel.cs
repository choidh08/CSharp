using System;
using DevExpress.Mvvm;
using System.Windows.Media.Imaging;

namespace coinBlock.Model.MyPage
{
    public class RequestKYCAuthModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public virtual string fileSn { get; set; }

        public RequestKYCAuthModel() : base("bt.auth.uploadKycFileOne.dp/proc.go") { }
    }
}