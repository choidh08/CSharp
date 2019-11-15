using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestGetNatnBankModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public virtual string langCd { get; set; }

        public RequestGetNatnBankModel() : base("bt.getNatnBankList.dp/proc.go") { }
    }
}