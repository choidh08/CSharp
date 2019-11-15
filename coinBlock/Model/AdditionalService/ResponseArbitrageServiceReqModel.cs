using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseArbitrageServiceReqModel : ResponseBaseModel
    {
        public ResponseArbitrageServiceReqDataModel data { get; set; }
        public ResponseArbitrageServiceReqModel()
        {
            data = new ResponseArbitrageServiceReqDataModel();
        }
    }

    public class ResponseArbitrageServiceReqDataModel
    {
        public virtual string failCd { get; set; }
    }
}