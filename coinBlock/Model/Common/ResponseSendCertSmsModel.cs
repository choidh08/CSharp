using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.Common
{
    public class ResponseSendCertSmsModel : ResponseBaseModel
    {
        public ResponseSendCertSmsDataModel data { get; set; }
        public ResponseSendCertSmsModel()
        {
            data = new ResponseSendCertSmsDataModel();
        }
    }

    public class ResponseSendCertSmsDataModel
    {
        public virtual string failCd { get; set; }
    }    
}