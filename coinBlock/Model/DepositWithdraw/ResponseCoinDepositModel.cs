using System.Collections.ObjectModel;

namespace coinBlock.Model.DepositWithdraw
{

    public class ResponseCoinDepositModel : ResponseBaseModel
    {
        public ResponseCoinDepositDataModel data { get; set; }
        public ResponseCoinDepositModel()
        {
            data = new ResponseCoinDepositDataModel();
        }
    }
   
    public class ResponseCoinDepositDataModel
    {
        public ObservableCollection<ResponseCoinDepositListModel> list { get; set; }
        public ResponseCoinDepositDataModel()
        {
            list = new ObservableCollection<ResponseCoinDepositListModel>();
        }
    }    

    public class ResponseCoinDepositListModel
    {
        // <summary>
        /// 일자
        /// </summary>
        public virtual string reqDt { get; set; }
        /// <summary>
        /// 코인 종류
        /// </summary>
        public virtual string curcyCd { get; set; }
        /// <summary>
        /// 코인 갯수
        /// </summary>
        public virtual string cnAmt { get; set; }
        /// <summary>
        /// 진행상태
        /// </summary>
        public virtual string state { get; set; }
        /// <summary>
        /// 완료일자
        /// </summary>
        public virtual string compDt { get; set; }

        public virtual string exFlag { get; set; }

        public virtual string orderDesc { get; set; }
    }
}