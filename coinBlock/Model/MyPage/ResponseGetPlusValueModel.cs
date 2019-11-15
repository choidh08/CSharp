using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.MyPage
{
    public class ResponseGetPlusValueModel : ResponseBaseModel
    {
        public ResponseGetPlusValueDataModel data { get; set; }
        public ResponseGetPlusValueModel()
        {
            data = new ResponseGetPlusValueDataModel();
        }
    }

    public class ResponseGetPlusValueDataModel
    {
        public ObservableCollection<ResponseGetPlusValueListModel> list { get; set; }
        public ResponseGetPlusValueDataModel()
        {
            list = new ObservableCollection<ResponseGetPlusValueListModel>();
        }
    }

    public class ResponseGetPlusValueListModel
    {
        public virtual string cnkndnm { get; set; }
        public virtual string cnkndcd { get; set; }
        public virtual decimal? posamt { get; set; }
        public virtual decimal? avgcnprc { get; set; }
        public virtual decimal? cshprc { get; set; }
        public virtual decimal? cnprc { get; set; }
        public virtual decimal? evalprc { get; set; }
        public virtual decimal? evalpnlprc { get; set; }
        public virtual decimal? marginRate { get; set; }
        public virtual string CommonFloat { get; set; }
    }
}