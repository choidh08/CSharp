using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class ResponseTradeHistoryTradingSearchWaitModel : ResponseBaseModel
    {
        public ResponseTradeHistoryTradingSearchWaitDataModel data { get; set; }
        public ResponseTradeHistoryTradingSearchWaitModel()
        {
            data = new ResponseTradeHistoryTradingSearchWaitDataModel();
        }
    }

    public class ResponseTradeHistoryTradingSearchWaitDataModel
    {   
        public virtual ObservableCollection<ResponseTradeHistoryTradingSearchWaitListModel> list { get; set; }
        public ResponseTradeHistoryTradingSearchWaitDataModel()
        {
            list = new ObservableCollection<ResponseTradeHistoryTradingSearchWaitListModel>();
        }
    }

    public class ResponseTradeHistoryTradingSearchWaitListModel
    {
        /// <summary>
        /// 누적수량
        /// </summary>
        public virtual string totTradeAmt { get; set; }
        /// <summary>
        /// 수량
        /// </summary>
        public virtual string tradeAmt { get; set; }
        /// <summary>
        /// 주문가격
        /// </summary>
        public virtual string tradePrc { get; set; }
    }
}
