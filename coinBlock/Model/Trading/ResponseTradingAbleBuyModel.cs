using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class ResponseTradingAbleBuyModel : ResponseBaseModel
    {
        public ResponseTradingAbleBuyDataModel data { get; set; }
        public ResponseTradingAbleBuyModel()
        {
            data = new ResponseTradingAbleBuyDataModel();
        }
    }

    public class ResponseTradingAbleBuyDataModel
    {
        /// <summary>
        /// 결재가능금액
        /// </summary>
        public virtual decimal price { get; set; }
        /// <summary>
        /// 미체결최저판매가
        /// </summary>
        public virtual decimal minPrc { get; set; }
        /// <summary>
        /// 포인트결제가능금액
        /// </summary>
        public virtual decimal pointPrc { get; set; }
    }  
}
