using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class ResponseGetMaxMinPrcModel : ResponseBaseModel
    {
        public ResponseGetMaxMinPrcDataModel data { get; set; }
        public ResponseGetMaxMinPrcModel()
        {
            data = new ResponseGetMaxMinPrcDataModel();
        }
    }

    public class ResponseGetMaxMinPrcDataModel
    {
        public virtual decimal? price { get; set; }
        public virtual decimal? payCnAmt { get; set; }
        public virtual decimal? payMaxPrc { get; set; }
        public virtual decimal? payMinPrc { get; set; }
        public virtual decimal? buyMaxPrc { get; set; }
        public virtual decimal? buyMinPrc { get; set; }

    }
}