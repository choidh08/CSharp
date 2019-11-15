using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseArbitrageGetUserAssetCoinModel : ResponseBaseModel
    {
        public ResponseArbitrageGetUserAssetCoinDataModel data { get; set; }
        public ResponseArbitrageGetUserAssetCoinModel()
        {
            data = new ResponseArbitrageGetUserAssetCoinDataModel();
        }
    }

    public class ResponseArbitrageGetUserAssetCoinDataModel
    {
        public virtual string failCd { get; set; }
        public virtual decimal data { get; set; }
        public virtual decimal coinPrc { get; set; }
        public virtual string exchgeNm { get; set; }
        public virtual string cnKndCd { get; set; }
        public virtual decimal cnPtcPrc { get; set; }

        public ResponseArbitrageGetUserAssetCoinDataModel()
        {

        }
    }
}