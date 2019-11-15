using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class ResponseCertifyInfoModel : ResponseBaseModel
    {
        public ResponseCertifyInfoDataModel data { get; set; }
        public ResponseCertifyInfoModel()
        {
            data = new ResponseCertifyInfoDataModel();               
        }
    }

    public class ResponseCertifyInfoDataModel
    {
        public virtual string emailCertYn { get; set; }

        public virtual string smsCertYn { get; set; }

        public virtual string accountNo { get; set; }

        public virtual string otpNo { get; set; }

        /// <summary>
        /// 1 : 완료 , 2 : 취소상태, 3: 신청상태, 공백 :미상태
        /// </summary>
        public virtual string kycCertYn { get; set; }

        public virtual string otpCertYn { get; set; } = string.Empty;

        public virtual string accCertYn { get; set; }

        public virtual string userInfoYn { get; set; } = string.Empty;
    }
}