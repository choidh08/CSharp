using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class RequestGetAskingModel : RequestBaseModel
    {
        //마켓 공통코드
        public string mkState { get; set; }

        public RequestGetAskingModel() : base("bt.trade.getArc.dp/proc.go") { }
    }
}