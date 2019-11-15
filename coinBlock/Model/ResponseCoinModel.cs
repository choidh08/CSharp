using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model
{
    public class ResponseCoinModel : ResponseBaseModel
    {
        public virtual ResponseCoinDataModel data { get; set; }
    }

    public class ResponseCoinDataModel
    {
        /// <summary>
        /// 기본 통화 표시명
        /// </summary>
        public string CurrencyDisplay { get; set; }

        /// <summary>
        /// 체결 최대 금액
        /// </summary>
        public decimal TradeFillMaxPrc { get; set; }
        /// <summary>
        /// 체결 최소 금액
        /// </summary>
        public decimal TradeFillMinPrc { get; set; }

        /// <summary>
        /// 출금최대 금액
        /// </summary>
        public decimal WithdrawMaxPrc { get; set; }
        /// <summary>
        /// 출금최소 금액
        /// </summary>
        public decimal WithdrawMinPrc { get; set; }
        /// <summary>
        /// 출금최소 금액
        /// </summary>
        public decimal WithdrawDayPrc { get; set; }
        /// <summary>
        /// 출금수수료
        /// </summary>
        public decimal WithdrawFee { get; set; }

        public virtual decimal mkKRW { get; set; }
        public virtual decimal mkETH { get; set; }
        public virtual decimal mkUSDT { get; set; }
        public virtual decimal mkBTC { get; set; }
        public virtual int Lv { get; set; }

        public virtual decimal CashDecimal { get; set; }

        public virtual ObservableCollection<ResponseCoinListModel> list { get; set; }
        public ResponseCoinDataModel()
        {
            list = new ObservableCollection<ResponseCoinListModel>();
        }
    }

    public class ResponseCoinListModel
    {
        /// <summary>
        /// 코인이름
        /// </summary>
        public virtual string CoinName { get; set; }
        /// <summary>
        /// 코인코드
        /// </summary>
        public virtual string CoinCode { get; set; }
        /// <summary>
        /// 코인 소수점 자릿수
        /// </summary>
        public virtual decimal CoinFloatCnt { get; set; }
        /// <summary>
        /// 코인 거래 호가
        /// </summary>
        public virtual decimal TradeBidding { get; set; }
        /// <summary>
        /// 코인 거래 최대 수량
        /// </summary>
        public virtual decimal TradeMaxCnt { get; set; }
        /// <summary>
        /// 코인 거래 최소 수량
        /// </summary>
        public virtual decimal TradeMinCnt { get; set; }
        /// <summary>
        /// 코인 송금 최대 한도
        /// </summary>
        public virtual decimal TransferMaxCnt { get; set; }
        /// <summary>
        /// 코인 송금 최소 한도
        /// </summary>
        public virtual decimal TransferMinCnt { get; set; }
        /// <summary>
        /// 코인 송금 일일 한도
        /// </summary>
        public virtual decimal TransferDayCnt { get; set; }
        /// <summary>
        /// 코인 송금 수수료
        /// </summary>
        public virtual decimal TransferFee { get; set; }
        /// <summary>
        /// 체결 최대 금액(ResponseCoinDataModel 동일값 사용 클라이언트에서 넣음)
        /// </summary>
        public virtual decimal TradeFillMaxPrc { get; set; }
        /// <summary>
        /// 체결 최소 금액(ResponseCoinDataModel 동일값 사용 클라이언테에서 넣음)
        /// </summary>
        public virtual decimal TradeFillMinPrc { get; set; }

        public virtual decimal CoinDecimal { get; set; }

        public virtual decimal CashDecimal { get; set; }

        public virtual string IsTagYn { get; set; }
    }
}