using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace coinBlock.Model
{
    public class ResponseMainAssetModel : ResponseBaseModel
    {
        /// <summary>
        /// 총자산
        /// </summary>
        public virtual int totAsset { get; set; }
        public virtual ResponseMainAssetDataModel data { get; set; }

        public ResponseMainAssetModel()
        {
            data = new ResponseMainAssetDataModel();
        }
    }

    public class ResponseMainAssetDataModel
    {   
        public virtual string sysTime { get; set; }
        public virtual decimal totAsset { get; set; }
        public virtual ObservableCollection<ResponseMainAssetListModel> list { get; set; }

        public ResponseMainAssetDataModel()
        {
            list = new ObservableCollection<ResponseMainAssetListModel>();
        }
    }

    public class ResponseMainAssetListModel
    {
        /// <summary>
        ///  통화
        /// </summary>
        public virtual string curcyCd { get; set; }
        public virtual string curcyNm { get; set; }
        /// <summary>
        /// 거래가능_코인수량
        /// </summary>
        public virtual decimal posCnAmt { get; set; }
        /// <summary>
        /// 거래중_코인수량
        /// </summary>
        public virtual decimal impCnAmt { get; set; }
        /// <summary>
        /// 통화수량 합계(거래가능 + 거래중)
        /// </summary>
        public virtual decimal totCoinAmt { get; set; }
        /// <summary>
        /// 통화수량 합계(거래가능 + 거래중)
        /// </summary>
        public virtual decimal totCoinPrc { get; set; } 

        /// <summary>
        /// 원화금액
        /// </summary>
        public virtual decimal kwdPrc { get; set; }

        public virtual decimal impCnPrc { get; set; }
        public virtual decimal posCnPrc { get; set; }

        public virtual decimal impUserPoint { get; set; }
        public virtual decimal posUserPoint { get; set; }
        public virtual decimal userPoint { get; set; }

        public virtual string UpDownColor { get; set; }

        public virtual string UpDownSignal { get; set; }
        /// <summary>
        /// 시세차익
        /// </summary>
        public virtual string MarketPriceProfit { get; set; }

        public virtual decimal pntPrc { get; set; }

        public virtual decimal trustAmt { get; set; }
        public virtual decimal trustPrc { get; set; }
        public virtual string trustCn { get; set; }
        public virtual string trustExchange { get; set; }

        public virtual decimal trLimtAmt { get; set; }
        public virtual string trLimtCn { get; set; }
        public virtual string trLimtColor { get; set; }

        public virtual string posExchange { get; set; }
        public virtual string impExchange { get; set; }
        public virtual string totExchange { get; set; }
        public virtual string posCn { get; set; }
        public virtual string impCn { get; set; }
        public virtual string totCn { get; set; }
        public virtual Visibility ExchangeVisible { get; set; } = Visibility.Visible;
        public virtual string CashDecimal { get; set; }
    }
}