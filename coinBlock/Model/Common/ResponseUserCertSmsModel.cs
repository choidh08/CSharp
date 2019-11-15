using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class ResponseUserCertSmsModel : ResponseBaseModel
    {
        public ResponseUserCertSmsDataModel data { get; set; }
        public ResponseUserCertSmsModel()
        {
            data = new ResponseUserCertSmsDataModel();
        }
    }

    public class ResponseUserCertSmsDataModel
    {
        public virtual string failCd { get; set; }
    }
}