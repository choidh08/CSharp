using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestGetMkCoinListModel : RequestBaseModel
    {
        public RequestGetMkCoinListModel() : base("bt.getMkCoinList.dp/proc.go") { }
    }
}