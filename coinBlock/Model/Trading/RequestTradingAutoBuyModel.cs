using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class RequestTradingAutoBuyModel : RequestBaseModel
    {
        public string cnKndCd { get; set; }
        public string userEmail { get; set; }
        public string mkState { get; set; }
        public string regIp { get; set; }
        public int sn { get; set; }
        public string udFlag { get; set; }
        public decimal? prc { get; set; }
        public int prcPer { get; set; }
        public decimal? rtPlsPrc { get; set; }
        public decimal? rtMnsPrc { get; set; }
        public decimal? amt { get; set; }        

        public RequestTradingAutoBuyModel() : base("bt.trade.autoBuy.dp/proc.go") { }
    }
}