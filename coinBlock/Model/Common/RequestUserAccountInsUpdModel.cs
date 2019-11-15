using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestUserAccountInsUpdModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }
        public virtual string bankCd { get; set; }
        public virtual string bankAccNo { get; set; }
        public virtual string accntNm { get; set; }
        public virtual string useYn { get; set; }
        public virtual string regIp { get; set; }

        public RequestUserAccountInsUpdModel() : base("bt.uptUserBankInfo.dp/proc.go") { }
    }
}