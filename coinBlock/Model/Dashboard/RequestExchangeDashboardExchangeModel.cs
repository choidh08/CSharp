using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Dashboard
{
    public class RequestExchangeDashboardExchangeModel : RequestBaseModel
    {     
        public virtual string curcyCd { get; set; }

        public RequestExchangeDashboardExchangeModel() : base("bt.realtime.exchange.price.dp/proc.go") { this.RtpCall(this); }
    }
}