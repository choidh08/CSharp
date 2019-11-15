using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class ResponseTradingTradeHistoryModel : ResponseBaseModel
    {
        public ResponseTradingTradeHistoryDataModel data { get; set; }

        public ResponseTradingTradeHistoryModel()
        {
            data = new ResponseTradingTradeHistoryDataModel();
        }
    }

    public class ResponseTradingTradeHistoryDataModel
    {
        public ObservableCollection<ResponseTradingTradeHistoryListModel> list { get; set; }
        public ResponseTradingTradeHistoryDataModel()
        {
            list = new ObservableCollection<ResponseTradingTradeHistoryListModel>();
        }
    }

    public class ResponseTradingTradeHistoryListModel
    {
        public string orderCd { get; set; }
        public string conTm { get; set; }
        public string conAmt { get; set; }
        public string aveConPrc { get; set; }
        public string conPrc { get; set; }
        public string fee { get; set; }
        public string totConAmt { get; set; }
        public virtual string floatCoin { get; set; }
        public virtual string floatCash { get; set; }
    }
}