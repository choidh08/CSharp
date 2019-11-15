using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseArbitrageGetSignInfoModel : ResponseBaseModel
    {
        public ResponseArbitrageGetSignInfoDataModel data { get; set; }
        public ResponseArbitrageGetSignInfoModel()
        {
            data = new ResponseArbitrageGetSignInfoDataModel();
        }
    }

    public class ResponseArbitrageGetSignInfoDataModel
    {
        public virtual string failCd { get; set; }
        public ObservableCollection<ResponseArbitrageGetSignInfoListModel> list { get; set; }
        public ResponseArbitrageGetSignInfoDataModel()
        {
            list = new ObservableCollection<ResponseArbitrageGetSignInfoListModel>();
        }
    }

    public class ResponseArbitrageGetSignInfoListModel
    {
        public virtual string no { get; set; }
        public virtual string exchgeId { get; set; }
        public virtual string exchgeNm { get; set; }
        public virtual string joinYn { get; set; }
        public virtual string mainYn { get; set; }
    }
}