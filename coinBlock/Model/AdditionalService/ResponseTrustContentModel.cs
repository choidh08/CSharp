using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseTrustContentModel : ResponseBaseModel
    {
        public ResponseTrustContentDataModel data { get; set; }
        public ResponseTrustContentModel()
        {
            data = new ResponseTrustContentDataModel();
        }
    }

    public class ResponseTrustContentDataModel
    {
        public virtual string failCd { get; set; }

        public virtual ObservableCollection<ResponseTrustContentListModel> list { get; set; }

        public ResponseTrustContentDataModel()
        {
            list = new ObservableCollection<ResponseTrustContentListModel>();
        }
    }

    public class ResponseTrustContentListModel
    {
        public virtual string trustReqCode { get; set; }

        public virtual string no { get; set; }

        public virtual string reqDt { get; set; }

        public virtual string expireDt { get; set; }

        public virtual string cmmNm { get; set; }

        public virtual decimal? reqAmt { get; set; }

        public virtual decimal? rate { get; set; }

        public virtual decimal? expInterest { get; set; }

        public virtual string cancelDt { get; set; }

        public virtual string trustStatus { get; set; }

        public virtual decimal? totalAmt { get; set; }

        public virtual Visibility cancelVisible { get; set; } = Visibility.Collapsed;
    }
}