using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestGetPopupListModel : RequestBaseModel
    {
        public string langCd { get; set; }

        public RequestGetPopupListModel() : base("bt.getPopUpList.dp/proc.go") { }
    }
}