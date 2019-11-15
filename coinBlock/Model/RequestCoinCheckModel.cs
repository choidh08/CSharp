using System;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class RequestCoinCheckModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public RequestCoinCheckModel() : base("bt.deposit.pendingCoin.dp/proc.go") { }
    }
}