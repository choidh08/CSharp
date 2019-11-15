using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class ResponseTradingCoinToCoinBuyModel : ResponseBaseModel
    {
        public virtual ResponseTradingCoinToCoinBuyDataModel data { get; set; }

        public ResponseTradingCoinToCoinBuyModel()
        {
            data = new ResponseTradingCoinToCoinBuyDataModel();
        }

        public class ResponseTradingCoinToCoinBuyDataModel
        {
            /// <summary>
            /// 결제가능금액
            /// </summary>
            public virtual decimal? price { get; set; }

            public virtual decimal? cnAmt { get; set; }
            /// <summary>
            /// 미체결최고구매가
            /// </summary>
            public virtual decimal? maxPrc { get; set; }
            /// <summary>
            /// 미체결최저판매가
            /// </summary>
            public virtual decimal? minPrc { get; set; }
            /// <summary>
            /// 포인트결제가능금액
            /// </summary>
            public virtual decimal? pointPrc { get; set; }
        }
    }
}