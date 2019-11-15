using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseArbitrageSetMainExchangeModel : ResponseBaseModel
    {
        public ResponseArbitrageSetMainExchangeDataModel data { get; set; }
        public ResponseArbitrageSetMainExchangeModel()
        {
            data = new ResponseArbitrageSetMainExchangeDataModel();
        }
    }

    public class ResponseArbitrageSetMainExchangeDataModel
    {
        public virtual string failCd { get; set; }

        public ResponseArbitrageSetMainExchangeDataModel()
        {

        }
    }
}