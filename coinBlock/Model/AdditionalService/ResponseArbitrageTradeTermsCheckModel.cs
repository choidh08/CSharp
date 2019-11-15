using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseArbitrageTradeTermsCheckModel : ResponseBaseModel
    {
        public ResponseArbitrageTradeTermsCheckDataModel data { get; set; }
        public ResponseArbitrageTradeTermsCheckModel()
        {
            data = new ResponseArbitrageTradeTermsCheckDataModel();
        }
    }

    public class ResponseArbitrageTradeTermsCheckDataModel
    {
        public virtual string failCd { get; set; }
    }
}