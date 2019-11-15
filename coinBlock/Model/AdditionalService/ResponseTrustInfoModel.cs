using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseTrustInfoModel : ResponseBaseModel
    {
        public ResponseTrustInfoDataModel data { get; set; }
        public ResponseTrustInfoModel()
        {
            data = new ResponseTrustInfoDataModel();
        }
    }

    public class ResponseTrustInfoDataModel
    {
        public virtual decimal? amt { get; set; }

        public virtual decimal? coinMinAmt { get; set; }

        public virtual string failCd { get; set; }

        public ObservableCollection<ResponseTrustInfoListModel> list { get; set; }

        public ResponseTrustInfoDataModel()
        {
            list = new ObservableCollection<ResponseTrustInfoListModel>();
        }
    }

    public class ResponseTrustInfoListModel
    {
        public virtual decimal month { get; set; }

        public virtual decimal rate { get; set; }
    }
}