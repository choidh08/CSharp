using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseArbitrageTradeHistoryModel : ResponseBaseModel
    {
        public ResponseArbitrageTradeHistoryDataModel data { get; set; }
        public ResponseArbitrageTradeHistoryModel()
        {
            data = new ResponseArbitrageTradeHistoryDataModel();
        }
    }

    public class ResponseArbitrageTradeHistoryDataModel
    {
        public virtual string failCd { get; set; }

        public ObservableCollection<ResponseArbitrageTradeHistoryListModel> list { get; set; }
        public ResponseArbitrageTradeHistoryDataModel()
        {
            list = new ObservableCollection<ResponseArbitrageTradeHistoryListModel>();
        }
    }

    public class ResponseArbitrageTradeHistoryListModel
    {
        public virtual string gubun { get; set; }
        public virtual string tradeCd { get; set; }
        public virtual string targetId { get; set; }
        public virtual string targetNm { get; set; }
        public virtual string cnKndNm { get; set; }
        public virtual string cnExchgAmt { get; set; }
        public virtual string cnExchgIngAmt { get; set; }
        public virtual string exCnPrc { get; set; }
        public virtual string exPrc { get; set; }
        public virtual string exchgeFee { get; set; }
        public virtual string abtrgFee { get; set; }
        public virtual string uptDt { get; set; }
        public virtual string abtrgMrgn { get; set; }
    }
}