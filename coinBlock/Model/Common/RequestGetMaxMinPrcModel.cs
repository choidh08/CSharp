using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestGetMaxMinPrcModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }
        public virtual string buyCuycyCd { get; set; }
        public virtual string payCuycyCd { get; set; }        
        public virtual string mkState { get; set; }

        public RequestGetMaxMinPrcModel() : base ("bt.trade.getMaxMinAmt.dp/proc.go") { }
    }
}