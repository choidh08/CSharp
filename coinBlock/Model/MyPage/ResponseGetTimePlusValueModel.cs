using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.MyPage
{
    public class ResponseGetTimePlusValueModel : ResponseBaseModel
    {
        public ResponseGetTimePlusValueDataModel data { get; set; }
        public ResponseGetTimePlusValueModel()
        {
            data = new ResponseGetTimePlusValueDataModel();
        }
    }

    public class ResponseGetTimePlusValueDataModel
    {
        public ObservableCollection<ResponseGetTimePlusValueListModel> list { get; set; }
        public ResponseGetTimePlusValueDataModel()
        {
            list = new ObservableCollection<ResponseGetTimePlusValueListModel>();
        }
    }

    public class ResponseGetTimePlusValueListModel
    {
        public virtual string cnkndcd { get; set; }
        public virtual string cnkndnm { get; set; }
        public virtual decimal? buyamt { get; set; }
        public virtual decimal? buyavgprc { get; set; }
        public virtual decimal? buyTotPrc { get; set; }
        public virtual decimal? selAmt { get; set; }
        public virtual decimal? selavgprc { get; set; }
        public virtual decimal? seltotprc { get; set; }
        public virtual decimal? pnlprc { get; set; }
        public virtual decimal? marginrate { get; set; }
        public virtual decimal? cnPrc { get; set; }
        public virtual string CommonFloat { get; set; }
    }
}