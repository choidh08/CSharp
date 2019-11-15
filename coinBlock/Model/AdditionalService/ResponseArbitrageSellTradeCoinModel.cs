using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseArbitrageSellTradeCoinModel : ResponseBaseModel
    {
        public ResponseArbitrageSellTradeCoinDataModel data { get; set; }
        public ResponseArbitrageSellTradeCoinModel()
        {
            data = new ResponseArbitrageSellTradeCoinDataModel();
        }
    }

    public class ResponseArbitrageSellTradeCoinDataModel
    {
        public virtual string failCd { get; set; }
    }
}