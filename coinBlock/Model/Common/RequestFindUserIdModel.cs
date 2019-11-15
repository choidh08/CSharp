using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestFindUserIdModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }

        public virtual string userMobile { get; set; }

        public RequestFindUserIdModel() : base("bt.withdrow.findUserId.dp/proc.go") { }
    }
}