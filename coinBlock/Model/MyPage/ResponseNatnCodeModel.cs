using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.MyPage
{
    public class ResponseNatnCodeModel : ResponseBaseModel
    {
        public ResponseNatnCodeDataModel data { get; set; }
        public ResponseNatnCodeModel()
        {
            data = new ResponseNatnCodeDataModel();
        }
    }

    public class ResponseNatnCodeDataModel
    {
        public ObservableCollection<ResponseNatnCodeListModel> list { get; set; }
        public ResponseNatnCodeDataModel()
        {
            list = new ObservableCollection<ResponseNatnCodeListModel>();
        }
    }

    public class ResponseNatnCodeListModel
    {
        public virtual string natnCode { get; set; }
        public virtual string natnNm { get; set; }
    }
}