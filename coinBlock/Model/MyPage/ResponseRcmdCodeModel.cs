using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.MyPage
{
    public class ResponseRcmdCodeModel : ResponseBaseModel
    {
        public ResponseRcmdCodeDataModel data { get; set; }
        public ResponseRcmdCodeModel()
        {
            data = new ResponseRcmdCodeDataModel();
        }
    }

    public class ResponseRcmdCodeDataModel
    {
        public ObservableCollection<ResponseRcmdCodeListModel> list { get; set; }
        public ResponseRcmdCodeDataModel()
        {
            list = new ObservableCollection<ResponseRcmdCodeListModel>();
        }
    }

    public class ResponseRcmdCodeListModel
    {
        public virtual string rcmdCode { get; set; }
        public virtual string rcmdNm { get; set; }
    }

}