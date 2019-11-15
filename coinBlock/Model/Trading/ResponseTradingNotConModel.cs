using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class ResponseTradingNotConModel : ResponseBaseModel
    {
        public ResponseTradingNotConDataModel data { get; set; }
        public ResponseTradingNotConModel()
        {
            data = new ResponseTradingNotConDataModel();
        }
    }

    public class ResponseTradingNotConDataModel
    {
        public virtual ObservableCollection<ResponseTradingNotConListModel> list { get; set; }
        public ResponseTradingNotConDataModel()
        {
            list = new ObservableCollection<ResponseTradingNotConListModel>();
        }
    }

    public class ResponseTradingNotConListModel
    {
        /// <summary>
        /// 주문번호
        /// </summary>
        public virtual string orderNo { get; set; }
        /// <summary>
        /// 판매/구매 종류 (CMMC00000000197:구매 CMMC00000000198:판매)
        /// </summary>
        public virtual string orderCd { get; set; }
        /// <summary>
        /// 주문시각
        /// </summary>
        public virtual string orderTm { get; set; }
        /// <summary>
        /// 주문량
        /// </summary>
        public virtual string orderAmt { get; set; }
        /// <summary>
        /// 체결량
        /// </summary>
        public virtual string tradeAmt { get; set; }
        /// <summary>
        /// 평균체결가
        /// </summary>
        public virtual string avrTradePrc { get; set; }
        /// <summary>
        /// 주문가
        /// </summary>
        public virtual string orderPrc { get; set; }
        /// <summary>
        /// 부분체결여부
        /// </summary>
        public virtual string buyAllYn { get; set; }

        public virtual string coinNm { get; set; }

        public virtual string floatCoin { get; set; }

        public virtual string floatCash { get; set; }

    }
}
