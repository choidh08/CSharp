using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    [POCOViewModel]
    public class ResponseArbitrageContentModel : ResponseBaseModel
    {
        public ResponseArbitrageContentDataModel data { get; set; }
        public ResponseArbitrageContentModel()
        {
            data = new ResponseArbitrageContentDataModel();
        }
    }

    public class ResponseArbitrageContentDataModel
    {
        public virtual string cnKndCd { get; set; }
        public virtual string userEmail { get; set; }
        public virtual string reqDt { get; set; }
        public virtual string endDt { get; set; }
        public virtual decimal? payAmt { get; set; }
    }
}