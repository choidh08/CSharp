using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Dashboard
{   
    public class ResponseExchangeDashboardExchangeModel : ResponseBaseModel
    {
        public ResponseExchangeDashboardExchangeDataModel data { get; set; }
        public ResponseExchangeDashboardExchangeModel()
        {
            data = new ResponseExchangeDashboardExchangeDataModel();
        }
    }

    public class ResponseExchangeDashboardExchangeDataModel
    {
        public ObservableCollection<ResponseExchangeDashboardExchangeListModel> list { get; set; }
        public ResponseExchangeDashboardExchangeDataModel()
        {
            list = new ObservableCollection<ResponseExchangeDashboardExchangeListModel>();
        }
    }

    public class ResponseExchangeDashboardExchangeListModel
    {
        /// <summary>
        /// 거래소 이름
        /// </summary>
        public virtual string exchgNm { get; set; }
        /// <summary>
        /// 시세
        /// </summary>
        public virtual string coinPrc { get; set; }
        /// <summary>
        /// 거개량
        /// </summary>
        public virtual string svcTranAmt { get; set; }
        /// <summary>
        /// 변동금액
        /// </summary>
       
        public virtual string exchgGap { get; set; }

        public virtual string exchgGapPer { get; set; }

        
        public virtual string chgPrc { get; set; }
        /// <summary>
        /// 변동률
        /// </summary>
        public virtual string volume { get; set; }
    }
}