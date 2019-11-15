using System;
using DevExpress.Mvvm;

namespace PlanBit.Model.MyPage
{
    public class ResponseGoogleOtpModel : ResponseBaseModel
    {
        public virtual string urlHost { get; set; }

        public virtual string encodedKey { get; set; }

        public virtual string qrCodeUrl { get; set; }

        public virtual string authCode { get; set; }

        public ResponseGoogleOtpModel()
        {

        }
    } 
}