using System;
using DevExpress.Mvvm;

namespace PlanBit.Model.Common
{
    public class RequestGetMkCoinListModel : RequestBaseModel
    {
        public RequestGetMkCoinListModel() : base("bt.getMkCoinList.dp/proc.go") { }
    }
}