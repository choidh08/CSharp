using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseKrwRechargeModel : ResponseBaseModel
    {
        public ResponseKrwRechargeDataModel data { get; set; }
        public ResponseKrwRechargeModel()
        {
            data = new ResponseKrwRechargeDataModel();
        }
    }

    public class ResponseKrwRechargeDataModel
    {
        public ObservableCollection<ResponseKrwRechargeListModel> list { get; set; }
        public ResponseKrwRechargeDataModel()
        {
            list = new ObservableCollection<ResponseKrwRechargeListModel>();
        }
    }

    public class ResponseKrwRechargeListModel
    {
        /// <summary>
        /// 요청일자
        /// </summary>
        public virtual string reqDt { get; set; }
        /// <summary>
        /// 입금종류코드
        /// </summary>
        public virtual string payKndCd { get; set; }
        /// <summary>
        /// 충전 금액
        /// </summary>
        public virtual decimal crgPrc { get; set; }
        /// <summary>
        /// Y: 완료, N:미완료
        /// </summary>
        public virtual string state { get; set; }
        /// <summary>
        /// 완료일자
        /// </summary>
        public virtual string compDt { get; set; }
        /// <summary>
        /// 입금통화코드
        /// </summary>
        public virtual string inCryCode { get; set; }
        /// <summary>
        /// 입금금액
        /// </summary>
        public virtual decimal inAmt { get; set; }
        /// <summary>
        /// 입금통화 환율
        /// </summary>
        //public virtual decimal exRate { get; set; }
        /// <summary>
        /// 기준통화코드
        /// </summary>
        public virtual string exCryCode { get; set; }

        public virtual string cardNum { get; set; }

        public virtual decimal dptFee { get; set; }        
    }
}