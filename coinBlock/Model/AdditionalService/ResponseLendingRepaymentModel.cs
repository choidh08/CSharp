using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseLendingRepaymentModel : ResponseBaseModel
    {
        public ResponseLendingRepaymentDataModel data { get; set; }
        public ResponseLendingRepaymentModel()
        {
            data = new ResponseLendingRepaymentDataModel();
        }
    }

    public class ResponseLendingRepaymentDataModel
    {
        public ObservableCollection<ResponseLendingRepaymentListModel> list { get; set; }
        public ResponseLendingRepaymentDataModel()
        {
            list = new ObservableCollection<ResponseLendingRepaymentListModel>();
        }
    }

    public class ResponseLendingRepaymentListModel
    {
        public virtual string no { get; set; }
        public virtual string renDiv { get; set; }
        // 0 : 일반 , 1 : 연장
        public virtual string repayDt { get; set; }
        // 0 : 일반 , 1 : 연장
        public virtual string interestDiv { get; set; }
        public virtual string interestCnt { get; set; }
        public virtual decimal repayAmt { get; set; }
        public virtual decimal overInterest { get; set; }
        public virtual decimal totRepayAmt { get; set; }
        // R : 대기 , Y : 상환, N : 연체
        public virtual string repayYn { get; set; }
        public virtual string memo { get; set; }
    }
}