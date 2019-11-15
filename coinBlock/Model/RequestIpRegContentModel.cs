using System;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class RequestIpRegContentModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string ip { get; set; }

        public RequestIpRegContentModel() : base ("bt.getUserIpList.dp/proc.go") { }
    }
}