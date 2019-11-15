using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.HelpDesk
{
    public class RequestAdminMailSendModel : RequestBaseModel
    {
        public virtual string regUser { get; set; }

        public virtual string contentId { get; set; }

        public RequestAdminMailSendModel() : base("bt.admin.All.Sendmail.dp/proc.go") { }
    }
}