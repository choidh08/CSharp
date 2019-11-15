using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class ResponseGetGoogleOtpModel : ResponseBaseModel
    {
        public ResponseGetGoogleOtpDataModel data { get; set; }
        public ResponseGetGoogleOtpModel()
        {
            data = new ResponseGetGoogleOtpDataModel();
        }
    } 
    
    public class ResponseGetGoogleOtpDataModel
    {
        public virtual string urlHost { get; set; }

        public virtual string encodedKey { get; set; }

        public virtual string qrCodeUrl { get; set; }

        public virtual string authCode { get; set; }
    }
}