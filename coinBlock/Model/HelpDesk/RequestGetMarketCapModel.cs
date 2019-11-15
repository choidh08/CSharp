using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.HelpDesk
{
    public class RequestGetMarketCapModel : RequestBaseModel
    {
        public RequestGetMarketCapModel() : base("bt.getMarketCapInfo.dp/proc.go") { }
    }
}