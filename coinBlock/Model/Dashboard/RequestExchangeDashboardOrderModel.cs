using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Dashboard
{
    public class RequestExchangeDashboardOrderModel : RequestBaseModel
    {
        /// <summary>
        /// 통화
        /// </summary>
        public string curcyCd { get; set; }
        /// <summary>
        /// 리스트 갯수
        /// </summary>
        public int listSize { get; set; }

        public string userEmail { get; set; }

        public RequestExchangeDashboardOrderModel() : base("bt.coin.beforeList.dp/proc.go") { this.RtpCall(this); }
    }
}