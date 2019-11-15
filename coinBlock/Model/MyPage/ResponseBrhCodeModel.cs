using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.MyPage
{
    public class ResponseBrhCodeModel : ResponseBaseModel
    {
        public ResponseBrhCodeDataModel data { get; set; }
        public ResponseBrhCodeModel()
        {
            data = new ResponseBrhCodeDataModel();
        }
    }

    public class ResponseBrhCodeDataModel
    {
        public ObservableCollection<ResponseBrhCodeListModel> list { get; set; }
        public ResponseBrhCodeDataModel()
        {
            list = new ObservableCollection<ResponseBrhCodeListModel>();
        }
    }

    public class ResponseBrhCodeListModel
    {
        public virtual string brhCode { get; set; }
        public virtual string brhNm { get; set; }
    }
}