using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseArbitrageBuyTradeCoinModel : ResponseBaseModel
    {
        public ResponseArbitrageBuyTradeCoinDataModel data { get; set; }
        public ResponseArbitrageBuyTradeCoinModel()
        {
            data = new ResponseArbitrageBuyTradeCoinDataModel();
        }
    }

    public class ResponseArbitrageBuyTradeCoinDataModel
    {
        public virtual string failCd { get; set; }
    }
}