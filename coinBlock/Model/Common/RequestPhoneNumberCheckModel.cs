using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestPhoneNumberCheckModel : RequestBaseModel
    {
        public string userMobile { get; set; }

        public RequestPhoneNumberCheckModel() : base("bt.auth.mobileOverlapChk.dp/proc.go") { }
    }
}