using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class ResponseTradingCoinToCoinSellModel : ResponseBaseModel
    {
        public virtual ResponseTradingCoinToCoinSelDataModel data { get; set; }

        public ResponseTradingCoinToCoinSellModel()
        {
            data = new ResponseTradingCoinToCoinSelDataModel();
        }

        public class ResponseTradingCoinToCoinSelDataModel
        {
            /// <summary>
            /// 미체결최고구매가
            /// </summary>
            public virtual decimal? maxPrc { get; set; }
            /// <summary>
            /// 미체결최저판매가
            /// </summary>
            public virtual decimal? minPrc { get; set; }

            public virtual decimal? curcyAmt { get; set; }

            public virtual decimal? rcvCurcyAmt { get; set; }

            public virtual decimal? krwPrice { get; set; }
        }
    }
}