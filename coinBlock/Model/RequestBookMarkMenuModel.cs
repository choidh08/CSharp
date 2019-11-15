using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class RequestBookMarkMenuModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestBookMarkMenuModel() : base("bt.menu.getMenuList.dp/proc.go") { this.RtpCall(this); }
    }
}