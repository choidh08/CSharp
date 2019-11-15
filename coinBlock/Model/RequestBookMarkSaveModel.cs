using System;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class RequestBookMarkSaveModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string menuCd { get; set; }

        public RequestBookMarkSaveModel() : base("bt.menu.insUptMenu.dp/proc.go") { this.RtpCall(this); }
    }
}