using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseGetCoinAddressModel : ResponseBaseModel
    {
        public ResponseGetCoinAddressDataModel data { get; set; }
        public ResponseGetCoinAddressModel()
        {
            data = new ResponseGetCoinAddressDataModel();
        }
    }

    public class ResponseGetCoinAddressDataModel
    {
        public virtual string accNo { get; set; }

        public virtual string destiTag { get; set; }

        public virtual string failCd { get; set; }
    }
}