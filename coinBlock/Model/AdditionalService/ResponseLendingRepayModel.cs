using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseLendingRepayModel : ResponseBaseModel
    {
        public ResponseLendingRepayDataModel data { get; set; }
        public ResponseLendingRepayModel()
        {
            data = new ResponseLendingRepayDataModel();
        }
    }

    public class ResponseLendingRepayDataModel
    {
        public virtual decimal applyAmt { get; set; }
        public virtual decimal remainInterest { get; set; }
        public virtual decimal totRepayAmt { get; set; }
        public virtual string repayDt { get; set; }
        public virtual string cnKndNm { get; set; }
    }
}