using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseUserWithdrawInfoModel : ResponseBaseModel
    {
        public ResponseUserWithdrawInfoDataModel data { get; set; }
        public ResponseUserWithdrawInfoModel()
        {
            data = new ResponseUserWithdrawInfoDataModel();
        }
    }

    public class ResponseUserWithdrawInfoDataModel
    {
        public virtual decimal krwPrc { get; set; }

        public virtual decimal krwPrcCard { get; set; }

        public virtual string bankNm { get; set; }

        public virtual string bankCd { get; set; }

        public virtual string bankAccNo { get; set; }

        public virtual string accntNm { get; set; }

        //public virtual decimal exRate { get; set; }

        public virtual string cryCode { get; set; }

        public virtual string certYn { get; set; }

        public virtual string useYn { get; set; }
    }
}