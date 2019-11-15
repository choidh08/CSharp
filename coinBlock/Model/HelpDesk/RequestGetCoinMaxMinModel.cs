using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.HelpDesk
{
    public class RequestGetCoinMaxMinModel : RequestBaseModel
    {
        public RequestGetCoinMaxMinModel() : base("bt.getCoinMinMaxInfo.dp/proc.go") { }        
    }
}