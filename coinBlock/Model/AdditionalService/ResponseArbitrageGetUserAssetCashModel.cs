using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseArbitrageGetUserAssetCashModel : ResponseBaseModel
    {
        public ResponseArbitrageGetUserAssetCasDataModel data { get; set; }
        public ResponseArbitrageGetUserAssetCashModel()
        {
            data = new ResponseArbitrageGetUserAssetCasDataModel();
        }
    }

    public class ResponseArbitrageGetUserAssetCasDataModel
    {
        public virtual string failCd { get; set; }
        public virtual decimal data { get; set; }
        public virtual decimal coinPrc { get; set; }
        public virtual string exchgeNm { get; set; }
        public virtual decimal cnPtcPrc { get; set; }

        public ResponseArbitrageGetUserAssetCasDataModel()
        {

        }
    }
}