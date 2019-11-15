using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.Trading
{

    public class ResponseTradingGetAutoSellModel : ResponseBaseModel
    {
        public ResponseTradingGetAutoSellDataModel data { get; set; }
        public ResponseTradingGetAutoSellModel()
        {
            data = new ResponseTradingGetAutoSellDataModel();
        }
    }

    public class ResponseTradingGetAutoSellDataModel
    {
        public ObservableCollection<ResponseTradingGetAutoSellListModel> list { get; set; }
        public ResponseTradingGetAutoSellDataModel()
        {
            list = new ObservableCollection<ResponseTradingGetAutoSellListModel>();
        }
    }

    public class ResponseTradingGetAutoSellListModel
    {
        /// <summary>
        /// 코인 종류
        /// </summary>
        public virtual string cnKndCd { get; set; }
        /// <summary>
        /// 순번
        /// </summary>
        public virtual int sn { get; set; }
        /// <summary>
        /// H / L
        /// </summary>
        public virtual string udFlag { get; set; }
        /// <summary>
        /// 금액
        /// </summary>
        public virtual decimal? prc { get; set; }
        /// <summary>
        /// 오차율
        /// </summary>
        public virtual int prcPer { get; set; }
        /// <summary>
        /// 높은 오차금액
        /// </summary>
        public virtual decimal? rtPlsPrc { get; set; }
        /// <summary>
        /// 낮음 오차금액
        /// </summary>
        public virtual decimal? rtMnsPrc { get; set; }
        /// <summary>
        /// 갯수
        /// </summary>
        public virtual decimal? amt { get; set; }

        public virtual string mkState { get; set; }
    }
}