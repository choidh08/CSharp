using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseTrustInsertModel : ResponseBaseModel
    {
        public ResponseTrustInsertDataModel data { get; set; }
        public ResponseTrustInsertModel()
        {
            data = new ResponseTrustInsertDataModel();
        }
    }

    public class ResponseTrustInsertDataModel
    {
        public virtual string failCd { get; set; }
    }
}