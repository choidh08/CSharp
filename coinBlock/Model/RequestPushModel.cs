using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model
{
    public class RequestPushModel :RequestBaseModel
    {
        /// <summary>
        /// 회원 계정
        /// </summary>
        public string userEmail { get; set; }

        public RequestPushModel() : base("bt.getHtsPushList.dp/proc.go") { base.RtpCall(this); }
    }
}
