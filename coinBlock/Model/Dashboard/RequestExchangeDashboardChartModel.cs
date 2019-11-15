using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Dashboard
{
    public class RequestExchangeDashboardChartModel : RequestBaseModel
    {
        /// <summary>
        /// 통화 종류(01:BTC , 02:ETH , 03:BCH , 04:XRP)
        /// </summary>
        public string curcyCd { get; set; }
        /// <summary>
        /// 시간 구분(1분 , 5분 , 15분 , 30분 , 60분, 1일, 1주)
        /// </summary>
        public string tradeTime { get; set; }
        /// <summary>
        /// 리스트 개수
        /// </summary>
        public int listSize { get; set; }

        public RequestExchangeDashboardChartModel() : base("bt.coin.price.chart.dp/proc.go") { this.RtpCall(this); }
    }
}
