using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class ResponseSetGoogleOtpModel : ResponseBaseModel
    {
        public ResponseSetGoogleOtpDataModel data { get; set; }
        public ResponseSetGoogleOtpModel()
        {
            data = new ResponseSetGoogleOtpDataModel();
        }
    }

    public class ResponseSetGoogleOtpDataModel
    {
        public virtual string failCd { get; set; }
    }
}