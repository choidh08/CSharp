using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseUserOtpCheckModel : ResponseBaseModel
    {
        public ResponseUserOtpCheckDataModel data { get; set; }
        public ResponseUserOtpCheckModel()
        {
            data = new ResponseUserOtpCheckDataModel();
        }
    }

    public class ResponseUserOtpCheckDataModel
    {
        public virtual string failCd { get; set; }

        // Y :  인증하는 사용자  N : 인증 안하는 사용자
        public virtual string otpYn { get; set; }
    }
}