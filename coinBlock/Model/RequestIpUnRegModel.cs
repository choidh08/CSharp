using System;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class RequestIpUnRegModel : RequestBaseModel
    {
        public virtual string userEmail { get; set; }
        public virtual string ip { get; set; }
        public virtual int limtHr { get; set; }

        public RequestIpUnRegModel () : base("bt.insUptUserIp.dp/proc.go") { }
    }
}