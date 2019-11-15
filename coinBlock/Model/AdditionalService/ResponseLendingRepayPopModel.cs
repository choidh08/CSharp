using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseLendingRepayPopModel : ResponseBaseModel
    {
        public ResponseLendingRepayPopDataModel data { get; set; }
        public ResponseLendingRepayPopModel()
        {
            data = new ResponseLendingRepayPopDataModel();
        }
    }

    public class ResponseLendingRepayPopDataModel
    {
        public virtual string failCd { get; set; }
    }
}