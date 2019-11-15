using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseCoinPayInfoModel : ResponseBaseModel
    {
        public ResponseCoinPayInfoDataModel data { get; set; }
        public ResponseCoinPayInfoModel()
        {
            data = new ResponseCoinPayInfoDataModel();
        }
    }

    public class ResponseCoinPayInfoDataModel
    {
        public ObservableCollection<ResponseCoinPayInfoListModel> list { get; set; }
        public ResponseCoinPayInfoDataModel()
        {
            list = new ObservableCollection<ResponseCoinPayInfoListModel>();
        }
    }

    public class ResponseCoinPayInfoListModel
    {
        public virtual string cnkndcd { get; set; }
        public virtual string cnkndnm { get; set; }
        public virtual decimal? cnPrc { get; set; }
        public virtual decimal? payAmt { get; set; }
    }
}