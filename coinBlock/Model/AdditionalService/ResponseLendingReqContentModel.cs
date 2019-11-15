using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseLendingReqContentModel : ResponseBaseModel
    {
        public ResponseLendingReqContentDataModel data { get; set; }
        public ResponseLendingReqContentModel()
        {
            data = new ResponseLendingReqContentDataModel();
        }
    }
    
    public class ResponseLendingReqContentDataModel
    {
        public ObservableCollection<ResponseLendingReqContentListModel> list { get; set; }
        public ResponseLendingReqContentDataModel()
        {
            list = new ObservableCollection<ResponseLendingReqContentListModel>();
        }
    }

    public class ResponseLendingReqContentListModel
    {
        public virtual string no { get; set; }
        public virtual string renDftCode { get; set; }
        public virtual string userEmail { get; set; }
        public virtual string cnKndNm { get; set; }
        public virtual string applyDt { get; set; }
        public virtual string expireDt { get; set; }
        public virtual string overDivDt { get; set; }
        /// <summary>
        /// 구분값 (0:일반 , 1:연장)
        /// </summary>
        public virtual string renDiv { get; set; }
        public virtual decimal applyAmt { get; set; }
        public virtual string interestRate { get; set; }
        public virtual string repayDt { get; set; }
        public virtual decimal totRepayAmt { get; set; }
        /// <summary>
        /// 연체여부 (Y : 연체 , N : 미연체)
        /// </summary>
        public virtual string extensionYn { get; set; }
        /// <summary>
        /// 0 : 없음, 1: 상환, 2: 상환, 연체
        /// </summary>
        public virtual string btnView { get; set; }
        /// <summary>
        /// CMMC00000001411 신청완료
        /// CMMC00000001412 중도상환
        /// CMMC00000001413 만기상환
        /// </summary>
        public virtual string applyStatus { get; set; }
        public virtual decimal expInterest { get; set; }

        public virtual Visibility repayVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility extendVisible { get; set; } = Visibility.Collapsed;
    }
}


