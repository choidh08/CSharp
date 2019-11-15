using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseArbitrageCoinPayInfoModel : ResponseBaseModel
    {
        public ResponseArbitrageCoinPayInfoDataModel data { get; set; }
        public ResponseArbitrageCoinPayInfoModel()
        {
            data = new ResponseArbitrageCoinPayInfoDataModel();
        }
    }

    public class ResponseArbitrageCoinPayInfoDataModel
    {

        public virtual string cnkndcd { get; set; }
        public virtual string cnkndnm { get; set; }
        public virtual decimal? cnPrc { get; set; }
        public virtual decimal? payAmt { get; set; }      
    }    
}