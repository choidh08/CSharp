using System;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class RequestSendPushModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public string regIp { get; set; }

        public string contents { get; set; }

        public string pushType { get; set; }

        public RequestSendPushModel() : base("bt.sendPush.dp/proc.go") { }
    }
}