using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class ResponseUserTelCertiryModel : ResponseBaseModel
    {
        public ResponseUserTelCertiryDataModel data { get; set; }
        public ResponseUserTelCertiryModel()
        {
            data = new ResponseUserTelCertiryDataModel();
        }
    }

    public class ResponseUserTelCertiryDataModel
    {
        public virtual string telNo { get; set; }
        public virtual string telCertYn { get; set; }
        public virtual string telCertDt { get; set; }

    }
}