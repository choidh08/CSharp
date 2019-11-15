using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseLendingExtendModel : ResponseBaseModel
    {
        public ResponseLendingExtendDataModel data { get; set; }
        public ResponseLendingExtendModel()
        {
            data = new ResponseLendingExtendDataModel();
        }
    }

    public class ResponseLendingExtendDataModel
    {
        public virtual decimal extensionRate { get; set; }
        public virtual string extensionDt { get; set; }
    }
}