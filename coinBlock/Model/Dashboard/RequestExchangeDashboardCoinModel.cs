using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Dashboard
{
    public class RequestExchangeDashboardCoinModel : RequestBaseModel
    {
        public virtual string time { get; set; }

        public RequestExchangeDashboardCoinModel() : base("bt.realtime.coin.price.dp/proc.go") { this.RtpCall(this); }
    }
}
