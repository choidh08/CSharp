using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class ResponseAccountCertModel : ResponseBaseModel
    {
        public ResponseAccountCertDataModel data { get; set; }
        public ResponseAccountCertModel()
        {
            data = new ResponseAccountCertDataModel();
        }
    }

    public class ResponseAccountCertDataModel
    {
        public virtual string failCd { get; set; }

        public ResponseAccountCertDataModel()
        {

        }
    }
}