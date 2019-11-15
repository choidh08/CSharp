using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestUserInsUdtModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }
        public virtual string userPhone { get; set; }
        public virtual string userPwd { get; set; }
        public virtual string userNm { get; set; }
        public virtual string userMobile { get; set; }
        public virtual string nickNm { get; set; }
        public virtual string postCd { get; set; }
        public virtual string adrs { get; set; }
        public virtual string dtlAdrs { get; set; }
        public virtual string useYn { get; set; }
        public virtual string birthDay { get; set; }
        public virtual string pwdChgYn { get; set; }
        public virtual string tmpPwd { get; set; }
        public virtual string pushToken { get; set; }
        public virtual string signDt { get; set; }
        public virtual string regIp { get; set; }
        public virtual string gender { get; set; }
        public virtual string country { get; set; }
        public virtual string langCd { get; set; }
        public virtual string lockYn { get; set; }
        public virtual string shHaveYn { get; set; }
        public virtual string brhCode { get; set; }
        public virtual string rcmdCode { get; set; }
        public virtual string birthType { get; set; }

        public RequestUserInsUdtModel() : base("bt.auth.userInsUpt.dp/proc.go") { }
    }
}