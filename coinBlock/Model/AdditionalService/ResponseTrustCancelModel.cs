using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseTrustCancelModel : ResponseBaseModel
    {
        public ResponseTrustCancelDataModel data { get; set; }
        public ResponseTrustCancelModel()
        {
            data = new ResponseTrustCancelDataModel();
        }
    }

    public class ResponseTrustCancelDataModel
    {
        public virtual string failCd { get; set; }
    }
}