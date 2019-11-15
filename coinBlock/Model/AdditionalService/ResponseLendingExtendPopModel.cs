using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseLendingExtendPopModel : ResponseBaseModel
    {
        public ResponseLendingExtendPopDataModel data { get; set; }
        public ResponseLendingExtendPopModel()
        {
            data = new ResponseLendingExtendPopDataModel();
        }
    }

    public class ResponseLendingExtendPopDataModel
    {
        public virtual string failCd { get; set; }
    }
}