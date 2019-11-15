using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.Trading
{
    public class ResponseTradingGetAutoBuyModel : ResponseBaseModel
    {
        public ResponseTradingGetAutoBuyDataModel data { get; set; }
        public ResponseTradingGetAutoBuyModel()
        {
            data = new ResponseTradingGetAutoBuyDataModel();
        }
    }

    public class ResponseTradingGetAutoBuyDataModel
    {
        public ObservableCollection<ResponseTradingGetAutoBuyListModel> list { get; set; }
        public ResponseTradingGetAutoBuyDataModel()
        {
            list = new ObservableCollection<ResponseTradingGetAutoBuyListModel>();
        }
    }

    public class ResponseTradingGetAutoBuyListModel
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
        /// <summary>
        /// 구매/판매 구분
        /// </summary>
        public virtual string trade { get; set; }

        public virtual string mkState { get; set; }
    }
}