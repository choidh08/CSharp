using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseLendingApplyModel : ResponseBaseModel
    {
        public ResponseLendingApplyDataModel data { get; set; }
        public ResponseLendingApplyModel()
        {
            data = new ResponseLendingApplyDataModel();
        }
    }

    public class ResponseLendingApplyDataModel
    {
        public string failCd { get; set; }
    }
}