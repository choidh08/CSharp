using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class ResponseTradeHistoryTradingSearchCompleteModel : ResponseBaseModel
    {
        public ResponseTradeHistoryTradingSearchCompleteDataModel data { get; set; }
        public ResponseTradeHistoryTradingSearchCompleteModel()
        {
            data = new ResponseTradeHistoryTradingSearchCompleteDataModel();
        }
    }

    public class ResponseTradeHistoryTradingSearchCompleteDataModel
    {
        public virtual ObservableCollection<ResponseTradeHistoryTradingSearchCompleteListModel> list { get; set; }
        public ResponseTradeHistoryTradingSearchCompleteDataModel()
        {
            list = new ObservableCollection<ResponseTradeHistoryTradingSearchCompleteListModel>();
        }
    }

    public class ResponseTradeHistoryTradingSearchCompleteListModel
    {
        /// <summary>
        /// 체결시각
        /// </summary>
        public virtual string tradeTm { get; set; }
        /// <summary>
        /// 체결가
        /// </summary>
        public virtual string tradePrc { get; set; }
        /// <summary>
        /// 거래량
        /// </summary>
        public virtual string tradeAmt { get; set; }
        /// <summary>
        /// 체결금액
        /// </summary>
        public virtual string totTradePrc { get; set; }
        /// <summary>
        /// 주문구분(01:구매 02:판매)
        /// </summary>
        public virtual string orderCd { get; set; }

        /// <summary>
        /// 전 체결가랑 비교하여 색상 설정(크거나 같으면 빨강, 작으면 파랑)
        /// </summary>
        public virtual string fillPrc { get; set; } = "0";
    }
}