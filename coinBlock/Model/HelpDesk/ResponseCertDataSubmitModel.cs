using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.HelpDesk
{
    public class ResponseCertDataSubmitModel : ResponseBaseModel
    {
        public ResponseCertDataSubmitDataModel data { get; set; }
        public ResponseCertDataSubmitModel()
        {
            data = new ResponseCertDataSubmitDataModel();
        }
    }

    public class ResponseCertDataSubmitDataModel
    {
        public virtual string certCode { get; set; }
    }
}